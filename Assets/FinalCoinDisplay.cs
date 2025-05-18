using UnityEngine;
using UnityEngine.UI;

public class FinalCoinDisplay : MonoBehaviour
{
    public static FinalCoinDisplay Instance; // Instance statique pour le singleton
    public Text coinTotalText;

    private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance de FinalCoinDisplay
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Détruire l'objet si une autre instance existe déjà
        }
        else
        {
            Instance = this; // Définir l'instance
        }
    }

    private void Start()
    {
        // Attendre que CoinData soit bien initialisé
        Invoke("DisplayCoins", 0.1f); // Délai très court pour éviter les problèmes d’ordre de chargement
    }

    private void DisplayCoins()
    {
        if (CoinData.instance != null)
        {
            coinTotalText.text = CoinData.instance.totalCoins.ToString();
            Debug.Log("FinalCoinDisplay: Coins displayed: " + CoinData.instance.totalCoins);
        }
        else
        {
            coinTotalText.text = "No data found.";
            Debug.LogWarning("FinalCoinDisplay: CoinData.instance is null");
        }
    }
}
