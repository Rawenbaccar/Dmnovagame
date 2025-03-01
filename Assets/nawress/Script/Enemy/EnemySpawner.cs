using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region private variables
    //=enemy manger
    [SerializeField] private EnemyTypes enemyTypes;  // Reference to our enemy types
    [SerializeField] private float spawnInterval = 3f;  // Time between spawns
    [SerializeField] private Vector2 spawnArea = new Vector2(10f, 10f);  // Spawn area size
    private float nextSpawnTime;
    private float gameTime;  // Track how long the game has been running
    #endregion

    #region Unity Callbacks
    void Start()
    {
        // Add validation
        if (enemyTypes == null)
        {
            Debug.LogError("EnemyTypes not assigned to EnemySpawner!");
            enabled = false; // Disable this component if enemyTypes is missing
            return;
        }

        if (enemyTypes.enemyTypes == null || enemyTypes.enemyTypes.Length == 0)
        {
            Debug.LogError("No enemy types configured in EnemyTypes!");
            enabled = false;
            return;
        }

        Init();
    }

    void Update()
    {
        gameTime += Time.deltaTime;  // Keep track of game time
        CheckSpawn();
    }
    #endregion

    #region private functions
    private void Init()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void CheckSpawn()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    public void SpawnEnemy()
    {
        // Get random position
        float randomX = Random.Range(-spawnArea.x, spawnArea.x);
        float randomY = Random.Range(-spawnArea.y, spawnArea.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // Choose which enemy to spawn
        GameObject enemyToSpawn = ChooseEnemyType();
        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            Debug.Log($"Spawned enemy: {enemyToSpawn.name} at position: {spawnPosition}");
        }
        else
        {
            Debug.LogWarning("No valid enemy type to spawn!");
        }
    }

    private GameObject ChooseEnemyType()
    {
        if (enemyTypes == null || enemyTypes.enemyTypes == null)
        {
            Debug.LogError("EnemyTypes not properly configured!");
            return null;
        }

        float totalWeight = 0;

        // Calculate total weight of available enemies
        foreach (var enemyType in enemyTypes.enemyTypes)
        {
            if (gameTime >= enemyType.minSpawnTime)
            {
                totalWeight += enemyType.spawnWeight;
            }
        }

        if (totalWeight <= 0)
        {
            Debug.LogWarning("No enemies available to spawn yet!");
            return null;
        }

        // Get a random value between 0 and total weight
        float randomValue = Random.Range(0, totalWeight);
        float currentWeight = 0;

        // Choose enemy based on weights
        foreach (var enemyType in enemyTypes.enemyTypes)
        {
            if (gameTime >= enemyType.minSpawnTime)
            {
                currentWeight += enemyType.spawnWeight;
                if (randomValue <= currentWeight)
                {
                    return enemyType.enemyPrefab;
                }
            }
        }

        return null;
    }
    #endregion
}