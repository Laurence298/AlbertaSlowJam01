using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Values
    public float health, 
                 speed, 
                 maxHealth,
                 defense;

    //When the enemy moves
    public abstract void Move();

    //When the enemy dies
    public abstract void Death();

    //When the enemy attack the player tree
    public abstract void Attack();


}
