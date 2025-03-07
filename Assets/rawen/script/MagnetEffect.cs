using UnityEngine;
using System.Collections;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private float magnetRadius = 15f;  // Distance d'attraction
    [SerializeField] private float attractionSpeed = 10f;  // Vitesse d'attraction
    [SerializeField] private float duration = 10f;  // Durée du pouvoir
    [SerializeField] private ParticleSystem magnetEffect;  // Effet visuel
    public bool isActive = false;

    private void Start()
    {
        if (magnetEffect != null)
        {
            magnetEffect.Stop();
        }
    }

    public void ActivateMagnet()
    {
        Debug.Log("activito magnito temchito wella lito ?");
        isActive = true;

        if (magnetEffect != null)
        {
            magnetEffect.Play();
        }

        StartCoroutine(DeactivateAfterTime(duration));
    }

    private void Update()
    {
        if (isActive)
        {

            AttractDiamonds();
        }
    }

    private void AttractDiamonds()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, magnetRadius);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Diamond"))
            {
                Debug.Log("el magnito yemchito");
                // Faire bouger le diamant vers le joueur
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, Time.deltaTime * attractionSpeed);
            }
        }
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = false;

        if (magnetEffect != null)
        {
            magnetEffect.Stop();
        }
    }
}
