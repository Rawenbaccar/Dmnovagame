using UnityEngine;
using UnityEngine.UI;  // N'oublie pas d'ajouter cette directive pour accéder à Slider

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed = 5f;
    [SerializeField] private float baseAttackSpeed = 1f;

    [SerializeField] private float maxHealth = 100f; // Santé maximale
    private float currentHealth; // Santé actuelle

    [SerializeField] private Slider healthSlider; // Référence au Slider de la santé

    private void Start()
    {
        currentHealth = maxHealth; // La santé actuelle commence à la valeur maximale
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // Définit la valeur maximale du slider
            healthSlider.value = currentHealth; // Initialise le slider avec la santé actuelle
        }
    }

    // Méthode pour régénérer la santé au maximum et mettre à jour le slider
    public void RegenerateHealth()
    {
        currentHealth = maxHealth; // Remet la santé à son maximum

        // Met à jour le slider de santé pour qu'il affiche la santé complète
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        Debug.Log("Santé régénérée au maximum!");
    }

    public void IncreasePlayerSpeed()
    {
        basePlayerSpeed *= 1.5f;
        FindObjectOfType<PlayerMovement>().SetSpeed(basePlayerSpeed);
    }

    public void IncreaseAttackSpeed()
    {
        baseAttackSpeed /= 1.5f;
        FindObjectOfType<WhipWapean>().SetAttackSpeed(baseAttackSpeed);
    }
}