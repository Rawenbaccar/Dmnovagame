using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    #region Private Variable 
    public float curentHealth , maxHealth;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        curentHealth =maxHealth;
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            
        }
    }

    public void TakeDamage(float damageToTake){
        curentHealth -= damageToTake;
        if (curentHealth <=0){
            gameObject.SetActive(false);
        }
        
    }
}
