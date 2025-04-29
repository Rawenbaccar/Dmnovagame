using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Singleton instance
    public static EnemyHealthManager Instance { get; private set; }

    [Header("Health Scaling")]
    public float healthMultiplier = 1f;          // Current health multiplier
    public float increasePerLevel = 0.2f;        // 20% increase per level
    public float increasePerInterval = 0.1f;     // 10% increase every interval
    public float intervalTime = 15f;             // Time between automatic increases (15 seconds)

    private bool isAutoScalingActive = false;    // Becomes true at level 13
    private float timer = 0f;
    private int currentLevel = 1;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        
        // Increase health multiplier
        IncreaseHealthMultiplier(increasePerLevel);
        
        // Activate auto-scaling at level 13
        if (newLevel >= 13 && !isAutoScalingActive)
        {
            isAutoScalingActive = true;
            timer = 0f;
            Debug.Log("Auto-scaling activated: Enemy health will now increase every 15 seconds");
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