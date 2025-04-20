using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;  // Add this for scene management
using TMPro;
using System;

[Serializable]
public class SignupRequest
{
    public string username;
    public string email;
    public string password;
}

[Serializable]
public class LoginResponse
{
    public string token;
}

[Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

[Serializable]
public class TokenResponse
{
    public string access;
    public string refresh;
}

public class Authentification : MonoBehaviour
{
    [Header("Login Panel References")]
    [SerializeField] private TMP_InputField loginusernameInput;
    [SerializeField] private TMP_InputField loginPasswordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI loginButtonText;  // Add reference to button text
    [SerializeField] private TextMeshProUGUI loginErrorText;
    [SerializeField] private TextMeshProUGUI InfoUserNameText;
    [SerializeField] private TextMeshProUGUI InfoPasswordText;

    [Header("Signup Panel References")]
    [SerializeField] private TMP_InputField signupUsernameInput;
    [SerializeField] private TMP_InputField signupEmailInput;
    [SerializeField] private TMP_InputField signupPasswordInput;
    [SerializeField] private TMP_InputField signupConfirmPasswordInput;
    [SerializeField] private Button signupButton;
    [SerializeField] private TextMeshProUGUI signupButtonText;  // Add reference to signup button text
    [SerializeField] private TextMeshProUGUI signupErrorText;
    [SerializeField] private TextMeshProUGUI InfoSignUserNameText;
    [SerializeField] private TextMeshProUGUI InfoSignMailText;
    [SerializeField] private TextMeshProUGUI InfoSignPasswordText;

    [Header("Panel References")]
    [SerializeField] private GameObject loginPanel;    // Reference to your login panel
    [SerializeField] private GameObject signupPanel;   // Reference to your signup panel
    [SerializeField] private Button switchToSignupButton;  // "Sign up" link in login panel
    [SerializeField] private Button switchToLoginButton;   // "Login" link in signup panel

    [Header("API Settings")]
    private string baseUrl = "http://localhost:8000"; // Base URL without endpoints

    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene";  // Add this to set your game scene name

    private Color successColor = new Color(0.0f, 0.6f, 0.0f);  // Darker green color

    private const string LOGIN_BUTTON_DEFAULT_TEXT = "LOGIN";
    private const string LOGIN_BUTTON_LOADING_TEXT = "Logging in...";
    private const string SIGNUP_BUTTON_DEFAULT_TEXT = "SIGN UP";
    private const string SIGNUP_BUTTON_LOADING_TEXT = "Creating account...";

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to buttons
        loginButton.onClick.AddListener(HandleLogin);
        signupButton.onClick.AddListener(HandleSignup);
        
        // Add listeners for panel switching
        if (switchToSignupButton != null)
            switchToSignupButton.onClick.AddListener(ShowSignupPanel);
        if (switchToLoginButton != null)
            switchToLoginButton.onClick.AddListener(ShowLoginPanel);
        
        // Initialize button texts
        if(loginButtonText != null)
            loginButtonText.text = LOGIN_BUTTON_DEFAULT_TEXT;
        if(signupButtonText != null)
            signupButtonText.text = SIGNUP_BUTTON_DEFAULT_TEXT;
        
        // Clear all error messages
        if(loginErrorText != null) {
            loginErrorText.text = "";
            loginErrorText.color = Color.red;
        }
        if(signupErrorText != null) {
            signupErrorText.text = "";
            signupErrorText.color = Color.red;
        }
        if(InfoUserNameText != null) {
            InfoUserNameText.text = "";
            InfoUserNameText.color = Color.red;
        }
        if(InfoPasswordText != null) {
            InfoPasswordText.text = "";
            InfoPasswordText.color = Color.red;
        }
        if(InfoSignUserNameText != null) {
            InfoSignUserNameText.text = "";
            InfoSignUserNameText.color = Color.red;
        }
        if(InfoSignMailText != null) {
            InfoSignMailText.text = "";
            InfoSignMailText.color = Color.red;
        }
        if(InfoSignPasswordText != null) {
            InfoSignPasswordText.text = "";
            InfoSignPasswordText.color = Color.red;
        }

        ShowLoginPanel();
    }

    private void ClearStateMessages()
    {
        // Clear login panel messages
        if(loginErrorText != null)
            loginErrorText.text = "";
        if(InfoUserNameText != null)
            InfoUserNameText.text = "";
        if(InfoPasswordText != null)
            InfoPasswordText.text = "";

        // Clear signup panel messages
        if(signupErrorText != null)
            signupErrorText.text = "";
        if(InfoSignUserNameText != null)
            InfoSignUserNameText.text = "";
        if(InfoSignMailText != null)
            InfoSignMailText.text = "";
        if(InfoSignPasswordText != null)
            InfoSignPasswordText.text = "";
    }

    // Function to switch to Login Panel
    public void ShowLoginPanel()
    {
        ClearStateMessages();
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
    }

    // Function to switch to Signup Panel
    public void ShowSignupPanel()
    {
        ClearStateMessages();
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
    }

    public void HandleLogin()
    {
        ClearStateMessages();
        StartCoroutine(LoginCoroutine());
    }

    public void HandleSignup()
    {
        ClearStateMessages();
        // Validate username
        if (string.IsNullOrEmpty(signupUsernameInput.text))
        {
            signupErrorText.text = "Username is required!";
            return;
        }

        // Validate password length and content
        string password = signupPasswordInput.text;
        if (password.Length < 5)
        {
            signupErrorText.text = "Password must be at least 5 characters long!";
            return;
        }

        // Check for at least 4 letters
        int letterCount = 0;
        foreach (char c in password)
        {
            if (char.IsLetter(c))
            {
                letterCount++;
            }
        }
        
        // Check for at least 1 digit
        int digitCount = 0;
        foreach (char c in password)
        {
            if (char.IsDigit(c))
            {
                digitCount++;
            }
        }

        if (letterCount < 4 || digitCount < 1)
        {
            signupErrorText.text = "Password must contain at least 4 letters and 1 number!";
            return;
        }

        // Validate passwords match
        if (signupPasswordInput.text != signupConfirmPasswordInput.text)
        {
            signupErrorText.text = "Passwords do not match!";
            return;
        }

        StartCoroutine(SignupCoroutine());
    }

    private IEnumerator LoginCoroutine()
    {
        loginButton.interactable = false;
        
        // Clear all error messages at start
        if(loginErrorText != null) loginErrorText.text = "";
        if(InfoUserNameText != null) {
            InfoUserNameText.text = "";
            InfoUserNameText.color = Color.red;
        }
        if(InfoPasswordText != null) {
            InfoPasswordText.text = "";
            InfoPasswordText.color = Color.red;
        }

        // Update button text to show loading state
        if(loginButtonText != null)
        {
            loginButtonText.text = LOGIN_BUTTON_LOADING_TEXT;
        }

        Debug.Log("Sending login request to: " + baseUrl + "/auth/login/");

        // Create form data
        WWWForm form = new WWWForm();
        form.AddField("username", loginusernameInput.text);
        form.AddField("password", loginPasswordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/auth/login/", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Error: " + www.error);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Reset button text on error
                if(loginButtonText != null)
                {
                    loginButtonText.text = LOGIN_BUTTON_DEFAULT_TEXT;
                }

                Debug.LogError("Login Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
                
                try
                {
                    // Try to parse as validation error
                    var validationError = JsonUtility.FromJson<AuthResponses.ValidationErrorResponse>(www.downloadHandler.text);
                    
                    // Display username errors
                    if (validationError.username != null && validationError.username.Length > 0)
                    {
                        if(InfoUserNameText != null)
                        {
                            InfoUserNameText.text = string.Join("\n", validationError.username);
                        }
                    }

                    // Display password errors
                    if (validationError.password != null && validationError.password.Length > 0)
                    {
                        if(InfoPasswordText != null)
                        {
                            InfoPasswordText.text = string.Join("\n", validationError.password);
                        }
                    }

                    // If we have any other errors, show them in the main error text
                    if (loginErrorText != null && 
                        (validationError.username == null || validationError.username.Length == 0) &&
                        (validationError.password == null || validationError.password.Length == 0))
                    {
                        loginErrorText.color = Color.red;
                        loginErrorText.alignment = TextAlignmentOptions.Center;
                        loginErrorText.text = "Login failed: " + www.error;
                    }
                }
                catch
                {
                    // If we can't parse as validation error, show generic error in main error text
                    if(loginErrorText != null)
                    {
                        loginErrorText.color = Color.red;
                        loginErrorText.alignment = TextAlignmentOptions.Center;
                        loginErrorText.text = "Login failed. Please try again.";
                    }
                }
            }
            else
            {
                bool loginSuccess = false;
                string username = loginusernameInput.text;

                try {
                    // Parse the full response with both tokens
                    var tokenResponse = JsonUtility.FromJson<AuthResponses.TokenResponse>(www.downloadHandler.text);
                    
                    // Store both tokens in AuthManager
                    AuthManager.Instance.SetTokens(tokenResponse.access, tokenResponse.refresh);
                    
                    loginSuccess = true;
                    Debug.Log("Tokens stored successfully!");
                }
                catch (Exception e) {
                    // Reset button text on error
                    if(loginButtonText != null)
                    {
                        loginButtonText.text = LOGIN_BUTTON_DEFAULT_TEXT;
                    }

                    Debug.LogError("Error parsing login response: " + e.Message);
                    Debug.LogError("Response was: " + www.downloadHandler.text);
                    if(loginErrorText != null) {
                        loginErrorText.color = Color.red;
                        loginErrorText.alignment = TextAlignmentOptions.Center;
                        loginErrorText.text = "Login succeeded but got unexpected response";
                    }
                }

                // Handle successful login outside try-catch
                if (loginSuccess)
                {
                    // Clear any error messages
                    if(InfoUserNameText != null) InfoUserNameText.text = "";
                    if(InfoPasswordText != null) InfoPasswordText.text = "";
                    
                    if(loginErrorText != null) {
                        loginErrorText.color = successColor;
                        loginErrorText.alignment = TextAlignmentOptions.Center;
                        loginErrorText.text = "Login successful! Welcome back, " + username;
                    }

                    yield return new WaitForSeconds(2f);
                    SceneManager.LoadScene(gameSceneName);
                }
            }
        }

        // Reset button state
        loginButton.interactable = true;
        if(loginButtonText != null)
        {
            loginButtonText.text = LOGIN_BUTTON_DEFAULT_TEXT;
        }
    }

    private IEnumerator SignupCoroutine()
    {
        signupButton.interactable = false;

        // Clear all error messages
        if(signupErrorText != null) signupErrorText.text = "";
        if(InfoSignUserNameText != null) {
            InfoSignUserNameText.text = "";
            InfoSignUserNameText.color = Color.red;
        }
        if(InfoSignMailText != null) {
            InfoSignMailText.text = "";
            InfoSignMailText.color = Color.red;
        }
        if(InfoSignPasswordText != null) {
            InfoSignPasswordText.text = "";
            InfoSignPasswordText.color = Color.red;
        }

        // Update button text to show loading state
        if(signupButtonText != null)
            signupButtonText.text = SIGNUP_BUTTON_LOADING_TEXT;

        // Validate passwords match
        if (signupPasswordInput.text != signupConfirmPasswordInput.text)
        {
            if(InfoSignPasswordText != null) {
                InfoSignPasswordText.color = Color.red;
                InfoSignPasswordText.text = "Passwords do not match!";
            }
            // Reset button
            signupButton.interactable = true;
            if(signupButtonText != null)
                signupButtonText.text = SIGNUP_BUTTON_DEFAULT_TEXT;
            yield break;
        }

        // Create signup data
        var signupData = new SignupRequest
        {
            username = signupUsernameInput.text,
            email = signupEmailInput.text,
            password = signupPasswordInput.text
        };

        string jsonData = JsonUtility.ToJson(signupData);
        Debug.Log("Sending signup request to: " + baseUrl + "/auth/signup/");
        Debug.Log("Request data: " + jsonData);

        // Create form data
        WWWForm form = new WWWForm();
        form.AddField("username", signupUsernameInput.text);
        form.AddField("email", signupEmailInput.text);
        form.AddField("password", signupPasswordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/auth/signup/", form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode);
            Debug.Log("Response Error: " + www.error);
            Debug.Log("Response Text: " + www.downloadHandler.text);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Signup Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
                
                try
                {
                    // Try to parse as validation error
                    var validationError = JsonUtility.FromJson<AuthResponses.ValidationErrorResponse>(www.downloadHandler.text);
                    
                    // Display username errors
                    if (validationError.username != null && validationError.username.Length > 0)
                    {
                        if(InfoSignUserNameText != null)
                        {
                            InfoSignUserNameText.text = string.Join("\n", validationError.username);
                        }
                    }

                    // Display email errors
                    if (validationError.email != null && validationError.email.Length > 0)
                    {
                        if(InfoSignMailText != null)
                        {
                            InfoSignMailText.text = string.Join("\n", validationError.email);
                        }
                    }

                    // Display password errors
                    if (validationError.password != null && validationError.password.Length > 0)
                    {
                        if(InfoSignPasswordText != null)
                        {
                            InfoSignPasswordText.text = string.Join("\n", validationError.password);
                        }
                    }

                    // If we have any other errors, show them in the main error text
                    if (signupErrorText != null && 
                        (validationError.username == null || validationError.username.Length == 0) &&
                        (validationError.email == null || validationError.email.Length == 0) &&
                        (validationError.password == null || validationError.password.Length == 0))
                    {
                        signupErrorText.color = Color.red;
                        signupErrorText.text = "Signup failed: " + www.error;
                    }
                }
                catch
                {
                    if(signupErrorText != null)
                    {
                        signupErrorText.color = Color.red;
                        signupErrorText.text = "Signup failed. Please try again.";
                    }
                }
            }
            else
            {
                Debug.Log("Signup successful!");
                // Clear all error messages
                if(InfoSignUserNameText != null) InfoSignUserNameText.text = "";
                if(InfoSignMailText != null) InfoSignMailText.text = "";
                if(InfoSignPasswordText != null) InfoSignPasswordText.text = "";
                
                if(signupErrorText != null) {
                    signupErrorText.color = successColor;
                    signupErrorText.text = "Account created successfully!";
                }
                
                yield return new WaitForSeconds(2f);
                ShowLoginPanel();
            }
        }

        // Reset button state
        signupButton.interactable = true;
        if(signupButtonText != null)
            signupButtonText.text = SIGNUP_BUTTON_DEFAULT_TEXT;
    }
}
