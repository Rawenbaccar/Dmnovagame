using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Ajout d'un namespace pour éviter les conflits

public class ExperienceLevelController : MonoBehaviour
{
    // Event for level up
    public delegate void LevelUpHandler(int newLevel);
    public event LevelUpHandler OnLevelUp;

    // Utilisation du pattern Singleton avec vérification de nullité
    private static ExperienceLevelController _instance;
    public static ExperienceLevelController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ExperienceLevelController>();
            }
            return _instance;
        }
    }

    [Header("Level Settings")]
    [SerializeField] public int currentLevel = 1;
    [SerializeField] private int diamondsPerLevel;
    [SerializeField] private int collectedDiamonds;

    [Header("UI References")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject powerUpPanel; // Référence au panel de power-up

    [Header("Slider Animation")]
    //[SerializeField] private float sliderSpeed = 2f; // Vitesse de l'animation du slider
    public float targetSliderValue = 0f;
    public float currentSliderValue = 0f;

    [Header("Enemy Management")]
    [SerializeField] private EnemySpawner enemySpawner; // Reference to the EnemySpawner
    [SerializeField] private GroundChange groundChange;
    [SerializeField] private CoinManager coinManager;


    private void Awake()
    {
        // S'assurer qu'il n'y a qu'une seule instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        UpdateUI();
    }

    private void Start()
    {
        if (powerUpPanel != null)
            powerUpPanel.SetActive(false);
        UpdateUI();
        if (coinManager != null)
        {
            coinManager.SpawnCoins(); //  Spawn 3 pièces dès le départ
        }
    }

    private void Update()
    {
        // Animation fluide du slider
        if (currentSliderValue != targetSliderValue)
        {
            //Debug.Log("wuuut ?");
            //currentSliderValue = Mathf.Lerp(currentSliderValue, targetSliderValue, Time.deltaTime * sliderSpeed);
            //currentSliderValue += 0.2f;
            expSlider.value = (float)collectedDiamonds / diamondsPerLevel;

            if (expSlider.value >= 0.999f)
            {
                ShowPowerUpPanel();
                currentSliderValue = 0f;
                collectedDiamonds = 0;
                targetSliderValue = 0f;
                expSlider.value = 0f;
                LevelUp();
                //  Time.timeScale = 1f; // Reprend le jeu si vous l'aviez mis en pause
            }
        }
    }

    public void SpawnExp(Vector3 position)
    {
        AddExperience(1);
    }

    public void CollectDiamond()
    {

        collectedDiamonds++;
        targetSliderValue = (float)collectedDiamonds / diamondsPerLevel;

        UpdateUI();




    }
    private void AddExperience(int amount)
    {
        this.currentLevel += amount;
        Debug.Log($"Niveau augmenté! Niveau actuel: {this.currentLevel}");
        UpdateUI();
    }

    private void ShowPowerUpPanel()
    {
        if (powerUpPanel != null)
        {
            powerUpPanel.SetActive(true);
            //  Time.timeScale = 0f; // Optionnel : met le jeu en pause
        }
    }

    // Méthode à appeler depuis les boutons de power-up
    public void OnPowerUpSelected()
    {
        if (powerUpPanel != null)
            powerUpPanel.SetActive(false);

        Time.timeScale = 1f; // Reprend le jeu si vous l'aviez mis en pause
        LevelUp();
        collectedDiamonds = 0;
        targetSliderValue = 0f;
    }

    private void LevelUp()
    {
        this.currentLevel++;

        // Update enemy spawner's level
        if (enemySpawner != null)
        {
            enemySpawner.SetLevel(this.currentLevel);
            Debug.Log($"Updated EnemySpawner level to {this.currentLevel}");
        }
        else
        {
            Debug.LogError("EnemySpawner reference is missing in ExperienceLevelController!");
        }

        // Trigger the level up event
        OnLevelUp?.Invoke(this.currentLevel);

        // Update enemy health scaling
        if (EnemyHealthManager.Instance != null)
        {
            EnemyHealthManager.Instance.OnPlayerLevelUp(this.currentLevel);
        }

        UpdateUI();
        SpawnNewEnemyType();
        AudioManager.PlayLevelChangeSound();
        if (coinManager != null)
        {
            coinManager.SpawnCoins(); // ➕ Spawn 3 nouvelles pièces
        }
        if (groundChange != null)
        {
            groundChange.OnLevelUp();
        }
    }

    private void SpawnNewEnemyType()
    {
        if (enemySpawner != null)
        {

            enemySpawner.UnlockNewEnemy();
        }
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ChangeMusicForLevel(this.currentLevel);
        }
    }



    private void UpdateUI()
    {
        // Met à jour le texte du niveau dans le slider
        if (levelText != null)
        {
            levelText.text = $"Level {this.currentLevel}";
        }
    }
}