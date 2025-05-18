using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerAnimator playerAnimator; // Drag your player animator here in Inspector
    public Rigidbody2D rb;
    
    #region Private Variables
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float damageAmount = 3f;
    [SerializeField] private float attackDamage = 1f; // Added this line for Flower damage
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private SurvivalTimer survivalTimer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hitSprite;
    

    #endregion

    public Slider Healthslider; // The health bar UI
    public bool isDead;

    void Start()
    {
        if (playerAnimator == null)
        {
            playerAnimator = FindObjectOfType<PlayerAnimator>();
            Debug.Log("Assigned PlayerAnimator: " + playerAnimator);
        }

        isDead = false;
        currentHealth = maxHealth;
        Healthslider.maxValue = maxHealth;
        Healthslider.value = currentHealth;  

        if (hitSprite != null)  
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    return;
                }
            }
            if (normalSprite == null)
            {
                normalSprite = spriteRenderer.sprite;
            }
        }
    }

    void Update()
    {
        if (currentHealth <= 0f && !isDead)
        {
            PlayerDeath();
            playerAnimator.Die(); // Call the death animation
        }

        // Check if there are still flowers alive
       /* if (FlowerEnemyAI.flowerCount > 0)
        {
            // Prevent player from moving outside the circle
            Vector2 playerPosition = rb.position;
            float radius = 5f; // Same as the radius in FlowerEnemyAI
            if (playerPosition.magnitude > radius)
            {
                rb.position = playerPosition.normalized * radius; // Keep player within the circle
            }
        }*/
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Healthslider.value = currentHealth;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(damageAmount); // Player takes damage from enemies
        }
        if (collision.CompareTag("Boss"))
        {
            BossHealthManager bossHealth = collision.GetComponent<BossHealthManager>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(3f);
            }
        }
        if (collision.CompareTag("Flower"))
        {
            // **PLAYER TAKES DAMAGE FROM THE FLOWER**
            TakeDamage(5f); // Player loses 5 HP

            // **FLOWER TAKES DAMAGE FROM THE PLAYER**
            FlowerEnemyAI flower = collision.GetComponent<FlowerEnemyAI>();
            if (flower != null)
            {
                flower.TakeDamage(1); // Flower loses 1 HP
            }
        }
    }


    void PlayerDeath()
    {
        isDead = true;
        AudioManager.StopBackgroundMusic(); // Stop background music first
        AudioManager.PlayGameOverSound();

        gameOverScreen.ShowGameOver();
        survivalTimer.PlayerDied();
        
        // Send data to leaderboard
        StartCoroutine(UpdateLeaderboard());
       
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    private IEnumerator UpdateLeaderboard()
    {
        string url = "http://localhost:8000/api/v1/leaderboard/update/";
        
        // Convert survival time from float to int (seconds)
        int totalSeconds = Mathf.FloorToInt(survivalTimer.GetSurvivalTime());
        
        // Create the JSON data
        string jsonData = JsonUtility.ToJson(new LeaderboardData {
            survive_time = totalSeconds,  // Send total seconds as integer
            monster_killed = 0
        });

        while (true)
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", AuthManager.Instance.GetAuthorizationHeader());

            // For debugging - print the actual JSON being sent
            Debug.Log($"Sending JSON: {jsonData}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Leaderboard updated successfully!");
                break;
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
                // Enhanced error logging to see the actual response
                Debug.LogError($"Error updating leaderboard: {request.error}");
                Debug.LogError($"Response Code: {request.responseCode}");
                Debug.LogError($"Response Body: {request.downloadHandler.text}");
                break;
            }
        }
    }
}

[System.Serializable]
public class LeaderboardData
{
    public int survive_time;  // Changed from float to int
    public int monster_killed;
}
