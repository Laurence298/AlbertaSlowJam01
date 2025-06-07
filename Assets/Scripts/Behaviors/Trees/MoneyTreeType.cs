using UnityEngine;

public class MoneyTree : Abstract_Tree
{
    public float incTotal, incPerTick;
    public override void Attack(GameObject targetObj)
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        incTotal = 0;
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackDelay)
        {
            incTotal += incPerTick;
            attackDelay = Time.time + attackFrequency;

        }
    }
}
