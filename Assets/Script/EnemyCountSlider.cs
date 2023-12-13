using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountSlider : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = 0;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }

}
