using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class FirebaseDataSaver : IInitializable, IDisposable
{
    [Inject] private ScoreRewardModel _scoreRewardModel;

    private SessionData _data;

    private FirebaseAuth _auth;
    private FirebaseUser _user;

    public void Initialize()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChanged;

        _scoreRewardModel.Score.OnChanged += ChangeLastScore;

        CheckUser();
    }

    public void Dispose()
    {
        _scoreRewardModel.Score.OnChanged -= ChangeLastScore;

        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChanged;
    }

    private void OnSignedIn(Task<FirebaseUser> signInTask)
    {
        _user = signInTask.Result;
        if (signInTask.IsFaulted || signInTask.IsCanceled)
        {
            Debug.Log("Auth error");
        }

        var reference = FirebaseDatabase.DefaultInstance.GetReference($"users/{PlayerPrefs.GetString("CurrentUser")}");
        reference.ValueChanged += OnUsersDataChanged;
    }

    public void ChangeLastScore(int score)
    {
        SaveCurrentSession(score);
    }

    public void SaveCurrentSession(int score)
    {
        if (score <= _data.Score)
        {
            return;
        }

        _data.Score = score;
        var jsonNewUser = JsonConvert.SerializeObject(_data);

        FirebaseDatabase.DefaultInstance.GetReference($"users/{PlayerPrefs.GetString("CurrentUser")}")
            .SetRawJsonValueAsync(jsonNewUser)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log("Error");
                    return;
                }

                Debug.Log($"New high score saved: {_data.Score}");
            });
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
        string currentUserKey = PlayerPrefs.GetString("CurrentUser");

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            _auth = FirebaseAuth.DefaultInstance;
            _user = _auth.CurrentUser;

            var reference = FirebaseDatabase.DefaultInstance.GetReference($"users/{PlayerPrefs.GetString("CurrentUser")}");
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
                    .GetReference($"users/{PlayerPrefs.GetString("CurrentUser")}");

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

            SaveCurrentSession(0);
        }
    }
}
