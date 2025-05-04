using UnityEngine;
using TMPro;

public class ResultsDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI survivedText;    // Shows "Survived:" label
    [SerializeField] private TextMeshProUGUI goldEarnedText;  // Shows "Gold Earned:" label
    [SerializeField] private TextMeshProUGUI levelReachedText; // Shows "Level Reached:" label
    [SerializeField] private TextMeshProUGUI playerNameText;   // Shows player name

    [Header("Game References")]
    [SerializeField] private SurvivalTimer survivalTimer;
    [SerializeField] private ExperienceLevelController levelController;

    private void Start()
    {
        UpdateResultsDisplay();
    }

    private void UpdateResultsDisplay()
    {
        // Update Survived Time
        if (survivalTimer != null)
        {
            survivedText.text = survivalTimer.GetFormattedTime();
        }
        else
        {
            survivedText.text = "00:00";
            Debug.LogWarning("SurvivalTimer not assigned!");
        }

        // Update Level Reached
        if (levelController != null)
        {
           /* levelReachedText.text = levelController.GetCurrentLevel().ToString();*/
        }
        else
        {
            levelReachedText.text = "1";
            Debug.LogWarning("LevelController not assigned!");
        }

        // Update Player Name (if using authentication)
        if (AuthManager.Instance != null)
        {
          /*  playerNameText.text = AuthManager.Instance.GetCurrentUsername();*/
        }
        else
        {
            playerNameText.text = "Player";
        }

        // For now, gold earned is just set to 0
        goldEarnedText.text = "0";
    }
}