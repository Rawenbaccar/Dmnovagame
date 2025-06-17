using UnityEngine;
using UnityEngine.UI;  // N'oublie pas d'ajouter cette directive pour accéder à Slider

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed = 5f;
    [SerializeField] private float baseAttackSpeed = 1f;
    [SerializeField] private PlayerHealthManager PHM ;

    // Méthode pour régénérer la santé au maximum et mettre à jour le slider
    public void RegenerateHealth()
    {
        PHM.currentHealth = PHM.maxHealth; // Remet la santé à son maximum

        // Met à jour le slider de santé pour qu'il affiche la santé complète
        if (PHM.Healthslider != null)
        {
            PHM.Healthslider.value = PHM.currentHealth;
        }

        Debug.Log("Santé régénérée au maximum!");
    }

    public void IncreasePlayerSpeed()
    {
        basePlayerSpeed *= 1.1f;
        FindObjectOfType<PlayerMovement>().SetSpeed(basePlayerSpeed);
    }

    public void IncreaseAttackSpeed()
    {
        baseAttackSpeed /= 1.3f;
        FindObjectOfType<WhipWapean>().SetAttackSpeed(baseAttackSpeed);
    }
}