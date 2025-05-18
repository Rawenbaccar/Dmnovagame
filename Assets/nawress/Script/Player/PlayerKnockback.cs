using UnityEngine;
using System.Collections;

public class PlayerKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackDistance = 3f;
    public float knockbackDuration = 0.2f;
    public float knockbackForce = 10f;
    
    private bool isKnockedBack = false;
    private Rigidbody2D rb;
    private FlowerBarrier flowerBarrier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flowerBarrier = FindObjectOfType<FlowerBarrier>();
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        if (!isKnockedBack && rb != null)
        {
            StartCoroutine(PerformKnockback(sourcePosition));
        }
    }

    private IEnumerator PerformKnockback(Vector2 sourcePosition)
    {
        isKnockedBack = true;
        
        // Calculate knockback direction (away from the flower)
        Vector2 knockbackDirection = ((Vector2)transform.position - sourcePosition).normalized;
        Vector2 startPosition = transform.position;

        // Calculate target position while respecting the barrier
        Vector2 targetPosition = startPosition + knockbackDirection * knockbackDistance;
        
        if (flowerBarrier != null)
        {
            // Get the barrier center and radius
            Vector2 barrierCenter = flowerBarrier.transform.position;
            float barrierRadius = flowerBarrier.radius;
            
            // Calculate distance from target position to barrier center
            float distanceToCenter = Vector2.Distance(targetPosition, barrierCenter);
            
            // If target position would be outside barrier, clamp it
            if (distanceToCenter > barrierRadius)
            {
                Vector2 directionToCenter = (targetPosition - barrierCenter).normalized;
                targetPosition = barrierCenter + directionToCenter * (barrierRadius - 0.5f); // Subtract small offset to ensure inside
            }
        }

        // Apply immediate force
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we reach the exact target position
        transform.position = targetPosition;
        rb.velocity = Vector2.zero;
        
        isKnockedBack = false;
    }
} 