using UnityEngine;

public class FireballOrbit : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private FireballPowerUp fireball;

    private float angle;

    private void Start()
    {
        // Rendre la boule de feu invisible au début
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return; // Ne rien faire si la boule de feu est désactivée

        Debug.Log("fireBall supposed to be working");
        Vector2 center = (Vector2)player.position + offset;
        angle += fireball.orbitSpeed * Time.deltaTime;

        float x = center.x + Mathf.Cos(angle * Mathf.Deg2Rad) * fireball.distanceFromPlayer;
        float y = center.y + Mathf.Sin(angle * Mathf.Deg2Rad) * fireball.distanceFromPlayer;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void ActivateFireball()
    {
        gameObject.SetActive(true); // Active la boule de feu quand on clique sur le bouton
    }
}
