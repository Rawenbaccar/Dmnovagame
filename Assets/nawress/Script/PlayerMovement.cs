using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))] // add rigidbody auto 


public class PlayerMovement: MonoBehaviour // mouvement !!
{
    #region private variables
    private Rigidbody2D rgbd2d;
    [SerializeField]private Vector3 mouvementVector; // public cuz we gonna use it for attact hethi lezem prive hotou return fct gett aamlha don' use varible in script !!
    private float lastHorizontalVector; // for attact animation bch ye5ou last input
    private float lastVerticalVector;
    private Animate animate;
    [SerializeField] private float speed = 3f;

    #endregion



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
    #endregion










    #region Public Functions
    public Vector3 GetMouvementVector(){
        return mouvementVector;
    }
    
    
    public float GetLastHorizontalVector()
    {
        return lastHorizontalVector;
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
        //animate.SetHorizontal(mouvementVector.x);
        rgbd2d.velocity = mouvementVector;
    }
    #endregion



}
