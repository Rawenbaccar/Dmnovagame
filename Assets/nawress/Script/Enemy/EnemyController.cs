using UnityEngine;

public class EnemyControllerr : MonoBehaviour
{
    // Référence statique partagée par tous les ennemis
    public static GameObject deathEffectPrefab;
    private HealthController healthController;

    private void Start()
    {
        // Charge le préfab une seule fois si ce n'est pas déjà fait
        if (deathEffectPrefab == null)
        {
            deathEffectPrefab = Resources.Load<GameObject>("Prefabs/sprite");  // Mettez le bon chemin vers votre préfab
        }
        
        healthController = GetComponent<HealthController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1"))
        {
            if (healthController != null)
            {
                healthController.TakeDamage(1f); // Deal 1 damage per hit
            }
        }
    }
}
