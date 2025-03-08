using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorroEnemy : MonoBehaviour
{
    public GameObject acidPuddlePrefab; // Assign the AcidPuddle prefab in Unity
    public Transform spawnPoint; // The position where the acid appears


    public void Die()
    {
        // Spawn the acid puddle at the enemy's position
        GameObject acid = Instantiate(acidPuddlePrefab, spawnPoint.position, Quaternion.identity);

        // Destroy the acid puddle after 5 seconds
        acid.GetComponent<AcidPuddle>().DestroyAfterTime(5f);

        // Destroy the enemy
        Destroy(gameObject);
    }
}
