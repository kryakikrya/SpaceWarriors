using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using TMPro;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardItem _itemPrefab;

    [SerializeField] private int _leaderboardPlaces = 3;

    [SerializeField] private TextMeshProUGUI _statusText;

    private List<LeaderboardItem> _items = new List<LeaderboardItem>();

    private void OnEnable()
    {
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("Score").LimitToLast(_leaderboardPlaces)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    _statusText.text += "\nLeaderboard load error";
                    return;
                }

                List<SessionData> leaders = new List<SessionData>();

                foreach (DataSnapshot childSnapshot in task.Result.Children)
                {
                    string json = childSnapshot.GetRawJsonValue();

                    SessionData userData = JsonConvert.DeserializeObject<SessionData>(json);

                    if (userData != null)
                    {
                        leaders.Add(userData);
                    }
                }

                _statusText.text += $"\nLeaders count = {leaders.Count}";

                if (_items.Count == 0)
                {
                    for (int i = 0; i < leaders.Count; i++)
                    {
                        LeaderboardItem item = Instantiate(_itemPrefab, transform);

                        item.transform.SetSiblingIndex(0);

                        item.Change(leaders[i].Email, leaders[i].Score.ToString());

                        _items.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < _items.Count; i++)
                    {
                        _items[i].Change(leaders[i].Email, leaders[i].Score.ToString());
                    }
                }
            });
    }
}
