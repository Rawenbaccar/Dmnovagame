using UnityEngine;
using UnityEngine.UI;  // N'oublie pas d'ajouter cette directive pour acc�der � Slider

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed = 5f;
    [SerializeField] private float baseAttackSpeed = 1f;
    [SerializeField] private PlayerHealthManager PHM ;

    // M�thode pour r�g�n�rer la sant� au maximum et mettre � jour le slider
    public void RegenerateHealth()
    {
        PHM.currentHealth = PHM.maxHealth; // Remet la sant� � son maximum

        // Met � jour le slider de sant� pour qu'il affiche la sant� compl�te
        if (PHM.Healthslider != null)
        {
            PHM.Healthslider.value = PHM.currentHealth;
        }

        Debug.Log("Sant� r�g�n�r�e au maximum!");
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