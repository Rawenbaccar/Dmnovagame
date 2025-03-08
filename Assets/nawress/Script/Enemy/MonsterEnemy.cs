using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    public GameObject acidPuddlePrefab; // Acid puddle prefab
    public Transform spawnPoint; // Position to spawn the acid puddle
    public float attackRange = 2f; // Range to trigger the attack animation
    public float attackCooldown = 2f; // Time before the monster can attack again
    private float nextAttackTime = 0f; // To keep track of cooldown
    public Animator animator; // Animator to control animations

    private GameObject player; // Reference to the player

    public float movementSpeed = 2f; // Monster movement speed

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Assuming your player has the "Player" tag
    }

    void Update()
    {
        // Check the distance between the monster and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Move the monster towards the player (if not attacking)
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
            animator.SetBool("isWalking", true); // Set walking animation
        }
        else
        {
            animator.SetBool("isWalking", false); // Stop walking animation
        }

        // Attack if in range and cooldown has passed
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Reset attack cooldown
        }
    }

    void MoveTowardsPlayer()
    {
        // Move the monster towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

    }

    public void Die()
    {
        // Spawn the acid puddle at the enemy's position
        GameObject acid = Instantiate(acidPuddlePrefab, spawnPoint.position, Quaternion.identity);

        // Destroy the acid puddle after 5 seconds
        acid.GetComponent<AcidPuddle>().DestroyAfterTime(5f);

        // Destroy the enemy
        Destroy(gameObject);
    }
}
