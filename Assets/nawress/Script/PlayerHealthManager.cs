using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    #region Private Variables
    public float currentHealth;
    [SerializeField] public float maxHealth = 100f;
    #endregion

    [SerializeField] public Image healthBar; // L'image remplissable représentant la barre de santé

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Exemple pour tester la prise de dégâts
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Empêche les valeurs négatives
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth; // Mise à jour du remplissage de l'image
    }
}
