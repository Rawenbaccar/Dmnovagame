using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    #region private variale 
    private Transform player;
    private float movespeed;
    #endregion

    #region Unity CallBacks
    void Start()
    {
        Init();
    }
    void Update()
    {
        Position();
    }
    #endregion



    #region private functions
    private void Init()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    

    private void Position()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);
    }
    #endregion
}

