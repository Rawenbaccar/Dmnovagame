using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUpSelector : MonoBehaviour
{
    public List<GameObject> powerUps; // Assigne tes 10 power-ups dans l'inspecteur
    public RectTransform panel; // Référence du panel contenant les power-ups

    void OnEnable()
    {

        SelectRandomPowerUps();


    }

    void SelectRandomPowerUps()
    {
        if (powerUps.Count < 3)
        {
            Debug.LogError("Il faut au moins 3 power-ups dans la liste !");
            return;
        }

        // Désactiver tous les power-ups
        foreach (GameObject powerUp in powerUps)
        {
            powerUp.SetActive(false);
        }

        // Mélanger la liste de power-ups
        List<GameObject> shuffledList = new List<GameObject>(powerUps);
        for (int i = 0; i < shuffledList.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledList.Count);
            (shuffledList[i], shuffledList[randomIndex]) = (shuffledList[randomIndex], shuffledList[i]);
        }

        // Sélectionner et activer 3 power-ups aléatoires
        List<GameObject> selectedPowerUps = shuffledList.GetRange(0, 3);
        foreach (GameObject powerUp in selectedPowerUps)
        {
            powerUp.SetActive(true);
        }

    }
}