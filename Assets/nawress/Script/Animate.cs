using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    #region public varivables
    public float GetHorizontal()
    {
        return horizontal;
    }
    public void SetHorizontal(float horizontal)
    {
        this.horizontal = horizontal;
    }
    #endregion



    #region private variable
    private Animator animator;
    private  float horizontal;
    private  SpriteRenderer spriteRenderer;
    #endregion


    #region CallBacks
    private void Awake()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        Animat();
    }
    #endregion


    #region private functions
    private void Init()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Animat()
    {
       // animator.SetFloat("Horizontal", Mathf.Abs(horizontal));

        if (horizontal != 0)
        {
            spriteRenderer.flipX = (horizontal < 0);
        }
    }
    #endregion
}
