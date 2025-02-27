using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Référence au joueur
    public Vector3 offset;    // Décalage entre la caméra et le joueur
    public float smoothSpeed = 5f;  // Vitesse de suivi

    void LateUpdate()
    {
        if (player == null) return;

        // Position cible de la caméra
        Vector3 targetPosition = player.position + offset;
        // Déplacement fluide de la caméra
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
