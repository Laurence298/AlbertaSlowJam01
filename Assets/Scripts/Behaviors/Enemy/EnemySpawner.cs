using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //WIP
    GameObject enemy;
    public List<GameObject> enemyList;
    public float spawnFreq, timeDelay;

    public enum EnemyType 
    {
        basic,
        light,
        heavy
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeDelay) 
        {
            timeDelay = Time.time + spawnFreq;
            EnemySpawn();
        }
    }

    private void EnemySpawn() 
    {
        enemy = GameObject.Instantiate(enemyList.First(), transform.position, transform.rotation);
        enemy.GetComponent<EnemyScript>().pointA = new Vector2(-4f, -3.5f);
        enemy.GetComponent<EnemyScript>().pointB = new Vector2(5f, -3.5f);
    }
}
