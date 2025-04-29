using UnityEngine;

public class EnemyControllerr : MonoBehaviour
{
    // Référence statique partagée par tous les ennemis
    public static GameObject deathEffectPrefab;
    
    [Header("Health Settings")]
    [SerializeField] private float baseHealth = 2f;
    private float currentHealth;

    private void Start()
    {
        // Charge le préfab une seule fois si ce n'est pas déjà fait
        if (deathEffectPrefab == null)
        {
            deathEffectPrefab = Resources.Load<GameObject>("sprite");  // Changement du chemin
        }

        // Initialize health with scaling from EnemyHealthManager
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        // Apply health scaling if EnemyHealthManager exists
        if (EnemyHealthManager.Instance != null)
        {
            float healthMultiplier = EnemyHealthManager.Instance.GetHealthMultiplier();
            currentHealth = baseHealth * healthMultiplier;
            Debug.Log($"Enemy spawned with health: {currentHealth} (Base: {baseHealth} x Multiplier: {healthMultiplier})");
        }
        else
        {
            currentHealth = baseHealth;
            Debug.Log("EnemyHealthManager not found. Using base health: " + baseHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        // Handle MonsterEnemy specific death
        MonsterEnemy monsterEnemy = GetComponent<MonsterEnemy>();
        if (monsterEnemy != null)
        {
            monsterEnemy.Die();
        }

        // Handle HorroEnemy specific death
        HorroEnemy horroEnemy = GetComponent<HorroEnemy>();
        if (horroEnemy != null)
        {
            horroEnemy.Die();
        }

        // Spawn death effect
        if (deathEffectPrefab != null)
        {
            GameObject spawnedEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            spawnedEffect.tag = "Diamond";
        }

        // Destroy the enemy object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || 
            collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || 
            collision.CompareTag("knife"))
        {
            TakeDamage(1f);
        }
    }
}