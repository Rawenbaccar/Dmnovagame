/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int expValue;

    private bool movingToPlayer;
    public float moveSpeed;


    public float timeBetweenChecks = .2f;
    private float checkCounter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)
        {
            // transform.position = Vector3.MoveTowards(transform.position, PlayerMovementt.instance.transform.position, moveSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);
            FindObjectOfType<Level>().AddDiamond();
            Destroy(gameObject);
        }

    }

}*/