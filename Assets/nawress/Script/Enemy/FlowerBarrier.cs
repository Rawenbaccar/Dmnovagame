using UnityEngine;

public class FlowerBarrier : MonoBehaviour
{
    public static FlowerBarrier Instance;
    public float radius = 5f;
    private Transform player;
    private Rigidbody2D playerRb;
    private bool wasInside = false;

    void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
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
        // If player was previously inside and is now outside
        else if (wasInside)
        {
            // Calculate the closest point on the circle's edge
            Vector2 closestPoint = (Vector2)transform.position + (directionToPlayer.normalized * radius);
            
            // Force the player back to the circle's edge
            playerRb.MovePosition(closestPoint);
            
            // Zero out the velocity to prevent bouncing
            playerRb.velocity = Vector2.zero;
        }
        // If player is outside but hasn't been inside yet
        else
        {
            // Allow movement towards the circle
            Vector2 towardsCenter = ((Vector2)transform.position - (Vector2)player.position).normalized;
            float movingTowardsCenter = Vector2.Dot(playerRb.velocity, towardsCenter);

            if (movingTowardsCenter < 0)
            {
                // Stop movement away from circle
                playerRb.velocity = Vector2.zero;
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
