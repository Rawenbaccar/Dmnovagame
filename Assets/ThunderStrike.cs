using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    public float strikeInterval = 2f;
    public int maxBounces = 3;
    public float bounceRange = 3f;
    public LayerMask enemyLayer;
    public GameObject lightningEffectPrefab;

    private bool isActive = false; // Désactivé par défaut
    private float timer;

    void Update()
    {
        if (!isActive) return; // Si le pouvoir n'est pas activé, ne rien faire

        timer += Time.deltaTime;
        if (timer >= strikeInterval)
        {
            timer = 0;
            StrikeEnemy();
        }
    }

    public void ActivateThunder()
    {
        isActive = true;
        StrikeEnemy(); // Lance immédiatement un éclair
    }

    void StrikeEnemy()
    {
        GameObject firstEnemy = FindRandomEnemy();
        if (firstEnemy != null)
        {
            ChainLightning(firstEnemy, maxBounces);
        }
    }

    GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;
        return enemies[Random.Range(0, enemies.Length)];
    }

    void ChainLightning(GameObject enemy, int remainingBounces)
    {
        if (enemy == null) return;

        InstantiateLightningEffect(enemy.transform.position);
        Destroy(enemy);

        if (remainingBounces > 0)
        {
            GameObject nextEnemy = FindNearestEnemy(enemy.transform.position);
            if (nextEnemy != null)
            {
                ChainLightning(nextEnemy, remainingBounces - 1);
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
