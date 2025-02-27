using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed = 5f;
    [SerializeField] private float baseAttackSpeed = 1f;

    public void IncreasePlayerSpeed()
    {
        basePlayerSpeed *= 1.5f;
       
        FindObjectOfType<PlayerMovement>().SetSpeed(basePlayerSpeed);
    }

    public void IncreaseAttackSpeed()
    {
        baseAttackSpeed *= 1.5f;
        
        FindObjectOfType<WhipWapean>().SetAttackSpeed(baseAttackSpeed);
    }



}