using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Healthbar : MonoBehaviour
{
   
    public TextMeshProUGUI text;
    public void setMaxHealth(float health)
    {
        text.text = health.ToString();
    }

    public void SetHealth(float health)
    {
        text.text = health.ToString();

    }

  
    
}
