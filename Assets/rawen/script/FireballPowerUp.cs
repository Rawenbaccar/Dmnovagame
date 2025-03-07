using UnityEngine;

[CreateAssetMenu(fileName = "NewFireballPowerUp", menuName = "PowerUps/Fireball")]
public class FireballPowerUp : ScriptableObject
{
    public float orbitSpeed = 50f;  // Vitesse de rotation
    public float distanceFromPlayer = 2f;  // Distance du joueur
    public string fireBallname;
    public Vector3 position;
    public Sprite image;
    

    
}
