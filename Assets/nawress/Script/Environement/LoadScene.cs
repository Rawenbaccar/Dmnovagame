using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadScene1(string character)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene2(string SampleScene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void chactersekected()
    {
        SceneManager.LoadScene("Character");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("leaderBord");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("shop( character)");
    }
}
