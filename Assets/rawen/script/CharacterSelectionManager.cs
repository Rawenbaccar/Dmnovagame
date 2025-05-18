using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance;

    [Header("UI Elements")]
    public Text topBarText;
    public TMP_Text statsText;
    public Text infoTextCh;
    public Text PriceTextCh;
    public Image[] statImages;
    public Image bottomIcon;
    public Button buyButton; // << AJOUTÉ

    [HideInInspector] public CharacterDataShop selectedCharacterData;
    [HideInInspector] public CharacterButtonUI selectedButtonUI;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCharacter(CharacterDataShop data, CharacterButtonUI buttonUI = null)
    {
        selectedCharacterData = data;
        selectedButtonUI = buttonUI;

        topBarText.text = data.characterName;
        infoTextCh.text = data.characterInfo;
        PriceTextCh.text = data.characterPrice;

        statsText.text =
            $"{data.maxHealth}\n" +
            $"{data.moveSpeed}\n" +
            $"{data.attackSpeed}\n" +
            $"{data.cooldown}\n" +
            $"{data.magnet}\n" +
            $"{data.might}";

        for (int i = 0; i < statImages.Length; i++)
        {
            if (i < data.statIcons.Length)
                statImages[i].sprite = data.statIcons[i];
        }

        if (bottomIcon != null)
            bottomIcon.sprite = data.characterIcon;
    }

    public void BuyCharacter()
    {
        if (selectedCharacterData == null)
        {
            Debug.LogWarning("No character selected!");
            return;
        }

        if (selectedCharacterData.isUnlocked)
        {
            Debug.Log("Character already unlocked.");
            return;
        }

        if (CoinData.instance.totalCoins >= selectedCharacterData.priceValue)
        {
            CoinData.instance.totalCoins -= selectedCharacterData.priceValue;
            CoinData.instance.SaveCoins();
            selectedCharacterData.isUnlocked = true;
            selectedButtonUI.UpdateLockState(); // Met à jour le cadenas

            Debug.Log("Character unlocked!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
