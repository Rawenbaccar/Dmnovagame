using UnityEngine;
using UnityEngine.UI;

public class BatHealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float baseMaxHealth = 2f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthSlider;

    private float maxHealth;
    private string enemyName;

    void Start()
    {
        enemyName = gameObject.name;
        
        // Apply health scaling
        if (EnemyHealthManager.Instance != null)
        {
            maxHealth = baseMaxHealth * EnemyHealthManager.Instance.GetHealthMultiplier();
            Debug.Log($"[{enemyName}] Spawned with scaled health: {maxHealth} (Base: {baseMaxHealth} x Multiplier: {EnemyHealthManager.Instance.GetHealthMultiplier()})");
        }
        else
        {
            maxHealth = baseMaxHealth;
            Debug.Log($"[{enemyName}] Spawned with base health: {maxHealth} (EnemyHealthManager not found)");
        }
        
        currentHealth = maxHealth;
        
        // Setup health slider if attached
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"[{enemyName}] Took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[{enemyName}] Died!");
        
        // Spawn death effect if available
        if (EnemyControllerr.deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(EnemyControllerr.deathEffectPrefab, transform.position, Quaternion.identity);
            effect.tag = "Diamond";
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || 
            collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || 
            collision.CompareTag("knife"))
        {
            TakeDamage(1f);
        }
    }
} 