using System.Collections.Generic;
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
    private List<GameObject> unlockedEnemies = new List<GameObject>(); // Stores unlocked enemies

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
           
        }
        else
        {
            Debug.LogWarning("No valid enemy type to spawn!");
        }
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
                Debug.Log($"New enemy unlocked: {enemyType.enemyPrefab.name}");
                break; // Unlock only one new enemy at a time
            }
        }
    }
    #endregion
}