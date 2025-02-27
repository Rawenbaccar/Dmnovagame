using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private FireballOrbit fireball; // R�f�rence � l'objet Fireball dans la sc�ne

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
