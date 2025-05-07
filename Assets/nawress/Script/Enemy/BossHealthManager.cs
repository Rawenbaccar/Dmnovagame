using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this to use the Slider

public class BossHealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public Animator bossAnimator;
    public float damageAmount = 5f;
    public PlayerHealthManager playerHealth;
    public Slider healthSlider;
    public float deathDelay = 1f;
    public bool isDead;
    [SerializeField] private GameObject magnetPrefab; // Prefab for Magnet drop
    [SerializeField] private GameObject healthPrefab; // Prefab for Health drop
    [SerializeField] private GameObject freezingPrefab; // Prefab for Freezing drop
   

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || collision.CompareTag("knife"))        {
            TakeDamage(damageAmount);
            

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                
            }
            else
            {
                Debug.LogError("PlayerHealthManager is not assigned to BossHealthManager!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Stop taking damage if already dead

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthSlider();

        if (currentHealth <= 0 && !isDead) // Prevent multiple calls
        {
            BossDeath();
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void BossDeath()
    {
        if (isDead) return;
        isDead = true;

        // Drop XP and weapon
        DropItems();

        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Dead");
        }

        if (GetComponent<BossAI>() != null)
        {
            GetComponent<BossAI>().DisableBossAI(); // Disable movement
        }

        StartCoroutine(DestroyBossAfterDelay());
    }

    private void DropItems()
    {
        float dropDistance = 1.5f; // Adjust the distance between items

        // Drop Magnet
        if (magnetPrefab != null)
        {
            Vector2 magnetPosition = (Vector2)transform.position + new Vector2(-dropDistance, 0);
            Instantiate(magnetPrefab, magnetPosition, Quaternion.identity);
        }

        // Drop Health
        if (healthPrefab != null)
        {
            Vector2 healthPosition = (Vector2)transform.position + new Vector2(0, dropDistance);
            Instantiate(healthPrefab, healthPosition, Quaternion.identity);
        }

        // Drop Freezing
        if (freezingPrefab != null)
        {
            Vector2 freezingPosition = (Vector2)transform.position + new Vector2(dropDistance, 0);
            Instantiate(freezingPrefab, freezingPosition, Quaternion.identity);
        }
    }


    IEnumerator DestroyBossAfterDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
