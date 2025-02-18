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

    #endregion

    [SerializeField] public Image healthBar; // L'image remplissable repr�sentant la barre de sant�

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Exemple pour tester la prise de d�g�ts

        Debug.Log(currentHealth);
        if (currentHealth <= 0f)
        {
            Debug.Log("IIIIIIWAH");
           PlayerDeath();
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Emp�che les valeurs n�gatives
       /* if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        UpdateHealthBar();*/
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; // Mise � jour du remplissage de l'image
        }
        else
        {
            Debug.Log($"{nameof(healthBar)} is null! ");
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