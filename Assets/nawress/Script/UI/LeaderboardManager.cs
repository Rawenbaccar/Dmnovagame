using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using System;

[Serializable]
public class PlayerRank
{
    public int rank;
    public string username;
    public int survive_time;
}

[Serializable]
public class LeaderboardResponse
{
    public PlayerRank[] leaderboard;
    public PlayerRank current_user_rank;
}

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform leaderboardContent;        // The Content object of your ScrollView
    [SerializeField] private GameObject rankEntryPrefab;         // The prefab you'll create for each rank entry
    [SerializeField] private GameObject currentUserPanel;        // Panel for current user's rank
    
    [Header("Current User UI")]
    [SerializeField] private TextMeshProUGUI currentUserRank;    // Text showing current user's rank (#1, #2, etc)
    [SerializeField] private TextMeshProUGUI currentUserName;    // Text showing current user's name
    [SerializeField] private TextMeshProUGUI currentUserTime;    // Text showing current user's time

    private void Start()
    {
        // Validate all required components
        ValidateComponents();
        StartCoroutine(FetchLeaderboard());
    }

    private void ValidateComponents()
    {
        // Check and log status of each required component
        if (leaderboardContent == null)
        {
            //Debug.LogError("LeaderboardContent is not assigned!");
            // Updated path to match your exact hierarchy
            leaderboardContent = transform.Find("Canvas/Background/Leaderboard/Scroll View/Viewport/Content");
            //if (leaderboardContent == null)
                //Debug.LogError("Could not find Content transform automatically! Please create a Content object under Viewport!");
        }

        if (rankEntryPrefab == null)
        {
            //Debug.LogError("RankEntryPrefab is not assigned! Please assign the RankEntryPrefab from your Prefab folder!");
        }

        if (currentUserPanel == null)
        {
            //Debug.LogError("CurrentUserPanel is not assigned!");
            currentUserPanel = transform.Find("Canvas/Background/Leaderboard/Scroll View/CurrentUserPanel")?.gameObject;
            //if (currentUserPanel == null)
                ////Debug.LogError("Could not find CurrentUserPanel automatically!");
        }

        if (currentUserRank == null && currentUserPanel != null)
        {
            //Debug.LogError("CurrentUserRank Text is not assigned!");
            currentUserRank = currentUserPanel.transform.Find("RankText")?.GetComponent<TextMeshProUGUI>();
            //if (currentUserRank == null)
                //Debug.LogError("Could not find RankText in CurrentUserPanel!");
        }

        if (currentUserName == null && currentUserPanel != null)
        {
            //Debug.LogError("CurrentUserName Text is not assigned!");
            currentUserName = currentUserPanel.transform.Find("UsernameText")?.GetComponent<TextMeshProUGUI>();
            //if (currentUserName == null)
                //Debug.LogError("Could not find UsernameText in CurrentUserPanel!");
        }

        if (currentUserTime == null && currentUserPanel != null)
        {
            //Debug.LogError("CurrentUserTime Text is not assigned!");
            currentUserTime = currentUserPanel.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
            //if (currentUserTime == null)
                //Debug.LogError("Could not find TimeText in CurrentUserPanel!");
        }
    }

    public void RefreshLeaderboard()
    {
        StartCoroutine(FetchLeaderboard());
    }

    private IEnumerator FetchLeaderboard()
    {
        if (!ValidateBeforeFetch())
        {
            //Debug.LogError("Cannot fetch leaderboard - missing required components!");
            yield break;
        }

        string url = "http://51.255.29.221:8866/api/v1/leaderboard";

        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            
            // Check if AuthManager exists
            if (AuthManager.Instance == null)
            {
                //Debug.LogError("AuthManager instance is null!");
                yield break;
            }

            request.SetRequestHeader("Authorization", AuthManager.Instance.GetAuthorizationHeader());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    string jsonResponse = request.downloadHandler.text;
                    //Debug.Log($"Received JSON: {jsonResponse}"); // Debug log
                    LeaderboardResponse response = JsonUtility.FromJson<LeaderboardResponse>(jsonResponse);
                    UpdateLeaderboardUI(response);
                    break;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error parsing leaderboard response: {e.Message}");
                    break;
                }
            }
            else if (AuthManager.Instance.NeedsTokenRefresh(request))
            {
                Debug.Log("Token expired, attempting to refresh...");
                bool refreshSuccess = false;
                yield return AuthManager.Instance.RefreshTokenCoroutine((success) => refreshSuccess = success);
                
                if (!refreshSuccess)
                {
                    Debug.LogError("Failed to refresh authentication token");
                    break;
                }
                continue;
            }
            else
            {
                Debug.LogError($"Error fetching leaderboard: {request.error}");
                break;
            }
        }
    }

    private bool ValidateBeforeFetch()
    {
        return leaderboardContent != null && 
               rankEntryPrefab != null && 
               currentUserPanel != null && 
               currentUserRank != null && 
               currentUserName != null && 
               currentUserTime != null;
    }

    private void UpdateLeaderboardUI(LeaderboardResponse response)
    {
        if (response == null)
        {
            Debug.LogError("Received null leaderboard response!");
            return;
        }

        // Clear existing entries
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log($"Updating leaderboard with {response.leaderboard?.Length ?? 0} entries");

        // Update leaderboard entries
        if (response.leaderboard != null && response.leaderboard.Length > 0)
        {
            foreach (PlayerRank rank in response.leaderboard)
            {
                if (rank != null)
                {
                    GameObject entry = Instantiate(rankEntryPrefab, leaderboardContent);
                    if (!UpdateRankEntry(entry, rank))
                    {
                        Debug.LogError($"Failed to update rank entry for player {rank.username}");
                        continue;
                    }

                    // If this entry is the current user, highlight it
                    if (response.current_user_rank != null && 
                        rank.username == response.current_user_rank.username)
                    {
                        Image entryImage = entry.GetComponent<Image>();
                        if (entryImage != null)
                        {
                            entryImage.color = new Color(0.9058824f, 0.7058824f, 0.5019608f, 1f); // Subtle yellow highlight
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No leaderboard entries to display");
            GameObject entry = Instantiate(rankEntryPrefab, leaderboardContent);
            UpdateEmptyRankEntry(entry);
        }

        // Update current user panel
        if (response.current_user_rank != null)
        {
            Debug.Log($"Updating current user panel: Rank #{response.current_user_rank.rank} - {response.current_user_rank.username}");
            
            // Make sure the panel is visible
            currentUserPanel.SetActive(true);
            
            // Update the current user's information
            if (currentUserRank != null)
                currentUserRank.text = $"#{response.current_user_rank.rank}";
            else
                Debug.LogError("currentUserRank is null!");

            if (currentUserName != null)
                currentUserName.text = response.current_user_rank.username;
            else
                Debug.LogError("currentUserName is null!");

            if (currentUserTime != null)
                currentUserTime.text = FormatTime(response.current_user_rank.survive_time);
            else
                Debug.LogError("currentUserTime is null!");
        }
        else
        {
            Debug.Log("No current user rank data received");
            currentUserPanel.SetActive(false);
        }
    }

    private bool UpdateRankEntry(GameObject entry, PlayerRank rank)
    {
        if (entry == null)
        {
            Debug.LogError("Entry GameObject is null!");
            return false;
        }

        //Debug.Log($"Updating rank entry for player: {rank.username} (Rank #{rank.rank})");

        var rankText = entry.transform.Find("RankText")?.GetComponent<TextMeshProUGUI>();
        var usernameText = entry.transform.Find("UsernameText")?.GetComponent<TextMeshProUGUI>();
        var timeText = entry.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

        if (rankText == null)
            //Debug.LogError("RankText component not found in prefab!");
        if (usernameText == null)
            //Debug.LogError("UsernameText component not found in prefab!");
        if (timeText == null)
            //Debug.LogError("TimeText component not found in prefab!");

        if (rankText == null || usernameText == null || timeText == null)
        {
            return false;
        }

        rankText.text = $"#{rank.rank}";
        usernameText.text = rank.username;
        timeText.text = FormatTime(rank.survive_time);
        return true;
    }

    private void UpdateEmptyRankEntry(GameObject entry)
    {
        var rankText = entry.transform.Find("RankText")?.GetComponent<TextMeshProUGUI>();
        var usernameText = entry.transform.Find("UsernameText")?.GetComponent<TextMeshProUGUI>();
        var timeText = entry.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();

        if (rankText != null) rankText.text = "-";
        if (usernameText != null) usernameText.text = "No players yet";
        if (timeText != null) timeText.text = "--:--";
    }

    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:00}:{seconds:00}";
    }
} 