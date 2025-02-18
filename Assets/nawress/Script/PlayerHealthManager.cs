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
    [SerializeField] private float hitEffectDuration = 0.2f;

    #endregion

    public Slider Healthslider; // L'image remplissable repr�sentant la barre de sant�
    

    void Start()
    {
        currentHealth = maxHealth;
        Healthslider.maxValue = maxHealth;
        Healthslider.value = currentHealth;
      //check with yassine   
        // Only try to get and use SpriteRenderer if we need hit effects
        if (hitSprite != null)  // Only proceed if we want hit effects
        {
            // Get the SpriteRenderer if not assigned
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    Debug.LogWarning("No SpriteRenderer found on " + gameObject.name + ". Hit effects will be disabled.");
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

        // Exemple pour tester la prise de d�g�ts

        Debug.Log(currentHealth);
        if (currentHealth <= 0f)
        {
            Debug.Log("IIIIIIWAH");
           PlayerDeath();

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(damageAmount);

        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Emp�che les valeurs n�gatives
       /* if (currentHealth <= 0)

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Emp�che les valeurs n�gatives
        
        
        
        if (currentHealth <= 0)

        {
            survivalTimer.PlayerDied();
            gameObject.SetActive(false);
        }

        UpdateHealthBar();*/

        Healthslider.value = currentHealth;
        

    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (healthBar != null)

        if (collision.tag == "Enemy")

        {
            TakeDamage(damageAmount);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(damageAmount);
    }
    public void PlayerDeath()
    {
        // Quand le joueur meurt
        gameOverScreen.ShowGameOver();
    }


}

}

