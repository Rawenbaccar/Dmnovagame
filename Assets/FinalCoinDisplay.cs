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
            Destroy(gameObject); // D�truire l'objet si une autre instance existe d�j�
        }
        else
        {
            Instance = this; // D�finir l'instance
        }
    }

    private void Start()
    {
        // Attendre que CoinData soit bien initialis�
        Invoke("DisplayCoins", 0.1f); // D�lai tr�s court pour �viter les probl�mes d�ordre de chargement
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
