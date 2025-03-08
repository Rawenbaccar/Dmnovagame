using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use Slider

public class WolfHealthController : MonoBehaviour
{
    public static GameObject deathEffectPrefab;
    [SerializeField] private float maxHealth = 3f; // Maximum health (3 hits)
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthSlider; // Reference to the health slider

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
         
        // Charge le préfab une seule fois si ce n'est pas déjà fait
        if (deathEffectPrefab == null)
        {
            deathEffectPrefab = Resources.Load<GameObject>("sprite");  // Changement du chemin
        }
    
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "WhipAttack" or "WhipAttack1" tag
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1"))
        {
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            }
            TakeDamage(1); // Take 1 damage for each hit
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        UpdateHealthSlider(); // Update the health slider

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health is 0 or less
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Update the slider value
            //Debug.Log("Slider updated to: " + currentHealth); // Log the current health for debugging
        }
        else
        {
            Debug.LogError("Health Slider is not assigned!"); // Log an error if the slider is null
        }
    }

    private void Die()
    {
        // Here you can add any death effects or animations if needed
       
        Destroy(gameObject); // Destroy the wolf game object
    }

    
} 