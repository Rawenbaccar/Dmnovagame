using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1"))
        {
            // Détruit l'objet ennemi
            Destroy(gameObject);
        }
    }
}