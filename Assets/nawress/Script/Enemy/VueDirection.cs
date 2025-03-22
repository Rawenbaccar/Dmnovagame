using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VueDirection : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // Check player's position relative to the enemy
            if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false; // Face left (default)
            }
            else
            {
                spriteRenderer.flipX = true; // Face right
            }
        }
    }
}
