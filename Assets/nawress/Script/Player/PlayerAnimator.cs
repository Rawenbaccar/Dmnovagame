using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //References 
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;
    private bool isAnimationFrozen = false; // Track if the animation is frozen
    private Color originalColor; // Store the original color of the sprite

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; // Store the original color
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the animation is frozen
        if (isAnimationFrozen) return; // Skip animation updates if frozen

        // Check if there's any movement (horizontal or vertical)
        if (pm.GetMouvementVector().x != 0 || pm.GetMouvementVector().y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }
    void SpriteDirectionChecker()
    {
        if(pm.GetLastHorizontalVector() < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    internal void SetAnimate(GameObject animObject)
    {
        am = animObject.GetComponent<Animator>();
    }

    public void Die()
    {
        Debug.Log("Player Died");
        am.SetTrigger("Dead"); // Play the dead animation
    }

    // Method to freeze the animation
    public void FreezeAnimation(float duration)
    {
        StartCoroutine(FreezeAnimationCoroutine(duration));
    }

    private IEnumerator FreezeAnimationCoroutine(float duration)
    {
        isAnimationFrozen = true; // Freeze the animation
        sr.color = Color.white; // Change the color to white
        yield return new WaitForSeconds(duration); // Wait for the specified duration (1 second)
        sr.color = originalColor; // Reset the color back to the original
        isAnimationFrozen = false; // Unfreeze the animation
    }

}
