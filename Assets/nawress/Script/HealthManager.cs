using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Transform player; // R�f�rence vers le joueur
    public Vector3 offset = new Vector3(0, 1.5f, 0); // D�calage au-dessus du joueur

    void Update()
    {
        // Met � jour la position de la barre de sant� pour suivre le joueur
        if (player != null)
        {
            //transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        }
    }
}
