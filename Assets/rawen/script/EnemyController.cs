using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Référence statique partagée par tous les ennemis
    public GameObject deathEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || collision.CompareTag("FireBall")) 
        {
            
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            AudioManager.PlayEnemyDeathSound();  // Play death sound

            Destroy(gameObject);
        }
    }
}