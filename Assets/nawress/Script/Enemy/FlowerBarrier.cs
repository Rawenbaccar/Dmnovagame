using UnityEngine;

public class FlowerBarrier : MonoBehaviour
{
    public static FlowerBarrier Instance;
    public float radius = 5f;
    private Transform player;
    private Rigidbody2D playerRb;
    private bool wasInside = false;
    private PlayerKnockback playerKnockback;

    void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerKnockback = player.GetComponent<PlayerKnockback>();
    }

    void FixedUpdate()
    {
        if (player == null || playerRb == null) return;

        Vector2 directionToPlayer = (Vector2)(player.position - transform.position);
        float distanceToPlayer = directionToPlayer.magnitude;

        // If player is inside the circle
        if (distanceToPlayer <= radius)
        {
            wasInside = true;
        }
        // If player is outside the barrier
        else
        {
            // Calculate the closest point on the circle's edge
            Vector2 closestPoint = (Vector2)transform.position + (directionToPlayer.normalized * (radius - 0.5f));
            
            // Force the player back inside
            player.position = closestPoint;
            playerRb.velocity = Vector2.zero;

            // If player was previously inside, they hit the barrier
            if (wasInside && playerKnockback != null)
            {
                // Apply a small knockback inward
                Vector2 inwardDirection = -directionToPlayer.normalized;
                playerKnockback.ApplyKnockback(player.position + (Vector3)(inwardDirection * -1));
            }
        }
    }

    public void DestroyBarrier()
    {
        Debug.Log("Barrier destroyed! Player can escape.");
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
