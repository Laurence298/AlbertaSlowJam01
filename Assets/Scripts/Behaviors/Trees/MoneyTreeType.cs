using System;
using UI;
using UnityEngine;

public class MoneyTree : Abstract_Tree
{
    public float incTotal, incPerTick;
    public MoneyCounter moneyCounter;
    public SoUIEvents soUIEvents;
    public bool canGenerate;
    public int MONEYGENERATED;
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
        soUIEvents.OnGameStateChanged += SoUIEventsOnOnGameStateChanged;
    }

    private void OnDestroy()
    {
        soUIEvents.OnGameStateChanged -= SoUIEventsOnOnGameStateChanged;

    }

    private void SoUIEventsOnOnGameStateChanged(GameState arg0)
    {
        if (arg0 == GameState.Defending)
        {
            canGenerate = true;
            incTotal = 0;

        }
        else
        {
            canGenerate = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackDelay && canGenerate && incTotal <= 3)
        {
            incTotal += incPerTick;
            moneyCounter.AddScore(MONEYGENERATED);
            attackDelay = Time.time + attackFrequency;

        }
    }
}
