using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWapean : MonoBehaviour
{
    #region Private varibles
    [SerializeField] private  float timeToAttack = 4f; //how long it takes to use this weapen
    private float timer; // pour suivi le temps pour la prochaine attaque 
    [SerializeField] private GameObject leftWhipObject; //show varible in inspecter meme hiya private
    [SerializeField] private GameObject rightWhipObject;
    private PlayerMovement playerMove;
    #endregion


    #region Unity CallBacks
    // Start is called before the first frame update
    void Start() 
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAttack();
    }
    #endregion



    #region Private Functions
    private void ManageAttack()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        { // si le temps ecouler le joueur peut attquer 
            Attack();
        }
    }

    private void Attack(){
        timer = timeToAttack; 

        if(playerMove.GetLastHorizontalVector() > 0)
        {
            leftWhipObject.SetActive(true);
        }
        else{
            rightWhipObject.SetActive(true);
        }
        #endregion

    }
}
