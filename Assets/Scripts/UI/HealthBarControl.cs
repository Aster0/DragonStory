using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;

    public void SetMaxHealth(float health)
    {

        slider.maxValue = health; // change the max value of the slider
    }
    public float GetMaxHealth()
    {

        return slider.maxValue; // change the max value of the slider
    }
    public void Damage(float health)
    {
        slider.value -= health; // minus off the health.
    }

    public void Heal(float health)
    {
        slider.value += health; // add on the health. 
    }

    public float GetHealth() // get the health
    {
        return slider.value; // by returning the slider's value
    }
}
