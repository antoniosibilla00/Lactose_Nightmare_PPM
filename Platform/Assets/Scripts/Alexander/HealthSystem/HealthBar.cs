using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public RectTransform borderSlider;
    public RectTransform fillBar;
    private float realDimension; //Dimensione in percentuale
    
    
    public void SetHealthBar(int currentHealth)
    {
        slider.value = currentHealth;
        
    }
    
    public void SetHealthBarMaxValue(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void ResizeHealthBar(float dimension)
    {
        Debug.Log("dimension + " + dimension);
        slider.maxValue = dimension;
        realDimension = (dimension / 100);
        Debug.Log("realDimension + " + realDimension);
        borderSlider.localScale = new Vector3(realDimension, 1, 1);
        fillBar.localScale = new Vector3(realDimension, 1, 1);

    }

  
    
}
