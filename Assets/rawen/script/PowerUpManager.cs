using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private PlayerHealthManager playerHealthManager; // R�f�rence � PlayerHealthManager
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private FireballOrbit fireball; // R�f�rence � l'objet Fireball dans la sc�ne
    [SerializeField] private ThunderStrike thunderStrike; // R�f�rence au script ThunderStrike

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

    // M�thode pour appliquer le Power-Up de r�g�n�ration de la sant�
    public void ApplyHealthPowerUp()
    {
        playerStats.RegenerateHealth(); // R�g�n�re la sant� au maximum
        powerUpPanel.SetActive(false);
    }
}