using UnityEngine;

public class EnemyTypes : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float spawnWeight = 1f; // Higher weight = more common
        public float minSpawnTime = 0f; // When this enemy starts appearing (in seconds)
    }

    public EnemyType[] enemyTypes;
}