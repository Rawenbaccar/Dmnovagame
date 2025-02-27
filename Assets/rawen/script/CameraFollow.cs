using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // R�f�rence au joueur
    public Vector3 offset;    // D�calage entre la cam�ra et le joueur
    public float smoothSpeed = 5f;  // Vitesse de suivi

    void LateUpdate()
    {
        if (player == null) return;

        // Position cible de la cam�ra
        Vector3 targetPosition = player.position + offset;
        // D�placement fluide de la cam�ra
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
