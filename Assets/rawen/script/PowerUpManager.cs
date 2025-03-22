using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private PlayerHealthManager playerHealthManager; // Référence à PlayerHealthManager
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private FireballOrbit fireball; // Référence à l'objet Fireball dans la scène
    [SerializeField] private ThunderStrike thunderStrike; // Référence au script ThunderStrike

    public void ApplyPlayerSpeedPowerUp()
    {
        playerStats.IncreasePlayerSpeed();
        powerUpPanel.SetActive(false);
    }

    public void ApplyAttackSpeedPowerUp()
    {
        playerStats.IncreaseAttackSpeed();
        powerUpPanel.SetActive(false);
    }

    public void ApplyFireballPowerUp()
    {
        if (fireball != null)
        {
            fireball.ActivateFireball(); // Active la boule de feu
        }
        powerUpPanel.SetActive(false);
    }

    public void ApplyMagnetPowerUp()
    {
        FindObjectOfType<MagnetEffect>().ActivateMagnet(); // Active le pouvoir d'attraction
        powerUpPanel.SetActive(false);
    }

    public void ApplyThunderStrikePowerUp()
    {
        if (thunderStrike != null)
        {
            Debug.Log("escriptito yemchito willa lito ?");
            thunderStrike.ActivateThunder(); // Active la foudre
        }
        powerUpPanel.SetActive(false);
    }

    // Méthode pour appliquer le Power-Up de régénération de la santé
    public void ApplyHealthPowerUp()
    {
        playerStats.RegenerateHealth(); // Régénère la santé au maximum
        powerUpPanel.SetActive(false);
    }
}