using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    public float speed = 10f;   // Vitesse du couteau
    public int damage = 10;     // Dégâts infligés
    private Transform target;   // Cible du couteau
    public GameObject impactEffect;

    // Définir la cible du couteau
    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Détruire le couteau si l'ennemi est mort ou disparu
            return;
        }

        // Déplacement vers l'ennemi
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotation du couteau en direction de l'ennemi
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Collision avec un ennemi
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}