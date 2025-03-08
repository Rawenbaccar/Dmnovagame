using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 3f; // 3 hits to kill
    [SerializeField] private Slider healthBar; // Reference to UI health bar

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play death effect if available
        if (EnemyControllerr.deathEffectPrefab != null)
        {
            Instantiate(EnemyControllerr.deathEffectPrefab, transform.position, Quaternion.identity);
        }
        
        AudioManager.PlayEnemyDeathSound();
        Destroy(gameObject);
    }
}
