using UnityEngine;

public class FireballOrbit : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private FireballPowerUp fireball;
    private float angle;
    private int fireballCount = 0; // Compteur de boules de feux


private void Awake()
    {
        gameObject.SetActive(false); // Désactive la fireball au lancement
    }
    private void Update()
    {
        //if (!gameObject.activeSelf) return;

        Vector2 center = (Vector2)player.position + offset;
        angle += fireball.orbitSpeed * Time.deltaTime;

        float x = center.x + Mathf.Cos(angle * Mathf.Deg2Rad) * fireball.distanceFromPlayer;
        float y = center.y + Mathf.Sin(angle * Mathf.Deg2Rad) * fireball.distanceFromPlayer;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void ActivateFireball()
    {
        fireballCount++;
        CreateFireballInstance(fireballCount);
    }

    private void CreateFireballInstance(int count)
    {
        GameObject newFireball = Instantiate(gameObject, player.position, Quaternion.identity);
        newFireball.transform.SetParent(player); // Attacher au joueur
        newFireball.GetComponent<FireballOrbit>().angle = 360f / count * (count - 1); // Répartir autour du joueur
        newFireball.SetActive(true);
    }
}
