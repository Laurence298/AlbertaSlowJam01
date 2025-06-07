using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : Abstract_Enemy
{
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
        //transform.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        //Keeps it from rotating when moving across the spline
        transform.rotation = Quaternion.identity;

        //If health is reduced to zero or less, kill
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
