


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;



    private void Awake()
    {
        {
            instance = this;
        }
    }

    public ExpPickup pickup;
    public int currentExperience;



    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
    }

    public void SpawnExp(Vector3 position)
    {
        if (pickup != null)
        {
            Instantiate(pickup, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("ExpPickup n'est pas assigné dans l'inspecteur !");
        }
    }
}
