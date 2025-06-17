using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Animator bossAnimator; // Reference to the boss's Animator
    public Transform player; // The player's position
    public float moveSpeed = 1f; // Boss movement speed
    public float teleportCooldown = 10f; // Time between each teleport
    public GameObject clonePrefab; // The clone prefab
    private float teleportTimer; // Timer to track teleport cooldown
    public float animationCooldown = 5f; // Temps entre chaque animation
    private float animationTimer; // Timer pour suivre le temps

    void Start()
    {
        teleportTimer = teleportCooldown;
        animationTimer = animationCooldown; // Initialise le timer pour l'animation

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Move the boss toward the player
        MoveTowardPlayer();

        // Handle teleportation logic
        HandleTeleportation();

        // Decrease animation cooldown timer
        animationTimer -= Time.deltaTime;

        // Check animation cooldown value
        //Debug.Log("Animation Timer: " + animationTimer);

        // Check if it's time to play the animation
        if (animationTimer <= 0f)
        {
            PlayBossAnimation();
            animationTimer = animationCooldown; // Reset the animation timer
        }
    }

    void PlayBossAnimation()
    {
        // Trigger some other boss animation (like a special move or attack effect)
        if (player != null && player.GetComponent<Animator>() != null)
        {
            player.GetComponent<Animator>().SetTrigger("SpecialMove"); // Replace "SpecialMove" with your actual animation
            
        }
    }

    void MoveTowardPlayer()
    {
        if (player == null) return; // If no player, do nothing

        // Move the boss toward the player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    void HandleTeleportation()
    {
        teleportTimer -= Time.deltaTime; // Countdown the teleport timer

        if (teleportTimer <= 0f) // Time to teleport!
        {
            Teleport();
            teleportTimer = teleportCooldown; // Reset the teleport timer
        }
    }
// clones apparition
    void Teleport()
    {
        // Create shadow clones before teleporting
        SpawnClones();

        // Pick a random teleport location
        Vector2 randomPosition = new Vector2(
            Random.Range(-7f, 7f), // Adjust based on your map size
            Random.Range(-4f, 4f)
        );

        transform.position = randomPosition; // Move boss

        // Optional teleport effect
        Debug.Log("Boss Teleported!");
    }

    void SpawnClones()
    {
        // Spawn 3 clones fixed near the boss
        Vector2 offset = new Vector2(1f, 0f); // Adjust the distance between clones (adjust as needed)

        // Ensure the boss position is treated as a Vector2 for subtraction
        Vector2 bossPosition = new Vector2(transform.position.x, transform.position.y);

        Vector2 clonePosition1 = bossPosition + offset;
        Vector2 clonePosition2 = bossPosition - offset;
        Vector2 clonePosition3 = bossPosition + new Vector2(0f, 1f); // Add another clone a bit higher

        // Instantiate the clones
        GameObject clone1 = Instantiate(clonePrefab, clonePosition1, Quaternion.identity);
        GameObject clone2 = Instantiate(clonePrefab, clonePosition2, Quaternion.identity);
        GameObject clone3 = Instantiate(clonePrefab, clonePosition3, Quaternion.identity);

        // Make sure the clones don't fall by disabling gravity
        Rigidbody2D rb1 = clone1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = clone2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = clone3.GetComponent<Rigidbody2D>();

        if (rb1 != null)
            rb1.gravityScale = 0; // Disable gravity on the clone
        if (rb2 != null)
            rb2.gravityScale = 0; // Disable gravity on the clone
        if (rb3 != null)
            rb3.gravityScale = 0; // Disable gravity on the clone

        // Debug to confirm clones are spawned
        Debug.Log("Clones Spawned Near Boss!");
    }



    public void DisableBossAI()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Dead"); // Play the death animation
        }

        this.enabled = false; // Completely disable BossAI script
    }
}
