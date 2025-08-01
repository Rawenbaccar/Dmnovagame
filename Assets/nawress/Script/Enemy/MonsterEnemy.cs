using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    [Header("Health")]
    public float baseMaxHealth = 5f;
    public float currentHealth;
    public float maxHealth;

    [Header("Combat")]
    public GameObject acidPuddlePrefab; // Acid puddle prefab
    public Transform spawnPoint; // Position to spawn the acid puddle
    public float attackRange = 2f; // Range to trigger the attack animation
    public float attackCooldown = 2f; // Time before the monster can attack again
    private float nextAttackTime = 0f; // To keep track of cooldown
    public Animator animator; // Animator to control animations

    [Header("Movement")]
    private GameObject player; // Reference to the player
    public float movementSpeed = 2f; // Monster movement speed

    void Start()
    { // trouver le joueur par son tag 
        player = GameObject.FindWithTag("Player"); 

        // Initialize health with scaling
        if (EnemyHealthManager.Instance != null)
        {
            maxHealth = baseMaxHealth * EnemyHealthManager.Instance.GetHealthMultiplier();
        }
        else
        {
            maxHealth = baseMaxHealth;
        }
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        // Calcule la distance player et enemy 
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Move the monster near the player (if not attacking)
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
            if (animator != null)
            {
                animator.SetBool("isWalking", true); // Set walking animation
            }
        }
        //Si le joueur est proche, il arrête l’animation de marche
        else
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false); // Stop walking animation
            }

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
        // calculate the direction to move the monster towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        // Trigger the attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void TakeDamage(float damage, HorroEnemy horroEnemy, GameObject diamond)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (horroEnemy != null)
            {
                // Call the Die() function, which will spawn the acid puddle
                horroEnemy.Die();
            }
            if (diamond != null)
            {
                GameObject SpawnedMonster = Instantiate(diamond, transform.position, Quaternion.identity);
                SpawnedMonster.tag = "Diamond";
            }
            Die();
        }
    }

    public void Die()
    {
        // Spawn the acid puddle at the enemy's position
        if (acidPuddlePrefab != null && spawnPoint != null)
        {
            GameObject acid = Instantiate(acidPuddlePrefab, spawnPoint.position, Quaternion.identity);
            acid.GetComponent<AcidPuddle>().DestroyAfterTime(5f);
        }

        // Destroy the enemy
        Destroy(gameObject);
    }
}
