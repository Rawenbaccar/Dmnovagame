using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour
{
    public Text coinText;

    private void Start()
    {
        UpdateCoinText();
    }

    private void Update()
    {
        // Met à jour l'UI en continu (si les pièces changent dynamiquement)
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (CoinData.instance != null)
        {
            coinText.text = CoinData.instance.totalCoins.ToString();
        }
    }

    public void AddCoin()
    {
        if (CoinData.instance != null)
        {
            CoinData.instance.AddCoin();
            UpdateCoinText();
        }
    }
}
