using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Healthbar currentHealth;
    public int maxHealth = 100;
    public int minHealth = 0;
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health)
    {
        slider.value = health;
    }

  
    
}
