using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerAnimator playerAnimator; // Drag your player animator here in Inspector
    public Rigidbody2D rb;
    #region Private Variables
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float damageAmount = 3F;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private SurvivalTimer survivalTimer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hitSprite;
   // [SerializeField] private float hitEffectDuration = 0.2f;
    #endregion

    public Slider Healthslider; // L'image remplissable représentant la barre de santé
    public bool isDead;

    void Start()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInChildren<PlayerAnimator>();
            Debug.Log("Assigned PlayerAnimator: " + playerAnimator);
        }

        isDead = false;
        currentHealth = maxHealth;
        Healthslider.maxValue = maxHealth;
        Healthslider.value = currentHealth;  
        if (hitSprite != null)  // Only proceed if we want hit effects
        {
            // Get the SpriteRenderer if not assigned
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    return;
                }
            }
            // Store the normal sprite if not assigned
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
            playerAnimator.Die(); // Call the dead animation

        }
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
            TakeDamage(damageAmount);
        }
        if (collision.CompareTag("Boss")) // Check if the player collides with the boss
        {
            BossHealthManager bossHealth = collision.GetComponent<BossHealthManager>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(3f); // Deal damage to the boss
                
            }
        }
    }

    void PlayerDeath()
    {
        isDead = true;
        AudioManager.PlayGameOverSound(); // This will play the game over sound effect 

        // Quand le joueur meurt
        gameOverScreen.ShowGameOver();
       
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }
}

