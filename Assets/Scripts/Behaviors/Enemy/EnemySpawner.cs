using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    //WIP
    GameObject enemy;
    public float spawnFreq, timeDelay;
    public int maxSpawnNum, spawnedCount;
    public List<GameObject> enemySpawnList, spawnedEnemies;
    public SplineContainer spline;


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
            enemy = GameObject.Instantiate(enemySpawnList.Last(), transform.position, transform.rotation);
            enemy.gameObject.GetComponent<SplineAnimate>().Container = spline;
            enemy.gameObject.GetComponent<SplineAnimate>().MaxSpeed = enemy.GetComponent<EnemyScript>().moveSpeed;
            enemy.gameObject.GetComponent<SplineAnimate>().Play();
            enemy.gameObject.name += " " + spawnedCount;
            spawnedCount++;
            spawnedEnemies.Add(enemy);
        }
    }

    private void ClearSpawnedList() 
    {
        spawnedEnemies.RemoveAll(e => e.gameObject == null);

    }
}
