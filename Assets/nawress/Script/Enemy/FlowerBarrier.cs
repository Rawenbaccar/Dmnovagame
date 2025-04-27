using UnityEngine;

public class FlowerBarrier : MonoBehaviour
{
    public static FlowerBarrier Instance; // Singleton to access it easily

    void Awake()
    {
        Instance = this; // Save the instance
    }

    public void DestroyBarrier()
    {
        Debug.Log("Barrier destroyed! Player can escape.");
        Destroy(gameObject); // Remove the barrier
    }
}
