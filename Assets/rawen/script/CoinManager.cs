using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinsPerLevel = 3;
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f); // zone de spawn
    public float minDistanceBetweenCoins = 2f; // distance minimale entre deux pièces

    private List<GameObject> spawnedCoins = new List<GameObject>();
    private List<Vector3> coinPositions = new List<Vector3>();

    public void SpawnCoins()
    {
        ClearCoins();
        coinPositions.Clear();

        int attempts = 0;
        int maxAttempts = 100;

       while (spawnedCoins.Count < coinsPerLevel && attempts < maxAttempts)
        {
            attempts++;

            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0f, // sur le sol (Y=0)
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Vérifie la distance avec les autres coins déjà placés
            bool isFarEnough = true;
            foreach (Vector3 pos in coinPositions)
            {
                if (Vector3.Distance(randomPos, pos) < minDistanceBetweenCoins)
                {
                    isFarEnough = false;
                    break;
                }
            }

            if (isFarEnough)
            {
                GameObject coin = Instantiate(coinPrefab, randomPos, Quaternion.identity);
                spawnedCoins.Add(coin);
                coinPositions.Add(randomPos);
            }
        }

        if (spawnedCoins.Count < coinsPerLevel)
        {
            Debug.LogWarning("Pas assez d’espace pour spawn toutes les pièces sans collision !");
        }
    }

    public void ClearCoins()
    {
        foreach (var coin in spawnedCoins)
        {
            if (coin != null)
                Destroy(coin);
        }
        spawnedCoins.Clear();
    }
}




