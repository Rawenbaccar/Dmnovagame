using System.Collections;
using UnityEngine;

public class CloneAI : MonoBehaviour
{
    public float moveSpeed = 4f; // Clone moves faster than the boss
    public float lifetime = 5f; // Clone disappears after 5 seconds
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player
        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }
}
