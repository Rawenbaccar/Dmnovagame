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

        // au Début du jeu seul le premier ennemi est débloqué 
        Init();
        unlockedEnemies.Add(enemyTypes.enemyTypes[0].enemyPrefab);


        // Subscribe to level up event
        if (ExperienceLevelController.instance != null)
        {
            ExperienceLevelController.instance.OnLevelUp += OnLevelUp;
        }
        else
        {
            Debug.LogError("ExperienceLevelController instance not found!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        if (ExperienceLevelController.instance != null)
        {
            ExperienceLevelController.instance.OnLevelUp -= OnLevelUp;
        }
    }

    private void OnLevelUp(int newLevel)
    {
        currentLevel = newLevel;
        Debug.Log($"EnemySpawner: Level updated to {currentLevel}");
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
            if (hordePrefab != null && currentLevel >= 10 && Random.Range(0f, 1f) <= 1f) // Added level check
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

        // Check if we should spawn boss at level 8
        if (currentLevel == 8 && !hasBossSpawned)
        {
            Debug.Log($"Attempting to spawn boss at level 8. Current level: {currentLevel}, hasBossSpawned: {hasBossSpawned}");
            // Find the boss prefab
            foreach (var enemyType in enemyTypes.enemyTypes)
            {
                if (enemyType.isBoss)
                {
                    Debug.Log("Found boss prefab, spawning boss!");
                    Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
                    hasBossSpawned = true;
                    Debug.Log("Boss spawned at level 8!");
                    return;
                }
            }
            Debug.LogWarning("No boss prefab found in enemyTypes!");
        }

        // Choose which enemy to spawn
        GameObject enemyToSpawn = ChooseEnemyType();
        if (enemyToSpawn != null)
        {
            // Check if it's a boss enemy
            if (enemyToSpawn.CompareTag("Boss"))
            {
                if (!hasBossSpawned && currentLevel >= 8)
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
                // unclok le premier ennemi non debloque 
                unlockedEnemies.Add(enemyType.enemyPrefab);
                break; // for only one enmy
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
