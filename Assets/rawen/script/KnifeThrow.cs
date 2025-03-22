using UnityEngine;
using System.Collections;

public class KnifeThrow : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float knifeSpeed = 5f;
    [SerializeField] private float cooldown = 1f;
    private bool isAutoThrowActive = false; // État du Power-Up

    void Start()
    {
        StartCoroutine(AutoThrowKnife());
    }

    private IEnumerator AutoThrowKnife()
    {
        while (true)
        {
            if (isAutoThrowActive)
            {
                GameObject target = FindClosestEnemy();
                if (target != null)
                {
                    StartCoroutine(ThrowKnife(target));
                }
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private IEnumerator ThrowKnife(GameObject target)
    {
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
        while (knife != null && target != null && Vector2.Distance(knife.transform.position, target.transform.position) > 0.1f)
        {
            knife.transform.position = Vector2.MoveTowards(knife.transform.position, target.transform.position, knifeSpeed * Time.deltaTime);
            yield return null;
        }

        if (knife != null) Destroy(knife);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }

    // Méthodes pour gérer le Power-Up
    public void ActivateAutoThrow()
    {
        isAutoThrowActive = true;
    }

    public void DeactivateAutoThrow()
    {
        isAutoThrowActive = false;
    }
}
