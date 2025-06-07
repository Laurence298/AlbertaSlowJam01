using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Behaviors;
using GameFlow;
using Grid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    //WIP
    GameObject enemy;
    public WaveDataSo sowaveData;
    public int currentWave;
    public SplineContainer spline;
    public List<Vector3> paths;
    public List<BezierKnot> bezierKnot; 
    
    private float timeDelay;
    private WaveController waveController;

    public enum EnemyType 
    {
        basic,
        light,
        heavy
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitSpawner(WaveController waveController, WaveDataSo sowaveData)
    {
        this.sowaveData = sowaveData;
        this.waveController = waveController;

        foreach (var data in sowaveData.waves)
        {
            data.spawnedCount = 0;
            data.waveEnemys.spawnedEnemies.Clear();
        }
    }
    void Start()
    {
        transform.parent = null;
        paths = new List<Vector3>();
        timeDelay = 0;
    }

    public void InitROute(SplineContainer spline)
    {
        
       this.spline = spline;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(waveController == null)
            return;
        
        if(!waveController.IsWaveInProgress())
            return;

        if (Time.time > timeDelay) 
        {

            timeDelay = Time.time +  sowaveData.waves[currentWave].spawnFreq;
         EnemySpawn();
        }
       


      //  ClearSpawnedList();

    }

    private void EnemySpawn() 
    {

        if (sowaveData.waves[waveController.CurrentWaves].waveEnemys.spawnedEnemies.Count < sowaveData.waves[waveController.CurrentWaves].maxSpawnNum)
        {
            enemy = GameObject.Instantiate(sowaveData.waves[waveController.CurrentWaves].waveEnemys.enemySpawnList[0], transform.position, transform.rotation);
            enemy.gameObject.GetComponent<SplineAnimate>().Container = spline;
            enemy.gameObject.GetComponent<SplineAnimate>().MaxSpeed = enemy.GetComponent<EnemyScript>().moveSpeed;
            enemy.gameObject.GetComponent<SplineAnimate>().Play();
            enemy.gameObject.name = "Badguy #" + sowaveData.waves[waveController.CurrentWaves].spawnedCount;
            sowaveData.waves[waveController.CurrentWaves].spawnedCount++;
            sowaveData.waves[waveController.CurrentWaves].waveEnemys.spawnedEnemies.Add(enemy);
        }
    }
    
    public bool AreAllEnemiesDead()
    {
        if (sowaveData.waves[waveController.CurrentWaves].waveEnemys.enemySpawnList == null || sowaveData.waves[waveController.CurrentWaves].maxSpawnNum == 0)
            return true;

        foreach (var obj in sowaveData.waves[waveController.CurrentWaves].waveEnemys.spawnedEnemies)
        {
            if (obj != null) return false;
        }
        
        if(sowaveData.waves[waveController.CurrentWaves].maxSpawnNum == sowaveData.waves[waveController.CurrentWaves].spawnedCount)
            return true;
        else
            return false;
    }


   
}

[System.Serializable]
public class ListOfWaves
{
    public float spawnFreq, timeDelay;
    public int maxSpawnNum, spawnedCount;
    public WaveEnemys waveEnemys;
}

[System.Serializable]
public class WaveEnemys
{
    public List<GameObject> enemySpawnList, spawnedEnemies;
}
