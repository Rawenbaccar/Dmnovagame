using UnityEngine;

public class SwarmBatSpawner : MonoBehaviour
{
    public GameObject swarmBatPrefab;  // Prefab for a single bat
    public int swarmSize = 40;         // Total number of bats
    public float spawnRadius = 2f;     // Tight cluster formation
    public Vector2 swarmDirection = new Vector2(-1f, 0f); // Move left (adjust if needed)

    void Start()
    {
        SpawnSwarmWave(); // Spawn the swarm once
    }

    void SpawnSwarmWave()
    {
        Vector2 centerPosition = (Vector2)transform.position; // Spawn point

        for (int i = 0; i < swarmSize; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPos = centerPosition + randomOffset;

            GameObject bat = Instantiate(swarmBatPrefab, spawnPos, Quaternion.identity);

            // Make all bats move in the same direction like a wave
            bat.GetComponent<Rigidbody2D>().velocity = swarmDirection.normalized * 2f;
        }
    }
}
