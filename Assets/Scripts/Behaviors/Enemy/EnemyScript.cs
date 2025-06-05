using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : Abstract_Enemy
{
    public Vector2 targetPoint, pointA, pointB;
    //public List<Vector2> movePoints;
    public LinkedList<Vector2> movePoints;
    public float attackRange, attackDamage, attackTime, attackDelay;
    public RaycastHit2D attackHit;
    public LayerMask hitLayerMask;

    public override void Attack()
    {

        attackHit = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, hitLayerMask);
        if (attackHit.collider && Time.time > attackTime) 
        {
            attackTime = Time.time + attackDelay;
            attackHit.collider.GetComponent<PlayerHealthTest>().TakeDamage(attackDamage);
        }
    }

    public override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        transform.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
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
        targetPoint = GameObject.Find("PlayerHealthTest").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Move(pointA, pointB);
        Attack();


        //General Movement?
        if (Vector2.Distance(transform.position, targetPoint) >= 1f)
        {
            Move();
        }

        if (health <= 0) 
        {
            Death();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position, attackRange);
    }

}
