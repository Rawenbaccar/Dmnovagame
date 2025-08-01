
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    
    public ExperienceLevelController ELC;

    void Start()
    {
        ELC = FindAnyObjectByType<ExperienceLevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ELC.CollectDiamond();
            Destroy(gameObject); // Si le joueur touche l'effet, il dispara�t imm�diatement
        }
    }
}