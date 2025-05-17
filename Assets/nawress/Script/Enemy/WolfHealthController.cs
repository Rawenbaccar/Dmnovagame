using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Nécessaire pour utiliser les coroutines

public class WolfHealthController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float knockbackDistance = 1f; // Distance du recul
    [SerializeField] private float knockbackDuration = 0.2f; // Durée du recul

    private bool isKnockedBack = false; // Empêche les interruptions de recul

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Appliquer le recul
        if (!isKnockedBack)
        {
            StartCoroutine(Knockback(collision.transform));
        }
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