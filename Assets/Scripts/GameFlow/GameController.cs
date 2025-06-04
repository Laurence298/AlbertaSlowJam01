using System;
using System.Collections;
using GameFlow;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float preapingCountdown;
    public float countdown;
    
    
    public WaveController waveController;

    
    public GameState gameState;
    public Coroutine gameCoroutine;

    private void Start()
    {
        gameState = GameState.Preaping;
        countdown = preapingCountdown;

        gameCoroutine = StartCoroutine(Preaping());
    }



    public IEnumerator Preaping()
    {
        gameState = GameState.Preaping;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }
        
        gameCoroutine = StartCoroutine(Defending());

    }

    public IEnumerator Defending()
    {
        gameState = GameState.Defending;

        yield return new WaitUntil(() => !waveController.CheckForEnemies());
        countdown = preapingCountdown;
        waveController.NextWave();
        
        if(!waveController.LevelCompleted())
            gameCoroutine = StartCoroutine(Preaping());
        else if(waveController.LevelCompleted())
            gameCoroutine = StartCoroutine(LevelComplete());

    }

    public IEnumerator GameOver()
    {
        gameState = GameState.GameOver;
        yield return null;

    }

    public IEnumerator LevelComplete()
    {
        gameState = GameState.LevelComplete;
        yield return null;

        
    }
}

public enum GameState
{
    Preaping,
    Defending,
    LevelComplete,
    GameOver
}
