using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))] // add rigidbody auto 


public class PlayerMovement: MonoBehaviour // mouvement !!
{
    #region private variables
    public Rigidbody2D rgbd2d;
    [SerializeField]private Vector3 mouvementVector; // public cuz we gonna use it for attact hethi lezem prive hotou return fct gett aamlha don' use varible in script !!
    private float lastHorizontalVector; // for attact animation bch ye5ou last input
    private float lastVerticalVector;
    private Animate animate;
    [SerializeField] private float speed = 3f;
    private bool isAnimating;
    

    #endregion

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


    #region Unity callBacks
    private void Awake()
    {
        Init();

    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
     void Start()
    {
        
    }
    #endregion




    





    #region Public Functions
    public Vector3 GetMouvementVector(){
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


    #region private functions
    private void Init()
    {
        rgbd2d = GetComponent<Rigidbody2D>(); //On récupère Rigidbody2D du GameObject
        mouvementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void PlayerMove()
    {
        if (isAnimating) return; // If the player is animating, skip movement

        mouvementVector.x = Input.GetAxisRaw("Horizontal");
        mouvementVector.y = Input.GetAxisRaw("Vertical");

        // Update last vectors independently
        if (mouvementVector.x != 0)
        {
            lastHorizontalVector = mouvementVector.x;
        }
        if (mouvementVector.y != 0)
        {
            lastVerticalVector = mouvementVector.y;
        }

        mouvementVector *= speed;
        rgbd2d.velocity = mouvementVector;
    }

    private IEnumerator FreezeMovementCoroutine(float duration)
    {
        rgbd2d.velocity = Vector2.zero; // Set the player's velocity to zero to stop movement
        isAnimating = true; // Freeze movement
        yield return new WaitForSeconds(duration); // Wait for the specified duration (1 second)
        isAnimating = false; // Unfreeze movement
    }
    #endregion



}
