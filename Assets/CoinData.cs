using UnityEngine;

public class CoinData : MonoBehaviour
{
    public static CoinData instance;
    public int totalCoins = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadCoins();

        Debug.Log("CoinData loaded. Total coins: " + totalCoins);
    }


    public void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save(); // Enregistre sur le disque
    }

    public void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public void AddCoin()
    {
        totalCoins++;
        SaveCoins(); // Sauvegarder à chaque ajout
    }
}
