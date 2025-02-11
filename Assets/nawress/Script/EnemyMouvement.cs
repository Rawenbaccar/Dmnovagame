using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    #region private variables 
    private Transform player;

    [SerializeField] private float moveSpeed = 2.0f; // Définir la vitesse de déplacement
    #endregion

    #region Unity Callbacks
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }
    #endregion

    #region private functions
    private void Init()
    {
        // Assurez-vous que le joueur est trouvé
        PlayerMovement playerScript = FindObjectOfType<PlayerMovement>();
        if (playerScript != null)
        {
            player = playerScript.transform;
        }
        else
        {
            Debug.LogError("Aucun objet avec le script PlayerMovement n'a été trouvé !");
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    #endregion
}
