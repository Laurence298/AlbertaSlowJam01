using UnityEngine;

public abstract class Abstract_Enemy : MonoBehaviour
{
    //Values
    public float health, 
                 moveSpeed, 
                 maxHealth,
                 defense;

    //When the enemy moves
    public abstract void Move();

    //When the enemy dies
    public abstract void Death();

    //When the enemy attack the player tree
    public abstract void Attack();


}
