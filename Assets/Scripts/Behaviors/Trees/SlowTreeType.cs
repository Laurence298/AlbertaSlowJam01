using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SlowTreeType : Abstract_Tree
{
    public float projectileSpeed, effectDuration;
    [SerializeField] GameObject bulletType;
    private SlowingProjectile projectile;
    [SerializeField] LayerMask targetLayers;

    public List<GameObject> enemiesInRange;


    private void Start()
    {
        //IDK put something here

    }

    private void Update()
    {

        enemiesInRange = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0, targetLayers).Select(enemyInRange => enemyInRange.transform.gameObject).Distinct<GameObject>().ToList();

        //Sorts the list according to how close the enemy is (closest ones get priority)
        enemiesInRange.OrderByDescending(enemyInRange => enemyInRange.gameObject.GetComponent<SplineAnimate>().ElapsedTime).ToList();

        if (enemiesInRange.Count > 0 && Time.time > attackDelay)
        {
            attackDelay = Time.time + attackFrequency;
            Attack(enemiesInRange.Last());
        }

    }


    public override void Attack(GameObject targetObj)
    {
        //Todo: Make it so the projectile knows that it's coming from this script
        //NEED: The projectile to fly at the direction it's heading
        //NEED: Delay the attack, can't fire every frame.
        projectile = Instantiate(bulletType, transform.position, transform.rotation, transform).GetComponent<SlowingProjectile>();
        projectile.targetPos = targetObj.transform.position;
        projectile.damage = attackDmg;
        projectile.projectileSpeed = projectileSpeed;
        projectile.projectileRange = attackRange;
        projectile.effectDuration = effectDuration;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
