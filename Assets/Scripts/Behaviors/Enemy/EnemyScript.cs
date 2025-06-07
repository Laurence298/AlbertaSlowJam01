using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyScript : Abstract_Enemy
{
    public float attackRange, attackDamage, attackTime, attackDelay;
    public RaycastHit2D attackHit;
    public LayerMask hitLayerMask;


    //USED FOR DEBUFF Countdowns
    private float endTime, timeUntilZero;



    public override void Attack()
    {

        attackHit = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, hitLayerMask);
        if (attackHit.collider && Time.time > attackTime) 
        {
            attackTime = Time.time + attackDelay;
            attackHit.collider.GetComponent<Players>().TakeDamage(attackDamage);
        }
    }

    public override void Death()
    {
        // 
        Destroy(this.gameObject);
    }


    public override void ApplyDebuff(StatusEffects debuff, float duration)
    {

        status = debuff;
        endTime = Time.time + duration;
    }



    //Currently Never called
    public override void RemoveDebuff(StatusEffects debuff)
    {
        status = StatusEffects.Nothing;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void statusRun() 
    {
        timeUntilZero = endTime - Time.time;
        if (timeUntilZero > 0f)
        {
            status = StatusEffects.Stun;
            this.GetComponent<SplineAnimate>().Pause();
        }
        else
        {
            status = StatusEffects.Nothing;
            this.GetComponent<SplineAnimate>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        statusRun();

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
