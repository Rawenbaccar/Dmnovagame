using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinUIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<CoinUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiManager != null)
            {
                uiManager.AddCoin();
            }

            if (CoinData.instance != null)
            {
                CoinData.instance.AddCoin(); // Sauvegarde automatique ici
            }

            Destroy(gameObject);
        }
    }

}


