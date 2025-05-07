using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Animator bossAnimator; // Reference to the boss's Animator
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private float attackTimer; // Timer for tracking attack cooldown

    void Start()
    {
        attackTimer = 10f; // Set the timer to 10 seconds
    }

    void Update()
    {
        // Decrease the attack timer every frame
        attackTimer -= Time.deltaTime;

        // When the timer reaches 0 or below, perform the attack
        if (attackTimer <= 0f)
        {
            PerformAttack();
            attackTimer = 10f; // Reset the timer to 10 seconds
        }
    }

    void PerformAttack()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Attack"); // This will trigger the attack animation

            // Freeze the player's movement for 1 second
            if (playerMovement != null)
            {
                playerMovement.FreezeMovement(1f); // Freeze for 1 second
            }
        }
        else
        {
            Debug.LogError("Animator is not assigned!"); // This will show an error if Animator is missing
        }
    }
}
