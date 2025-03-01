using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] DataContainer datacontainer;
    // Start is called before the first frame update
    void Start()
    {
        LoadSelectedCharacter(datacontainer.selectedCharacter);
    }
    private void LoadSelectedCharacter(CharacterData selectedCharacter)
    {
        InitAnimation(selectedCharacter.spritePrefab);
    }

    private void InitAnimation(GameObject spritePrefab)
    {
        GameObject animObject = Instantiate(spritePrefab, transform);
        

    }

    // Update is called once per frame
    
}
