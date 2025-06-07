using UnityEngine;

public class SlowingProjectile : MonoBehaviour
{

    public Vector2 targetPos;
    public float damage, projectileSpeed, projectileRange, effectDuration;


    private void Start()
    {
        //Bullets points towards the target when spawned
        Vector2 bulletDirection = (Vector2)transform.position - targetPos;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg + 90);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, projectileSpeed * Time.deltaTime);

        //NEED: Destroy the object even if it doesn't hit anything, maybe just when it goes outside the range?
        //NEED: or destroyed when its original target is dead
        if (Vector2.Distance(transform.position, targetPos) < 0.1)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.5f);
        }

    }

    //Destroy Object if it hits something
    //NEED: Clarify what it needs to hit to disappear
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Abstract_Enemy>().health -= damage;
        if (collision.gameObject.GetComponent<Abstract_Enemy>().status != Abstract_Enemy.StatusEffects.Stun) 
        {
            collision.gameObject.GetComponent<Abstract_Enemy>().ApplyDebuff(Abstract_Enemy.StatusEffects.Stun, effectDuration);
        }
        Destroy(gameObject);
    }
}
