using System.Collections;
using UnityEngine;
using FirstPersonMobileTools; // Assure-toi d'utiliser le namespace de ton script Joystick

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region private variables
    public Rigidbody2D rgbd2d;
    [SerializeField] private Vector3 mouvementVector;
    private float lastHorizontalVector;
    private float lastVerticalVector;
    private Animate animate;
    [SerializeField] private float speed = 3f;
    private bool isAnimating;

    [Header("Joystick Settings")]
    public Joystick joystick; // 👈 Référence vers ton script Joystick
    #endregion

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    #region Unity Callbacks
    private void Awake()
    {
        Init();
    }

    void Update()
    {
        PlayerMove();
    }

    void Start() { }
    #endregion

    #region Public Functions
    public Vector3 GetMouvementVector()
    {
        return mouvementVector;
    }

    public float GetLastHorizontalVector()
    {
        return lastHorizontalVector;
    }

    public void FreezeMovement(float duration)
    {
        StartCoroutine(FreezeMovementCoroutine(duration));
    }
    #endregion

    #region Private Functions
    private void Init()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        mouvementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void PlayerMove()
    {
        if (isAnimating) return;

        // 👉 Utilisation du joystick au lieu de Input.GetAxisRaw
        mouvementVector.x = joystick.Horizontal;
        mouvementVector.y = joystick.Vertical;

        rgbd2d.velocity = mouvementVector.normalized * speed;

        if (mouvementVector.x != 0 || mouvementVector.y != 0)
        {
            lastHorizontalVector = mouvementVector.x;
            lastVerticalVector = mouvementVector.y;
        }

        if (animate != null)
            animate.MoveAnimation(mouvementVector);
    }

    private IEnumerator FreezeMovementCoroutine(float duration)
    {
        isAnimating = true;
        rgbd2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        isAnimating = false;
    }
    #endregion
}