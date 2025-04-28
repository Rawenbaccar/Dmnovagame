using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoutManager : MonoBehaviour
{
    [SerializeField] private Button logoutButton;
    [SerializeField] private string loginSceneName = "LoginScene"; // Set this to your login scene

    private void Start()
    {
        if (logoutButton != null)
            logoutButton.onClick.AddListener(HandleLogout);
    }

    public void HandleLogout()
    {
        StartCoroutine(LogoutCoroutine());
    }

    private IEnumerator LogoutCoroutine()
    {
        string url = "http://localhost:8000/auth/logout/";
        string refreshToken = AuthManager.Instance.GetRefreshToken();

        if (string.IsNullOrEmpty(refreshToken))
        {
            Debug.LogError("No refresh token found. Logging out locally.");
            AuthManager.Instance.ClearTokens();
            SceneManager.LoadScene(loginSceneName);
            yield break;
        }

        var requestBody = new { refresh = refreshToken };
        string jsonData = JsonUtility.ToJson(requestBody);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", AuthManager.Instance.GetAuthorizationHeader());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Logout successful.");
            }
            else
            {
                Debug.LogWarning($"Logout request failed: {request.error} - {request.downloadHandler.text}");
            }

            AuthManager.Instance.ClearTokens();
            SceneManager.LoadScene(loginSceneName);
        }
    }
}