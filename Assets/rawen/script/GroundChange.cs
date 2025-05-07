using UnityEngine;

public class GroundChange : MonoBehaviour
{
    public Sprite[] groundSprites; // assigner les sprites dans l’inspecteur
    public SpriteRenderer groundRenderer; // assigner le SpriteRenderer du sol
    public int levelsPerChange = 3;

    private int currentLevel = 1;

    public void OnLevelUp()
    {
        currentLevel++;

        if (currentLevel % levelsPerChange == 0)
        {
            int index = (currentLevel / levelsPerChange - 1) % groundSprites.Length;
            groundRenderer.sprite = groundSprites[index];
        }
    }
}