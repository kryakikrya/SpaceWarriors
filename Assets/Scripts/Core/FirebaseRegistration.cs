using Firebase.Database;
using System.Threading.Tasks;
using TMPro;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseRegistration : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private Button _signUpButton;
    [SerializeField] private Button _signInButton;

    [SerializeField] private TextMeshProUGUI _statusText;

    [SerializeField] private Button _playButton;

    private void Start()
    {
        PlayerPrefs.SetString("CurrentUser", string.Empty);

        _signUpButton.onClick.AddListener(SignUp);

        _signInButton.onClick.AddListener(SignIn);
    }

    private async void SignUp()
    {
        if (IsValidEmailFormat(_inputField.text))
        {
            if (await SignUpAsync())
            {
                _statusText.text += "\nSuccess";
            }
            else
            {
                _statusText.text += "\nFailure";
            }
        }
        else
        {
            _statusText.text += "\nWrong Format";
        }
    }

    private async Task<bool> SignUpAsync()
    {
        string email = _inputField.text;

        string normalizedEmail = email.Trim().ToLowerInvariant();
        string emailKey = GetEmailKey(normalizedEmail);

        DatabaseReference userReference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(emailKey);

        DataSnapshot snapshot = await userReference.GetValueAsync();

        if (snapshot.Exists)
        {
            _statusText.text += "\nUser already exists";
            return false;
        }

        SessionData userData = new SessionData(normalizedEmail);

        string json = JsonConvert.SerializeObject(userData);

        await userReference.SetRawJsonValueAsync(json);

        _statusText.text += "\nUser created";
        return true;
    }

    private async void SignIn()
    {
        if (IsValidEmailFormat(_inputField.text))
        {
            if (await SignInAsync())
            {
                _statusText.text += "\nSuccess";

                _playButton.interactable = true;
            }
            else
            {
                _statusText.text += "\nFailure";
            }
        }
        else
        {
            _statusText.text += "\nWrong Format";
        }
    }

    private async Task<bool> SignInAsync()
    {
        string email = _inputField.text;

        string normalizedEmail = email.Trim().ToLowerInvariant();
        string emailKey = GetEmailKey(normalizedEmail);

        DatabaseReference userReference = FirebaseDatabase.DefaultInstance.GetReference("users").Child(emailKey);

        DataSnapshot snapshot = await userReference.GetValueAsync();

        if (snapshot.Exists == false)
        {
            _statusText.text += "\nUser not exists";
            return false;
        }

        PlayerPrefs.SetString("CurrentUser", emailKey);
        PlayerPrefs.Save();

        _statusText.text += "\nUser created";
        return true;
    }

    private bool IsValidEmailFormat(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return System.Text.RegularExpressions.Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private string GetEmailKey(string email)
    {
        string normalizedEmail = email.Trim().ToLowerInvariant();

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(normalizedEmail);

        return System.Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", string.Empty);
    }
}
