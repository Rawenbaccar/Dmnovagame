using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;


   //[SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private string menuSceneName = "MainMenu"; // Nom de votre scène de menu

   

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        // Retourne au menu principal
        SceneManager.LoadScene(menuSceneName);
    }
}