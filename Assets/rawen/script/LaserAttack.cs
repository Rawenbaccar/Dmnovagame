using System.Collections;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] private Transform player; // Référence au joueur
    [SerializeField] private float laserDuration = 10f; // Durée totale du laser
    [SerializeField] private float attackInterval = 3f; // Intervalle entre les tirs
    [SerializeField] private GameObject laserEffectPrefab; // Effet visuel du laser
    [SerializeField] private float laserEffectLifetime = 1.5f; // Durée de l'effet laser

    private bool isActive = false;

    public void ActivateLaser()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(FireLaser());
        }
    }

    private IEnumerator FireLaser()
    {
        float elapsedTime = 0f;

        while (elapsedTime < laserDuration)
        {
            KillClosestEnemies(2); // Tue les 2 ennemis les plus proches
            yield return new WaitForSeconds(attackInterval);
            elapsedTime += attackInterval;
        }

        Destroy(gameObject); // Détruit le laser après utilisation
    }

    private void KillClosestEnemies(int count)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) return;

        // Trier les ennemis par distance au joueur
        System.Array.Sort(enemies, (a, b) =>
        {
            float distA = Vector2.Distance(player.position, a.transform.position);
            float distB = Vector2.Distance(player.position, b.transform.position);
            return distA.CompareTo(distB);
        });

        // Attaquer les "count" ennemis les plus proches
        for (int i = 0; i < Mathf.Min(count, enemies.Length); i++)
        {
            GameObject laserEffect = Instantiate(laserEffectPrefab, enemies[i].transform.position, Quaternion.identity); // Effet laser
            Destroy(laserEffect, laserEffectLifetime); // Détruit l'effet laser après un certain temps
            Destroy(enemies[i]); // Supprime l'ennemi
        }
    }
}
