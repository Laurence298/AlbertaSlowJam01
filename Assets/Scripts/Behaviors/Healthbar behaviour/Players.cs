using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    public float maxHealth = 25;
    public float currentHealth;

    public Healthbar healthbar;

    void Start()
    {
       
    }

    public void SetHealthBar(Healthbar healthbar)
    {
        this.healthbar = healthbar;
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }
}
