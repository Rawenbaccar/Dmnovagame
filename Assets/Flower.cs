using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject flowerCircle; // Assign the flower group (parent object) in Inspector

    void Start()
    {
        if (flowerCircle != null)
        {
            flowerCircle.SetActive(true); // Make the circle appear
            flowerCircle.transform.position = transform.position; // Center it on the player
        }
    }
}
