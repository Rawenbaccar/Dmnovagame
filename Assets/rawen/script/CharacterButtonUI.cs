
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonUI : MonoBehaviour
{
    public CharacterDataShop characterData;
    public Image iconImage;
    public TMP_Text nameText;
    public Text infoText;
    public Text PriceText;
    public GameObject lockIcon; // << AJOUTÉ : référence à l'icône du cadenas


    public void Start()
    {
        UpdateLockState();
    }
    public void Setup(CharacterDataShop data)
    {
        characterData = data;
        iconImage.sprite = data.characterIcon;
        nameText.text = data.characterName;
        infoText.text = data.characterInfo;
        PriceText.text = data.characterPrice;

        UpdateLockState(); // << AJOUTÉ
    }

    public void OnClick()
    {
        Debug.Log("hello from debugger");
        CharacterSelectionManager.Instance.SelectCharacter(characterData, this);
        if (!characterData.isUnlocked)
        {
            CharacterAPI itemAPI = FindObjectOfType<CharacterAPI>();
            itemAPI.AddItem(characterData.characterName);
            // Debug.Log("Ce personnage est verrouillé. Aucune action effectuée.");
            // return; // Ne rien faire si le personnage est encore verrouillé
        }

        // API system (pas vraiment nécessaire ici dans le shop, tu peux l'enlever si tu veux)

    }
    /* public void OnClick()
     {
         if (!characterData.isUnlocked)
         {
             Debug.Log("Ce personnage est verrouillé. Aucune action effectuée.");
             return; // Ne rien faire si le personnage est encore verrouillé
         }

         Debug.Log("Personnage sélectionné : " + characterData.characterName);
         CharacterSelectionManager.Instance.SelectCharacter(characterData, this);

         // Appel API : uniquement pour les personnages déverrouillés
         CharacterAPI itemAPI = FindObjectOfType<CharacterAPI>();
         itemAPI.AddItem(characterData.characterName);
     }*/

    public void UpdateLockState()
    {
        if (lockIcon != null)
            lockIcon.SetActive(!characterData.isUnlocked);
    }
}
