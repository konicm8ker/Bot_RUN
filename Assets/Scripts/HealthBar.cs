using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public RawImage fill;

    public void SetMaxHealth(int health)
    {
        // Set 100% health and 100% fill color based on gradient
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        // Set current health and current fill color based on gradient
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); // 0 - 1 value
    }
}
