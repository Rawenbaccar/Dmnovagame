using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

// Response classes for authentication
[Serializable]
public class AuthResponses
{
    [Serializable]
    public class TokenResponse
    {
        public string access;
        public string refresh;
    }

    [Serializable]
    public class RefreshTokenResponse
    {
        public string access;
        public string refresh;
    }

    [Serializable]
    public class ValidationErrorResponse
    {
        public string[] username;
        public string[] password;
        public string[] email;

        public string GetFormattedError()
        {
            System.Text.StringBuilder errorMsg = new System.Text.StringBuilder();
            
            if (username != null && username.Length > 0)
            {
                errorMsg.AppendLine("• Username: " + string.Join(", ", username));
            }
            if (password != null && password.Length > 0)
            {
                errorMsg.AppendLine("• Password: " + string.Join(", ", password));
            }
            if (email != null && email.Length > 0)
            {
                errorMsg.AppendLine("• Email: " + string.Join(", ", email));
            }

            return errorMsg.ToString().TrimEnd();
        }
    }
}

[Serializable]
public class RefreshTokenRequest
{
    public string refresh;
}

public class AuthManager : MonoBehaviour
{
    private static AuthManager instance;
    public static AuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AuthManager");
                instance = go.AddComponent<AuthManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    [Header("API Settings")]
    private string baseUrl = "http://51.255.29.221:8866";  // Make sure this matches your API URL

    private string accessToken;
    private string refreshToken;
    private bool isRefreshing = false;

    public bool IsAuthenticated => !string.IsNullOrEmpty(accessToken);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Try to load tokens when the game starts
        LoadTokens();
    }

    private void LoadTokens()
    {
        Debug.Log("Loading tokens from PlayerPrefs...");
        accessToken = PlayerPrefs.GetString("AccessToken", "");
        refreshToken = PlayerPrefs.GetString("RefreshToken", "");
        
        if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            Debug.Log("Tokens loaded successfully from PlayerPrefs");
            Debug.Log($"Access token length: {accessToken.Length}");
            Debug.Log($"Refresh token length: {refreshToken.Length}");
        }
        else
        {
            Debug.LogWarning("No tokens found in PlayerPrefs");
        }
    }

    public void SetTokens(string access, string refresh)
    {
        if (string.IsNullOrEmpty(access) || string.IsNullOrEmpty(refresh))
        {
            Debug.LogError("Attempting to set empty tokens!");
            return;
        }

        Debug.Log("Setting new tokens...");
        accessToken = access;
        refreshToken = refresh;
        
        // Save to PlayerPrefs for persistence
        PlayerPrefs.SetString("AccessToken", access);
        PlayerPrefs.SetString("RefreshToken", refresh);
        PlayerPrefs.Save();
        
        Debug.Log("Tokens saved successfully to PlayerPrefs");
        Debug.Log($"Access token length: {access.Length}");
        Debug.Log($"Refresh token length: {refresh.Length}");
    }

    public string GetAccessToken()
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            Debug.Log("Access token is empty, trying to load from PlayerPrefs...");
            accessToken = PlayerPrefs.GetString("AccessToken", "");
            
            if (!string.IsNullOrEmpty(accessToken))
            {
                Debug.Log("Access token loaded from PlayerPrefs");
            }
            else
            {
                Debug.LogWarning("No access token found in PlayerPrefs");
            }
        }
        return accessToken;
    }

    public string GetRefreshToken()
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            Debug.Log("Refresh token is empty, trying to load from PlayerPrefs...");
            refreshToken = PlayerPrefs.GetString("RefreshToken", "");
            
            if (!string.IsNullOrEmpty(refreshToken))
            {
                Debug.Log("Refresh token loaded from PlayerPrefs");
            }
            else
            {
                Debug.LogWarning("No refresh token found in PlayerPrefs");
            }
        }
        return refreshToken;
    }

    public void ClearTokens()
    {
        Debug.Log("Clearing all tokens...");
        accessToken = null;
        refreshToken = null;
        PlayerPrefs.DeleteKey("AccessToken");
        PlayerPrefs.DeleteKey("RefreshToken");
        PlayerPrefs.Save();
        Debug.Log("Tokens cleared successfully");
    }

    // Add this method to verify token storage
    public bool VerifyTokenStorage()
    {
        string storedAccess = PlayerPrefs.GetString("AccessToken", "");
        string storedRefresh = PlayerPrefs.GetString("RefreshToken", "");
        
        Debug.Log("Verifying token storage:");
        Debug.Log($"Access token in memory: {!string.IsNullOrEmpty(accessToken)}");
        Debug.Log($"Refresh token in memory: {!string.IsNullOrEmpty(refreshToken)}");
        Debug.Log($"Access token in PlayerPrefs: {!string.IsNullOrEmpty(storedAccess)}");
        Debug.Log($"Refresh token in PlayerPrefs: {!string.IsNullOrEmpty(storedRefresh)}");
        
        return !string.IsNullOrEmpty(storedAccess) && !string.IsNullOrEmpty(storedRefresh);
    }

    // Add this method for manual refresh
    public void ManualTokenRefresh(System.Action<bool> onComplete = null)
    {
        Debug.Log("Manual token refresh requested");
        // Verify tokens before attempting refresh
        VerifyTokenStorage();
        StartCoroutine(RefreshTokenCoroutine(onComplete));
    }

    public string GetAuthorizationHeader()
    {
        string token = GetAccessToken();
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("No access token available for Authorization header");
        }
        return "Bearer " + token;
    }

    // Call this method when you get a 401 Unauthorized response
    public IEnumerator RefreshTokenCoroutine(System.Action<bool> onComplete = null)
    {
        if (isRefreshing)
        {
            yield return new WaitUntil(() => !isRefreshing);
            onComplete?.Invoke(IsAuthenticated);
            yield break;
        }

        isRefreshing = true;
        Debug.Log("Attempting to refresh token...");

        string currentRefreshToken = GetRefreshToken();
        if (string.IsNullOrEmpty(currentRefreshToken))
        {
            Debug.LogError("No refresh token available");
            isRefreshing = false;
            onComplete?.Invoke(false);
            yield break;
        }

        // Create the refresh token request body
        RefreshTokenRequest requestBody = new RefreshTokenRequest
        {
            refresh = currentRefreshToken
        };
        string jsonData = JsonUtility.ToJson(requestBody);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "/auth/token/refresh/", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            Debug.Log($"Sending refresh request with token: {jsonData}");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Token refresh failed: {www.error}");
                Debug.LogError($"Response: {www.downloadHandler.text}");
                ClearTokens(); // Clear tokens on refresh failure
                isRefreshing = false;
                onComplete?.Invoke(false);
                yield break;
            }

            try
            {
                Debug.Log($"Refresh response: {www.downloadHandler.text}");
                var response = JsonUtility.FromJson<AuthResponses.RefreshTokenResponse>(www.downloadHandler.text);
                // Update both access and refresh tokens
                SetTokens(response.access, response.refresh);
                Debug.Log("Token refreshed successfully");
                onComplete?.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing refresh token response: {e.Message}");
                Debug.LogError($"Response was: {www.downloadHandler.text}");
                ClearTokens();
                onComplete?.Invoke(false);
            }
        }

        isRefreshing = false;
    }

    // Helper method to check if a response indicates the token needs refreshing
    public bool NeedsTokenRefresh(UnityWebRequest www)
    {
        return www.responseCode == 401; // Unauthorized
    }
} 