using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //WIP
    GameObject enemy;
    public float spawnFreq, timeDelay;
    public int maxSpawnNum, spawnedCount;
    public List<GameObject> enemySpawnList, spawnedEnemies;


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


        ClearSpawnedList();

    }

    private void EnemySpawn() 
    {

        if (spawnedEnemies.Count < maxSpawnNum)
        {
            enemy = GameObject.Instantiate(enemySpawnList.First(), transform.position, transform.rotation);
            enemy.GetComponent<EnemyScript>().pointA = new Vector2(-4f, -3.5f);
            enemy.GetComponent<EnemyScript>().pointB = new Vector2(5f, -3.5f);
            enemy.gameObject.name = "Badguy #" + spawnedCount;
            spawnedCount++;
            spawnedEnemies.Add(enemy);
        }
    }

    private void ClearSpawnedList() 
    {
        spawnedEnemies.RemoveAll(e => e.gameObject == null);

    }
}
