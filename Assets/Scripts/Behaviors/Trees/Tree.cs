using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    //Values
    public double initUpgradePrice, 
                  nextUpgradePrice;


    public float attackRange,
                 attackSpeed,
                 attackDmg;

    //Attack
    public abstract void Attack();

    /*
     * Need: initUpgradePrice, nextUpgradePrice
     * Note: nextUpgrade should go up exponentially
     */
    public abstract void Upgrade();

    //Calculates how much the next upgrade should cost
    public abstract double UpgradeCalc(double initUpgrade);


}
