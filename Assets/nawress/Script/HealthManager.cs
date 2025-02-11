using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Transform player; // Référence vers le joueur
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Décalage au-dessus du joueur

    void Update()
    {
        // Met à jour la position de la barre de santé pour suivre le joueur
        if (player != null)
        {
            //transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
        }
    }
}
