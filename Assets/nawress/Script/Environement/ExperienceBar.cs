using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void UpdateExperienceSlider(int curent, int target )
    {
        slider.maxValue = target;
        slider.value = curent;

    }
}
