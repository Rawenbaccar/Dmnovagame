using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public float damagePerSecond = 4f;  // Damage applied per second
    private bool playerInside = false;  // To track if the player is inside the puddle
    private PlayerHealthManager playerHealth;  // Reference to the player's health manager

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // When player steps on the acid puddle
        {
            playerInside = true;

            // Now we use GetComponentInChildren to search for the PlayerHealthManager on the child objects
            playerHealth = other.GetComponentInChildren<PlayerHealthManager>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond);  // Apply damage immediately
                StartCoroutine(DamagePlayer());  // Start damaging the player
            }
            else
            {
                // Debug message if PlayerHealthManager is not found on the player object or its children
                Debug.LogWarning("PlayerHealthManager not found on the player or its children!");
            }
        }
    }


 // call when player leave acide 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            playerInside = false;  // Stop damaging the player
        }
    }

    private System.Collections.IEnumerator DamagePlayer()
    {
        // Keep damaging the player as long as they are inside the puddle
        while (playerInside && playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond);  // Apply damage
            yield return new WaitForSeconds(1f);  // Wait for 1 second before applying damage again
        }
    }

    public void DestroyAfterTime(float time = 10f)
    {
        Destroy(gameObject, time);  // Destroy the puddle after the set time
    }
}
