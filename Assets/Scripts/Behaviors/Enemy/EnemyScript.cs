using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : Abstract_Enemy
{
    public Vector2 targetPoint, pointA, pointB;
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        
    }

    public override void Move(Vector2 pointA, Vector2 pointB)
    {

        if(Vector2.Distance(transform.position, targetPoint) <= 0.1f) 
        {
            targetPoint = (Vector2.Distance(transform.position, pointA) > Vector2.Distance(transform.position, pointB)) ? pointA : pointB;
        }

        transform.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        //transform.GetComponent<Rigidbody2D>().linearVelocity = (targetPoint - (Vector2)transform.position) * moveSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move(pointA, pointB);

        if (health < 0) 
        {
            Death();
        }
    }
}
