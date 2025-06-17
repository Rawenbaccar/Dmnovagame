using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creer objet data conatiner
[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public CharacterData selectedCharacter;

    public void SetSelectedCharacter(CharacterData character)
    {
        selectedCharacter = character;
    }

}
