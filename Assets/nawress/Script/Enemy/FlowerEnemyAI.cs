using UnityEngine;

public class FlowerEnemyAI : MonoBehaviour
{
    public GameObject flowerPrefab; // Prefab of the flower enemy
    public int numberOfFlowers = 20; // Number of flowers to spawn
    public float radius = 5f; // Distance from player
    public float barrierDuration = 15f; // How long the flower barrier lasts
    public Color fullHealthColor = Color.green; // Color when at full health
    public Color lowHealthColor = Color.red; // Color when at low health

    public int maxHealth = 15;  // Flower health
    private int currentHealth; // Track current health
    private float spawnTime; // When this flower was spawned
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    private static int activeFlowerCount; // Track number of active flowers
    
    void Start()
    {
        currentHealth = maxHealth;
        spawnTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeFlowerCount++;
        
        if (transform.parent == null) // Only the parent flower spawns the circle
        {
            SpawnFlowers();
        }
    }

    void Update()
    {
        // Check if the barrier should disappear due to time
        if (Time.time - spawnTime >= barrierDuration)
        {
            Die();
            return;
        }

        // Update flower color based on health
        if (spriteRenderer != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            spriteRenderer.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercentage);
        }
    }

    void SpawnFlowers()
    {
        if (flowerPrefab == null)
        {
            Debug.LogError("No flower prefab assigned!");
            return;
        }

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        for (int i = 0; i < numberOfFlowers; i++)
        {
            float angle = i * (360f / numberOfFlowers); // Spread evenly in a circle
            float radian = angle * Mathf.Deg2Rad; // Convert to radians

            Vector2 spawnPosition = new Vector2(
                player.position.x + Mathf.Cos(radian) * radius,
                player.position.y + Mathf.Sin(radian) * radius
            );

            GameObject flower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
            flower.transform.SetParent(transform); // Parent to the main flower
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Flower took damage! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        activeFlowerCount--;
        
        // If this was the last flower, notify the barrier
        if (activeFlowerCount <= 0)
        {
            if (FlowerBarrier.Instance != null)
            {
                FlowerBarrier.Instance.DestroyBarrier();
            }
        }
        
        Debug.Log("Flower died!");
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // Ensure we decrease the count if destroyed by other means
        activeFlowerCount--;
    }
}
