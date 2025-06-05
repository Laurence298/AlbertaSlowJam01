using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public float playerHealth, maxPlayerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {


        transform.GetComponent<SpriteRenderer>().color = Color.red;
        playerHealth -= damage;

        transform.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
