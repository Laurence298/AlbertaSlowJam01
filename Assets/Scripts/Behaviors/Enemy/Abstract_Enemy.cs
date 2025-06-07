using UnityEngine;

public abstract class Abstract_Enemy : MonoBehaviour
{
    //Values
    public float health,
                 moveSpeed,
                 maxHealth;
    public StatusEffects status;
    public enum StatusEffects
    {
        Nothing, Stun
    }

    //When the enemy dies
    public abstract void Death();

    //When the enemy attack the player tree
    public abstract void Attack();

    //Applies debuffs 
    public abstract void ApplyDebuff(StatusEffects debuff, float duration);

    public abstract void RemoveDebuff(StatusEffects debuff);


}
