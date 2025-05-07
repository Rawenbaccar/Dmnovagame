using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    private static DontDestroyOnLoadManager instance;
    public static DontDestroyOnLoadManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // If there's already an instance, destroy this one
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene loading event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find any duplicate DontDestroyOnLoad objects and remove them
        CheckForDuplicates("AudioManager");
        CheckForDuplicates("AuthManager");
        CheckForDuplicates("CoinData");
        CheckForDuplicates("DontDestroyOnLoad");
        CheckForDuplicates("QualityManager");
    }

    private void CheckForDuplicates(string objectName)
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true); // Include inactive objects
        bool foundFirst = false;

        foreach (GameObject obj in objects)
        {
            if (obj.name == objectName)
            {
                if (!foundFirst)
                {
                    foundFirst = true;
                    DontDestroyOnLoad(obj); // Make sure the first instance is marked DontDestroyOnLoad
                }
                else
                {
                    // If we already found one, destroy any duplicates
                    Destroy(obj);
                }
            }
        }
    }
} 