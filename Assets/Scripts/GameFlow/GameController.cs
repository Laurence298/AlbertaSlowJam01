using System;
using System.Collections;
using Behaviors;
using GameFlow;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
     public float timeUntilRoundStart;
    private float countdown;
    
    
    public WaveController waveController;

    
    public GameState gameState;
    public Coroutine gameCoroutine;
    public SoUIEvents uiEvents;



    public void StartGame()
    {
        gameState = GameState.Preaping;
        countdown = timeUntilRoundStart;
        uiEvents.RaiseTimerChanged(countdown);
        uiEvents.OnStartPressed += UiEventsOnOnStartPressed;
        gameCoroutine = StartCoroutine(Preaping());
        waveController.ReadyUp();

    }

    private void UiEventsOnOnStartPressed()
    {
        if (gameState == GameState.Preaping)
        {
            countdown = 0;
            uiEvents.RaiseTimerChanged(countdown);
        }
    }


    public IEnumerator Preaping()
    {
        waveController.StartWave(false);

        gameState = GameState.Preaping;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            uiEvents.RaiseTimerChanged(countdown);
            yield return null;
        }
        
        gameCoroutine = StartCoroutine(Defending());

    }

    public IEnumerator Defending()
    {
        gameState = GameState.Defending;
        waveController.StartWave(true);

        yield return new WaitUntil(() => waveController.AreAllEnemiesDead());
        countdown = timeUntilRoundStart;
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
