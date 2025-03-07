using UnityEngine;

public class EnemyControllerr : MonoBehaviour
{
    // Référence statique partagée par tous les ennemis

    public static GameObject deathEffectPrefab;

    private void Start()
    {
        // Charge le préfab une seule fois si ce n'est pas déjà fait
        if (deathEffectPrefab == null)
        {
            deathEffectPrefab = Resources.Load<GameObject>("sprite");  // Changement du chemin

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1"))
        {
            if (deathEffectPrefab != null)
            {
                GameObject instance = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
                instance.tag = "Diamond";
            }
            Destroy(gameObject);
        }
    }
}