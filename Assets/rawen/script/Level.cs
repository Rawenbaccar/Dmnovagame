using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int requiredDiamonds = 5; // Nombre de diamants nécessaires pour monter de niveau
    private int currentDiamonds = 0;
    [SerializeField] private Text levelText;

    private void Start()
    {
        UpdateLevelUI();
    }

    public void AddDiamond()
    {
        currentDiamonds++;
        Debug.Log($"Diamants collectés : {currentDiamonds}/{requiredDiamonds}");

        if (currentDiamonds >= requiredDiamonds)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentDiamonds = 0; // Réinitialise le compteur de diamants
        level++; // Augmente le niveau
        Debug.Log($"Niveau augmenté ! Nouveau niveau : {level}");
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = "Level " + level.ToString();
        }
    }
}