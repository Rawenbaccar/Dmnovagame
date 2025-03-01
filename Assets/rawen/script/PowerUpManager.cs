using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private FireballOrbit fireball; // Référence à l'objet Fireball dans la scène

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
}
