using UnityEngine;
using TMPro; // We use TextMeshPro because I see you're using it in your project

public class SurvivalTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // This will show the time on screen
    private float survivalTime = 0f;
    private bool isPlayerAlive = true;

    [SerializeField] private SurvivalTimer survivalTimer;

    void Update()
    {
        if (isPlayerAlive)
        {
            // Add time
            survivalTime += Time.deltaTime;
            
            // Update the text
            int minutes = Mathf.FloorToInt(survivalTime / 60f);
            int seconds = Mathf.FloorToInt(survivalTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}"; // Now just shows "00:00" format
        }
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
        Debug.Log($"Final Time: {timerText.text}");
    }
} 