using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
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
    

    void Start()
    {
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
        if (currentHealth <= 0f)
        {
            PlayerDeath();
        }
    }

    void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Healthslider.value = currentHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(damageAmount);
        }
    }

    void PlayerDeath()
    {
        // Quand le joueur meurt
        gameOverScreen.ShowGameOver();
    }
}

