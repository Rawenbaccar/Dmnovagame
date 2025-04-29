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

        // Recentrer les power-ups
        CenterPowerUps(selectedPowerUps);
    }

    void CenterPowerUps(List<GameObject> selectedPowerUps)
    {
        if (panel == null)
        {
            Debug.LogError("Panel reference is missing!");
            return;
        }

        float panelWidth = panel.rect.width;
        float panelHeight = panel.rect.height;
        
        // Keep the same horizontal spacing
        float spacing = panelWidth * 0.3f;
        
        bool useVerticalLayout = panelWidth < panelHeight * 0.8f;

        if (useVerticalLayout)
        {
            float verticalSpacing = panelHeight * 0.2f;
            float startY = verticalSpacing;

            for (int i = 0; i < selectedPowerUps.Count; i++)
            {
                RectTransform rt = selectedPowerUps[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = new Vector2(0, startY - (i * verticalSpacing));
                    
                    // Keep width the same, increase height significantly
                    float widthScale = panelWidth * 0.0045f;  // Width stays the same
                    float heightScale = panelWidth * 0.012f;   // Increased height scale from 0.007f to 0.012f
                    rt.localScale = new Vector3(widthScale, heightScale, 1f);
                    
                    rt.rotation = Quaternion.Euler(0, 0, 180f);
                }
            }
        }
        else
        {
            float totalWidth = spacing * (selectedPowerUps.Count - 1);
            float startX = -totalWidth / 2;

            for (int i = 0; i < selectedPowerUps.Count; i++)
            {
                RectTransform rt = selectedPowerUps[i].GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = new Vector2(startX + (i * spacing), 0);
                    
                    // Keep width the same, increase height significantly
                    float widthScale = panelWidth * 0.0045f;  // Width stays the same
                    float heightScale = panelWidth * 0.012f;   // Increased height scale from 0.007f to 0.012f
                    rt.localScale = new Vector3(widthScale, heightScale, 1f);
                    
                    rt.rotation = Quaternion.Euler(0, 0, 180f);
                }
            }
        }
    }
}