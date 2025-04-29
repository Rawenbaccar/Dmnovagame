using UnityEngine;

public class XpPickUp : MonoBehaviour
{
    private PowerUpManager powerUpManager; // Reference to PowerUpManager

    void Start()
    {
        // Get the PowerUpManager reference (assumes it's in the same scene)
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Freeze"))
        {
            // Call freeze power-up function and destroy the freeze prefab
            powerUpManager.ApplyFreezePowerUp();
            Destroy(collision.gameObject); // Destroy the freezing prefab
        }
        else if (collision.CompareTag("Magnet"))
        {
            // Call magnet power-up function and destroy the magnet prefab
            powerUpManager.ApplyMagnetPowerUp();
            Destroy(collision.gameObject); // Destroy the magnet prefab
        }
        else if (collision.CompareTag("Health"))
        {
            // Call health power-up function and destroy the health prefab
            powerUpManager.ApplyHealthPowerUp();
            Destroy(collision.gameObject); // Destroy the health prefab
        }
    }

}
