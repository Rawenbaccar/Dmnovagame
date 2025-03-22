using System.Collections;
using UnityEngine;

public class WhipWapean : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private float timeToAttack = 4f; // Temps entre les attaques
    private float timer;
    [SerializeField] private GameObject leftWhipObject;
    [SerializeField] private GameObject rightWhipObject;
    private PlayerMovement playerMove;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        ManageAttack();
    }
    #endregion

    #region Private Functions
    public void SetAttackSpeed(float newAttackSpeed)
    {
        timeToAttack = newAttackSpeed;
    }

    private void ManageAttack()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timer = timeToAttack;

        if (playerMove.GetLastHorizontalVector() > 0)
        {
            leftWhipObject.SetActive(true);
            StartCoroutine(DisableWhipAfterTime(leftWhipObject)); // Désactivation après un temps
        }
        else
        {
            rightWhipObject.SetActive(true);
            StartCoroutine(DisableWhipAfterTime(rightWhipObject));
        }
    }

    private IEnumerator DisableWhipAfterTime(GameObject whip)
    {
        yield return new WaitForSeconds(0.5f); // Ajuste la durée selon l'animation
        whip.SetActive(false);
    }
    #endregion
}
