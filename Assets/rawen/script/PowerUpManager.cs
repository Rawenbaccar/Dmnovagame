using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private PlayerHealthManager playerHealthManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private FireballOrbit fireball;
    [SerializeField] private ThunderStrike thunderStrike;
    [SerializeField] private KnifeThrow knifeThrow; // Ajout de la référence à KnifeThrow
    [SerializeField] private WhipUpgrade whipUpgrade; // Référence au fouet
    [SerializeField] private float knifePowerUpDuration = 10f; // Durée du Power-Up en secondes
    [SerializeField] private GameObject laserPrefab; // Préfab du laser
    [SerializeField] private Transform playerTransform; // Référence au joueur
    [SerializeField] private GameObject currentWhip; // Le fouet actuellement équipé
    [SerializeField] private GameObject newWhipPrefab; // Le prefab du nouveau fouet
    [SerializeField] private Transform whipSpawnPoint; // Position où le fouet sera instancié
    [SerializeField] private GameObject freezeEffectPrefab; // Préfab de l'effet de glace à appliquer
    [SerializeField] private float freezeDuration = 5f; // Durée du freeze




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
            fireball.ActivateFireball();
        }
        powerUpPanel.SetActive(false);
    }

    public void ApplyMagnetPowerUp()
    {
        FindObjectOfType<MagnetEffect>().ActivateMagnet();
        powerUpPanel.SetActive(false);
    }

    public void ApplyThunderStrikePowerUp()
    {
        if (thunderStrike != null)
        {
            thunderStrike.ActivateThunder();
        }
        powerUpPanel.SetActive(false);
    }

    public void ApplyHealthPowerUp()
    {
        playerStats.RegenerateHealth();
        powerUpPanel.SetActive(false);
    }

    // **Nouveau Power-Up : Activation du lancer automatique de couteaux**
    public void ApplyKnifeThrowPowerUp()
    {
        if (knifeThrow != null)
        {
            StartCoroutine(ActivateKnifeThrowTemporarily());
        }
        powerUpPanel.SetActive(false);
    }

    private IEnumerator ActivateKnifeThrowTemporarily()
    {
        knifeThrow.ActivateAutoThrow(); // Active le lancer automatique
        yield return new WaitForSeconds(knifePowerUpDuration);
        // knifeThrow.DeactivateAutoThrow(); // Désactive après la durée définie
    }
    public void ApplyFreezePowerUp()
    {
        StartCoroutine(FreezeEnemiesTemporarily());
        powerUpPanel.SetActive(false);
    }

    private IEnumerator FreezeEnemiesTemporarily()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.TryGetComponent<EnemyMouvement>(out EnemyMouvement movementScript))
            {
                movementScript.enabled = false; // Désactive le mouvement des ennemis
            }

            if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // Fige le Rigidbody
            }

            // Ajoutez l'effet de glace ici
            if (freezeEffectPrefab != null)
            {
                GameObject freezeEffect = Instantiate(freezeEffectPrefab, enemy.transform.position, Quaternion.identity);
                freezeEffect.transform.SetParent(enemy.transform); // L'effet suit l'ennemi
            }
        }

        yield return new WaitForSeconds(freezeDuration); // Durée du freeze

        foreach (GameObject enemy in enemies)
        {
            if (enemy.TryGetComponent<EnemyMouvement>(out EnemyMouvement movementScript))
            {
                movementScript.enabled = true; // Réactive le mouvement
            }

            if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.constraints = RigidbodyConstraints2D.None; // Libère les contraintes
            }

            // Enlevez l'effet de glace après le freeze
            if (enemy.transform.childCount > 0)
            {
                foreach (Transform child in enemy.transform)
                {
                    if (child.CompareTag("FreezeEffect")) // Si l'enfant a ce tag
                    {
                        Destroy(child.gameObject); // Enlevez l'effet de glace
                    }
                }
            }
        }
    }
        public void ApplyWhipUpgradePowerUp()
    {
        if (whipUpgrade != null)
        {
            whipUpgrade.UpgradeWhip();
        }
        powerUpPanel.SetActive(false);
    }
    public void ApplyLaserPowerUp()
    {
        GameObject laserInstance = Instantiate(laserPrefab, playerTransform.position, Quaternion.identity);
        LaserAttack laser = laserInstance.GetComponent<LaserAttack>();

        if (laser != null)
        {
            laser.ActivateLaser();
        }

        powerUpPanel.SetActive(false);
    }
    public void ApplyNewWhipPowerUp()
    {
        if (currentWhip != null)
        {
            Destroy(currentWhip); // Supprime l'ancien fouet
        }

        if (newWhipPrefab != null && playerTransform != null)
        {
            GameObject newWhip = Instantiate(newWhipPrefab, playerTransform.position, Quaternion.identity);
            newWhip.transform.SetParent(playerTransform); // Attache le fouet au joueur
            currentWhip = newWhip; // Mettre à jour la référence du fouet actuel
        }

        powerUpPanel.SetActive(false);
    }
   
}




