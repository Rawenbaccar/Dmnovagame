using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    #region private variable 
    [SerializeField] private GameObject enemyPrefab;    // Drag your enemy prefab here in the Inspector
    [SerializeField] private float spawnInterval = 3f;  // Time between spawns in seconds
    [SerializeField] private Vector2 spawnArea = new Vector2(10f, 10f);  // Area where enemies can spawn
    private float nextSpawnTime;
    #endregion



    #region Unity CallBack 
    void Start()
    {
        Init();
    }
    void Update()
    {
        CheckSpawn();
    }
    #endregion



    #region private fuctions
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

    private void SpawnEnemy()
    {
        // Random position within the spawn area
        float randomX = Random.Range(-spawnArea.x, spawnArea.x);
        float randomY = Random.Range(-spawnArea.y, spawnArea.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    #endregion 
}