using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    [Header("Health")]
    public float baseMaxHealth = 5f;
    private float currentHealth;
    private float maxHealth;

    [Header("Combat")]
    public GameObject acidPuddlePrefab;
    public Transform spawnPoint;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;
    public Animator animator;

    [Header("Movement")]
    private GameObject player;
    public float movementSpeed = 2f;

    void Start()
    {
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

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (acidPuddlePrefab != null && spawnPoint != null)
        {
            GameObject acid = Instantiate(acidPuddlePrefab, spawnPoint.position, Quaternion.identity);
            acid.GetComponent<AcidPuddle>().DestroyAfterTime(5f);
        }
        Destroy(gameObject);
    }
}
