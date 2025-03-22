using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Animator bossAnimator; // Reference to the boss's Animator
    public Transform player; // The player's position
    public float moveSpeed = 1f; // Boss movement speed
    public float teleportCooldown = 5f; // Time between each teleport
    public GameObject clonePrefab; // The clone prefab
    private float teleportTimer; // Timer to track teleport cooldown
    public float animationCooldown = 5f; // Temps entre chaque animation (change à 8f si tu veux)
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

        // Check if the "D" key is pressed to simulate boss death
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Boss has died!"); // Simulate boss death
            return; // Exit the Update method if the boss is dead
        }

        // Decrease animation cooldown timer
        animationTimer -= Time.deltaTime;

        // Check animation cooldown value
        Debug.Log("Animation Timer: " + animationTimer);

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
            Debug.Log("Played special animation");
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
        int numClones = Random.Range(2, 5); // Spawn 2 to 5 clones

        for (int i = 0; i < numClones; i++)
        {
            // Pick a random position near the boss
            Vector2 clonePosition = new Vector2(
                transform.position.x + Random.Range(-2f, 2f),
                transform.position.y + Random.Range(-2f, 2f)
            );

            // Instantiate a clone
            Instantiate(clonePrefab, clonePosition, Quaternion.identity);
        }
    }
}
