using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterDataShop : ScriptableObject
{
    public string characterName;
    public Sprite characterIcon;
    public string characterInfo;
    public string characterPrice;
    public int maxHealth;
    public float moveSpeed;
    public float attackSpeed;
    public float cooldown;
    public int magnet;
    public int might;
    public int characterID;
    public int priceValue;
    public Sprite[] statIcons;

    public bool isUnlocked
    {
        get => PlayerPrefs.GetInt($"CharacterUnlocked_{characterID}", 0) == 1;
        set => PlayerPrefs.SetInt($"CharacterUnlocked_{characterID}", value ? 1 : 0);
    }
}
