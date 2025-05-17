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
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || collision.CompareTag("knife"))
        {
            // Check if the enemy has a MonsterEnemy script attached to it
            MonsterEnemy monsterEnemy = GetComponent<MonsterEnemy>();
            HorroEnemy horroEnemy = GetComponent<HorroEnemy>();

            if (monsterEnemy != null)
            {
                // Call the Die() function, which will spawn the acid puddle


                monsterEnemy.TakeDamage(1, horroEnemy, deathEffectPrefab);

            }

        }
    }

}