using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Nécessaire pour utiliser les coroutines

public class WolfHealthController : MonoBehaviour
{
    public static GameObject deathEffectPrefab;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float knockbackDistance = 1f; // Distance du recul
    [SerializeField] private float knockbackDuration = 0.2f; // Durée du recul

    private bool isKnockedBack = false; // Empêche les interruptions de recul

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (deathEffectPrefab == null)
        {
            deathEffectPrefab = Resources.Load<GameObject>("sprite");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WhipAttack") || collision.CompareTag("WhipAttack1") || collision.CompareTag("FireBall") || collision.CompareTag("WhipUpgrade") || collision.CompareTag("knife"))
        {
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            }

            TakeDamage(1);

            // Appliquer le recul
            if (!isKnockedBack)
            {
                StartCoroutine(Knockback(collision.transform));
            }
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthSlider();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("Health Slider is not assigned!");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Knockback(Transform attacker)
    {
        isKnockedBack = true; // Bloque les interruptions de recul
        Vector2 knockbackDirection = (transform.position - attacker.position).normalized; // Direction opposée à l'attaque
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + knockbackDirection * knockbackDistance; // Calcul de la position finale

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Assure d'atteindre la destination exacte
        isKnockedBack = false; // Débloque le mouvement normal
    }
}