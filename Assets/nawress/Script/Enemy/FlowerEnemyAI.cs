using UnityEngine;
using System.Collections;

public class FlowerEnemyAI : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject flowerPrefab;
    public int numberOfFlowers = 20;
    public float radius = 5f;
    public float barrierDuration = 15f;
    [SerializeField] private bool isSpawner = true;

    [Header("Health Settings")]
    public int maxHealth = 70;
    public int currentHealth;  // Made public to see it clearly in inspector
    public Color fullHealthColor = Color.green;
    public Color lowHealthColor = new Color(0.424f, 0f, 0f);
    
    [Header("Knockback Settings")]
    public float knockbackDistance = 2f;
    public float knockbackDuration = 0.2f;
    
    private float spawnTime;
    private SpriteRenderer spriteRenderer;
    private static int activeFlowerCount;
    //private bool isKnockedBack = false;
    //private static bool hasSpawnedFlowers = false; // New static flag to track if we've spawned flowers
    
    void Awake()
    {
        // Initialize health as soon as object is created
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (isSpawner)
        {
            // Subscribe to level up event
            ExperienceLevelController.instance.OnLevelUp += CheckAndSpawnFlowers;
        }
        else
        {
            InitializeFlower();
        }
    }

    private void CheckAndSpawnFlowers(int level)
    {
        Debug.Log($"Checking level for flower spawn. Current level: {level}");
        if (level == 12)
        {
            Debug.Log("Level 12 reached! Attempting to spawn flowers...");
            SpawnFlowers();
            // Unsubscribe after spawning
            ExperienceLevelController.instance.OnLevelUp -= CheckAndSpawnFlowers;
        }
    }

    void InitializeFlower()
    {
        // Make sure health is set
        currentHealth = maxHealth;
        spawnTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeFlowerCount++;
        //Debug.Log($"Flower initialized with {currentHealth} HP!");
    }

    void Update()
    {
        if (!isSpawner && Time.time - spawnTime >= barrierDuration)
        {
            Die();
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawner)
        {
            //Debug.Log($"Collision detected with: {collision.gameObject.name}, Tag: {collision.tag}");
            
            if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || collision.CompareTag("knife"))
            {
                //Debug.Log("Whip hit detected!");
                TakeDamage(2);
            }
            else if (collision.CompareTag("Player"))
            {
                // Apply knockback to the player when they touch the flower
                PlayerKnockback playerKnockback = collision.gameObject.GetComponent<PlayerKnockback>();
                if (playerKnockback != null)
                {
                    playerKnockback.ApplyKnockback(transform.position);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        
        currentHealth -= damage;
        //Debug.Log($"Flower took {damage} damage! Current HP: {currentHealth}");

        StartCoroutine(DamageFlash());

        // Only die when health is exactly 0 or below
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Set to exactly 0
            Die();
        }
    }

    private System.Collections.IEnumerator DamageFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    void Die()
    {
        // Double check to make sure we only die at 0 health
        if (currentHealth > 0) return;

        activeFlowerCount--;
        
        if (activeFlowerCount <= 0)
        {
            if (FlowerBarrier.Instance != null)
            {
                FlowerBarrier.Instance.DestroyBarrier();
            }
        }
        
        //Debug.Log("Flower died at 0 HP!");
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (isSpawner)
        {
            // Clean up subscription when object is destroyed
            if (ExperienceLevelController.instance != null)
            {
                ExperienceLevelController.instance.OnLevelUp -= CheckAndSpawnFlowers;
            }
        }
        if (!isSpawner)
        {
            activeFlowerCount--;
        }
    }

    public void SpawnFlowers()
    {
        if (flowerPrefab == null)
        {
            //Debug.LogError("No flower prefab assigned!");
            return;
        }

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            //Debug.LogError("Player not found!");
            return;
        }

        for (int i = 0; i < numberOfFlowers; i++)
        {
            //Debug.Log("Spawning flower " + (i + 1));
            float angle = i * (360f / numberOfFlowers);
            float radian = angle * Mathf.Deg2Rad;

            Vector2 spawnPosition = new Vector2(
                player.position.x + Mathf.Cos(radian) * radius,
                player.position.y + Mathf.Sin(radian) * radius
            );

            GameObject flower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
            FlowerEnemyAI flowerAI = flower.AddComponent<FlowerEnemyAI>();
            
            // Set this as a flower instance, not a spawner
            flowerAI.isSpawner = false;
            
            // Copy settings
            flowerAI.maxHealth = this.maxHealth;
            flowerAI.barrierDuration = this.barrierDuration;
            flowerAI.fullHealthColor = this.fullHealthColor;
            flowerAI.lowHealthColor = this.lowHealthColor;
            
            // Initialize the flower immediately
            flowerAI.InitializeFlower();
        }
    }

    private IEnumerator Knockback(Transform attacker)
    {
        //isKnockedBack = true;
        Vector2 knockbackDirection = (transform.position - attacker.position).normalized;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + knockbackDirection * knockbackDistance;

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        //isKnockedBack = false;
    }
}
