using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    #region private variable 
     private float timeToDisable = 0.8f;
     private float timer;
    #endregion


    #region Unity CallBacks
    private void OnEnable()
    {
        timer = timeToDisable;
        // On supprime la vérification de l'échelle car elle doit rester
        // cohérente avec la direction du joueur
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
           gameObject.SetActive(false);
        }
    }
    #endregion 
}
