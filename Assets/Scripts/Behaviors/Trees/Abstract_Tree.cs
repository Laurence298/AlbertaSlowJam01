using UnityEngine;

public abstract class Abstract_Tree : MonoBehaviour
{
    //Must haves for Any tree Unit
    public float attackRange, attackFrequency, attackDelay, attackDmg;

    public abstract void Attack(GameObject targetObj);

}
