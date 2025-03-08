using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    public float strikeInterval = 2f;
    public float strikeRange = 5f; // Distance maximale entre le héros et l'ennemi pour déclencher l'éclair
    public int maxBounces = 3;
    public float bounceRange = 1f;
    public LayerMask enemyLayer;
    public GameObject lightningEffectPrefab;
    public Transform hero; // Référence au héros

    private bool isActive = false;
    private bool isStriking = false; // Empêche plusieurs éclairs en même temps
    private float timer;

    void Update()
    {
        if (!isActive || isStriking) return; // Ne fait rien si désactivé ou en cours de frappe

        timer += Time.deltaTime;
        if (timer >= strikeInterval)
        {
            Debug.Log("bashar is cute");
            timer = 0;
            StartCoroutine(StrikeEnemy());
        }
    }

    public void ActivateThunder()
    {
        isActive = true;
        timer = 0; // Réinitialise le timer pour frapper immédiatement
        StartCoroutine(StrikeEnemy());
    }

    private IEnumerator StrikeEnemy()
    {
        isStriking = true;

        GameObject firstEnemy = FindClosestEnemy();
        if (firstEnemy != null)
        {
            yield return StartCoroutine(ChainLightning(firstEnemy, maxBounces));
        }

        isStriking = false;
    }

    GameObject FindClosestEnemy()
    {
        if (hero == null) return null; // Vérifie si le héros est assigné

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = strikeRange; // Distance maximale

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(hero.position, enemy.transform.position);
            if (distance <= strikeRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private IEnumerator ChainLightning(GameObject enemy, int remainingBounces)
    {
        if (enemy == null) yield break;

        InstantiateLightningEffect(enemy.transform.position);
        yield return new WaitForSeconds(0.2f); // Petit délai pour l'effet visuel

        Destroy(enemy);

        if (remainingBounces > 0)
        {
            GameObject nextEnemy = FindNearestEnemy(enemy.transform.position);
            if (nextEnemy != null)
            {
                yield return StartCoroutine(ChainLightning(nextEnemy, remainingBounces - 1));
            }
        }
    }

    GameObject FindNearestEnemy(Vector3 position)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(position, bounceRange, enemyLayer);
        if (enemiesInRange.Length == 0) return null;
        return enemiesInRange[Random.Range(0, enemiesInRange.Length)].gameObject;
    }

    void InstantiateLightningEffect(Vector3 position)
    {
        if (lightningEffectPrefab != null)
        {
            GameObject lightning = Instantiate(lightningEffectPrefab, position, Quaternion.identity);
            Destroy(lightning, 0.5f);
        }
    }
}