using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    private Transform parentHorde; // Reference to the horde parent
    
    void Start()
    {
        // Store the parent horde reference
        parentHorde = transform.parent;
    }

    void Update()
    {
        // Check if we have a parent horde
        if (parentHorde != null)
        {
            // Count how many bats are left in the horde (excluding this one)
            int remainingBats = parentHorde.childCount - 1;
            
            // If this is the last bat in the horde
            if (remainingBats <= 0)
            {
                // Destroy the parent horde game object
                Destroy(parentHorde.gameObject);
                // Destroy this bat
                Destroy(gameObject);
                
                Debug.Log("Horde eliminated!");
            }
        }
    }

    void OnDestroy()
    {
        // If this bat is destroyed and it was the last one, ensure the horde is cleaned up
        if (parentHorde != null && parentHorde.childCount <= 1)
        {
            Destroy(parentHorde.gameObject);
        }
    }
} 