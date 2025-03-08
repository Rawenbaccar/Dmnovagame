using UnityEngine;
using UnityEngine.UI;  // N'oublie pas d'ajouter cette directive pour acc�der � Slider

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed = 5f;
    [SerializeField] private float baseAttackSpeed = 1f;

    [SerializeField] private float maxHealth = 100f; // Sant� maximale
    private float currentHealth; // Sant� actuelle

    [SerializeField] private Slider healthSlider; // R�f�rence au Slider de la sant�

    private void Start()
    {
        currentHealth = maxHealth; // La sant� actuelle commence � la valeur maximale
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // D�finit la valeur maximale du slider
            healthSlider.value = currentHealth; // Initialise le slider avec la sant� actuelle
        }
    }

    // M�thode pour r�g�n�rer la sant� au maximum et mettre � jour le slider
    public void RegenerateHealth()
    {
        currentHealth = maxHealth; // Remet la sant� � son maximum

        // Met � jour le slider de sant� pour qu'il affiche la sant� compl�te
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        Debug.Log("Sant� r�g�n�r�e au maximum!");
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