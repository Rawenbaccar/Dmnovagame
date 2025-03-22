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

        // Find PlayerHealthManager
        PlayerHealthManager healthManager = FindObjectOfType<PlayerHealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("PlayerHealthManager NOT found!");
            return;
        }

        // Find PlayerAnimator
        PlayerAnimator animator = animObject.GetComponent<PlayerAnimator>(); 
        if (animator == null)
        {
            Debug.LogError("PlayerAnimator NOT found in instantiated character!");
            return;
        }

        // Assign animator to health manager
        healthManager.playerAnimator = animator;
        Debug.Log("Successfully assigned PlayerAnimator to HealthManager!");
    }





    // Update is called once per frame

}
