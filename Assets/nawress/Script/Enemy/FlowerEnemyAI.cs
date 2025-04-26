using UnityEngine;

public class FlowerEnemyAI : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject flowerPrefab;
    public int numberOfFlowers = 20;
    public float radius = 5f;
    public float barrierDuration = 15f;

    [Header("Health Settings")]
    public int maxHealth = 70;
    public int currentHealth;  // Made public to see it clearly in inspector
    public Color fullHealthColor = Color.green;
    public Color lowHealthColor = new Color(0.424f, 0f, 0f);
    
    private float spawnTime;
    private SpriteRenderer spriteRenderer;
    private static int activeFlowerCount;
    private bool isSpawner = true;
    
    void Awake()
    {
        // Initialize health as soon as object is created
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (isSpawner)
        {
            SpawnFlowers();
        }
        else
        {
            InitializeFlower();
        }
    }

    void InitializeFlower()
    {
        // Make sure health is set
        currentHealth = maxHealth;
        spawnTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeFlowerCount++;
        Debug.Log($"Flower initialized with {currentHealth} HP!");
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
            Debug.Log($"Collision detected with: {collision.gameObject.name}, Tag: {collision.tag}");
            
            if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1"))
            {
                Debug.Log("Whip hit detected!");
                TakeDamage(2);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        
        currentHealth -= damage;
        Debug.Log($"Flower took {damage} damage! Current HP: {currentHealth}");

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
        
        Debug.Log("Flower died at 0 HP!");
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (!isSpawner)
        {
            activeFlowerCount--;
        }
    }

    public void SpawnFlowers()
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
            Debug.Log("Spawning flower " + (i + 1));
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
}
