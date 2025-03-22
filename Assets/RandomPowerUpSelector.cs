using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUpSelector : MonoBehaviour
{
    public List<GameObject> powerUps; // Assigne tes 10 power-ups dans l'inspecteur
    public RectTransform panel; // R�f�rence du panel contenant les power-ups

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

        // D�sactiver tous les power-ups
        foreach (GameObject powerUp in powerUps)
        {
            powerUp.SetActive(false);
        }

        // M�langer la liste de power-ups
        List<GameObject> shuffledList = new List<GameObject>(powerUps);
        for (int i = 0; i < shuffledList.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledList.Count);
            (shuffledList[i], shuffledList[randomIndex]) = (shuffledList[randomIndex], shuffledList[i]);
        }

        // S�lectionner et activer 3 power-ups al�atoires
        List<GameObject> selectedPowerUps = shuffledList.GetRange(0, 3);
        foreach (GameObject powerUp in selectedPowerUps)
        {
            powerUp.SetActive(true);
        }

        // Recentrer les power-ups
        CenterPowerUps(selectedPowerUps);
    }

    void CenterPowerUps(List<GameObject> selectedPowerUps)
    {
        float spacing = 250f; // Ajuste l'�cart entre les power-ups
        float startX = -spacing; // Position de d�part pour centrer

        for (int i = 0; i < selectedPowerUps.Count; i++)
        {
            RectTransform rt = selectedPowerUps[i].GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(startX + (i * spacing), 0);
            }
        }
    }
}
