using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class FirebaseDataSaver : MonoBehaviour
{
    public static FirebaseDataSaver instance;

    [Inject] private ScoreRewardModel _scoreRewardModel;

    private SessionData _data;

    private FirebaseAuth _auth;
    private FirebaseUser _user;

    private void Awake()
    {
        if (instance == null)
        {
            FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChanged;

            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        _scoreRewardModel.Score.OnChanged += ChangeRecordScore;
    }

    private void OnDisable()
    {
        _scoreRewardModel.Score.OnChanged -= ChangeRecordScore;
    }

    private void OnSignedIn(Task<FirebaseUser> signInTask)
    {
        _user = signInTask.Result;
        if (signInTask.IsFaulted || signInTask.IsCanceled)
        {
            Debug.Log("Auth error");
        }

        var reference = FirebaseDatabase.DefaultInstance.GetReference($"users/{_user.UserId}");
        reference.ValueChanged += OnUsersDataChanged;
    }

    private void OnUsersDataChanged(object sender, ValueChangedEventArgs args)
    {
        ParseUserData(args.Snapshot);
    }

    private void HandleAuthStateChanged(object sender, EventArgs args)
    {
        CheckUser();
    }

    private void CheckUser()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            _auth = FirebaseAuth.DefaultInstance;
            _user = _auth.CurrentUser;

            var reference = FirebaseDatabase.DefaultInstance.GetReference($"users/{_user.UserId}");
            reference.ValueChanged += OnUsersDataChanged;
        }
        else
        {
            _auth = FirebaseAuth.GetAuth(Firebase.FirebaseApp.DefaultInstance);

            _auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    return;
                }

                _user = task.Result.User;

                var reference = FirebaseDatabase.DefaultInstance
                    .GetReference($"users/{_user.UserId}");

                reference.ValueChanged += OnUsersDataChanged;
            });
        }
    }
    
    private void ParseUserData(DataSnapshot snapshot)
    {
        var json = snapshot.GetRawJsonValue();

        if (json != null)
        {
            _data = JsonConvert.DeserializeObject<SessionData>(json);
        }
        else
        {
            _data = new SessionData();

            SaveCurrentSession();
        }
    }

    public void ChangeRecordScore(int score)
    {
        if (_data.Score < score)
        {
            _data.Score = score;
        }

        SaveCurrentSession();
    }

    public void SaveCurrentSession()
    {
        var jsonNewUser = JsonConvert.SerializeObject(_data);

        FirebaseDatabase.DefaultInstance.GetReference($"users/{_user.UserId}").SetRawJsonValueAsync(jsonNewUser).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Error");
            }
        });
    }
}
