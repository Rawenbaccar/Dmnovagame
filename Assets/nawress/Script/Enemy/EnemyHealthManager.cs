using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Singleton instance
    public static EnemyHealthManager Instance { get; private set; }

    [Header("Health Scaling")]
    public float healthMultiplier;          // Current health multiplier
    public float increasePerLevel;        // 20% increase per level
    public float increasePerInterval;     // 10% increase every interval
    public float intervalTime;             // Time between automatic increases (15 seconds)

    public bool isAutoScalingActive = true;    // Becomes true at level 13
    private float timer = 0f;
    private int currentLevel = 1;


    public void Start()
    {
        EnemyHealthManager.Instance = this;
    }
    private void Update()
    {
        // After level 13, increase health every 15 seconds
        if (isAutoScalingActive)
        {
            timer += Time.deltaTime;
            if (timer >= intervalTime)
            {
                IncreaseHealthMultiplier(increasePerInterval);
                timer = 0f;
                Debug.Log($"Auto-scaling: Enemy health multiplier increased to {healthMultiplier}");
            }
        }
    }

    // Called when player levels up
    public void OnPlayerLevelUp(int newLevel)
    {
        currentLevel = newLevel;

        // Only start scaling health at level 13
        if (currentLevel >= 13)
        {
            // First time reaching level 13
            if (!isAutoScalingActive)
            {
                isAutoScalingActive = true;
                timer = 0f;
                // Start with base multiplier at level 13
                healthMultiplier = 1f;
                Debug.Log("Auto-scaling activated: Enemy health will now increase every 15 seconds");
            }

            // Increase health multiplier
            IncreaseHealthMultiplier(increasePerLevel);
        }

        Debug.Log($"Level {newLevel}: Enemy health multiplier is now {healthMultiplier}");
    }

    private void IncreaseHealthMultiplier(float amount)
    {
        healthMultiplier += amount;
    }

    // Call this to get the current health multiplier for new enemies
    public float GetHealthMultiplier()
    {
        return healthMultiplier;
    }
}