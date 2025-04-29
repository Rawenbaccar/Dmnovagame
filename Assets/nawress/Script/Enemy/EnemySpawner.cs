using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region private variables
    [SerializeField] private EnemyTypes enemyTypes;  // Reference to our enemy types
    [SerializeField] private GameObject hordePrefab; // Reference to the horde prefab
    [SerializeField] private float spawnInterval = 3f;  // Time between spawns
    [SerializeField] private Vector2 spawnArea = new Vector2(10f, 10f);  // Spawn area size
    private float nextSpawnTime;
    private float gameTime;  // Track how long the game has been running
    private List<GameObject> unlockedEnemies = new List<GameObject>(); // Stores unlocked enemies
    private int currentLevel = 1; // Add this new variable to track the current level
    private bool hasBossSpawned = false; // Track if boss has been spawned

    #endregion

    #region Unity Callbacks
    void Start()
    {
        if (enemyTypes == null || enemyTypes.enemyTypes == null || enemyTypes.enemyTypes.Length == 0)
        {
            Debug.LogError("No enemy types configured in EnemyTypes!");
            enabled = false;
            return;
        }

        Init();
        unlockedEnemies.Add(enemyTypes.enemyTypes[0].enemyPrefab);
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
            if (hordePrefab != null && currentLevel >= 9 && Random.Range(0f, 1f) <= 1f) // Added level check
            {
                Debug.Log("is horde spawning  ?");
                SpawnHorde();
            }
            else
            {
                SpawnEnemy();
            }

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    public void SpawnEnemy()
    {
        // Get random position
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Choose which enemy to spawn
        GameObject enemyToSpawn = ChooseEnemyType();
        if (enemyToSpawn != null)
        {
            // Check if it's a boss enemy
            if (enemyToSpawn.CompareTag("Boss"))
            {
                if (!hasBossSpawned)
                {
                    Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    hasBossSpawned = true;
                    Debug.Log("Boss spawned!");
                }
                return;
            }
            
            // Spawn regular enemy
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No valid enemy type to spawn!");
        }
    }

    private void SpawnHorde()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(hordePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Horde Spawned!");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnArea.x, spawnArea.x);
        float randomY = Random.Range(-spawnArea.y, spawnArea.y);
        return new Vector3(randomX, randomY, 0f);
    }

    private GameObject ChooseEnemyType()
    {
        if (unlockedEnemies.Count == 0)
        {
            Debug.LogError("No enemies unlocked yet!");
            return null;
        }

        int randomIndex = Random.Range(0, unlockedEnemies.Count);
        return unlockedEnemies[randomIndex];
    }

    public void UnlockNewEnemy()
    {
        foreach (var enemyType in enemyTypes.enemyTypes)
        {
            if (!unlockedEnemies.Contains(enemyType.enemyPrefab)) // If not already unlocked
            {
                unlockedEnemies.Add(enemyType.enemyPrefab);
                break; // Unlock only one new enemy at a time
            }
        }
    }

    // Add this new public method to update the level
    public void SetLevel(int level)
    {
        currentLevel = level;
    }
    #endregion
}
