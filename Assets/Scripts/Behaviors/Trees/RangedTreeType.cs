using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedTreeType : Abstract_Tree
{
    public float projectileSpeed;
    [SerializeField] GameObject bulletType;
    public GameObject projectile;
    public List<RaycastHit2D> enemyDetect;
    [SerializeField] LayerMask targetLayers;

    //public GameObject targetObj;

    public List<GameObject> enemiesInRange;


    private void Start()
    {
        //IDK put something here
    }

    private void Update()
    {

        enemiesInRange = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0, targetLayers).Select(enemyInRange => enemyInRange.transform.gameObject).Distinct<GameObject>().ToList();
        

        if (enemiesInRange.Count > 0 && Time.time > attackDelay)
        {
            //Sorts the list according to how close the enemy is (closest ones get priority)
            enemiesInRange.OrderByDescending(enemyInRange => Vector2.Distance(enemyInRange.gameObject.transform.position, transform.position)).ToList();
            attackDelay = Time.time + attackFrequency;
            Attack(enemiesInRange.First());
        }

    }


    public override void Attack(GameObject targetObj)
    {
        //Todo: Make it so the projectile knows that it's coming from this script
        //NEED: The projectile to fly at the direction it's heading
        //NEED: Delay the attack, can't fire every frame.
        projectile = Instantiate(bulletType, transform.position, transform.rotation, transform);
        projectile.GetComponent<BasicProjectile>().targetPos = targetObj.transform.position;
        projectile.GetComponent<BasicProjectile>().damage = attackDmg;
        projectile.GetComponent<BasicProjectile>().projectileSpeed = projectileSpeed;
        projectile.GetComponent<BasicProjectile>().projectileRange = attackRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
