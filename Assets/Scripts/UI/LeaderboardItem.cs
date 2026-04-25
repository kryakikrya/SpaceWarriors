using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _emailText;

    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Change(string email, string score)
    {
        _emailText.text = email;

        _scoreText.text = score;
    }
}
