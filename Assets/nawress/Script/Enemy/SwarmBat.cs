using UnityEngine;

public class SwarmBat : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 moveDirection;
    private float waveFrequency = 2f; // How fast it wiggles
    private float waveAmplitude = 0.5f; // How much it wiggles
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        Vector2 waveMovement = new Vector2(moveDirection.x, waveOffset);
        transform.position += (Vector3)waveMovement * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}
