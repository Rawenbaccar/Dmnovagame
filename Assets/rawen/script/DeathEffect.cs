
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    public ExperienceLevelController ELC;

    void Start()
    {
        ELC = FindAnyObjectByType<ExperienceLevelController>();
        //Destroy(gameObject, lifetime); // L'effet de mort dispara�t apr�s un certain temps
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