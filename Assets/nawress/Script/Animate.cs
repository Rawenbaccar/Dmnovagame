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
    private float horizontal;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region CallBacks
    private void Awake()
    {
        Init();
    }

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

    //  Nouvelle méthode appelée depuis PlayerMovement
    public void MoveAnimation(Vector2 direction)
    {
        horizontal = direction.x; // ← utile pour flip
        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
    }
    #endregion
}