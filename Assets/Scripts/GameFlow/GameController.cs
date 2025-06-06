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

    Players players;


    public void StartGame()
    {
        gameState = GameState.Preaping;
        countdown = timeUntilRoundStart;
        uiEvents.RaiseTimerChanged(countdown);
        uiEvents.OnStartPressed += UiEventsOnOnStartPressed;
        gameCoroutine = StartCoroutine(Preaping());
        waveController.ReadyUp();
        uiEvents.RaiseGameStateChanged(gameState);

    }

    public void GetPlayerHealth(Players player)
    {
        this.players = player;
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
        uiEvents.RaiseGameStateChanged(gameState);

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
        uiEvents.RaiseGameStateChanged(gameState);

        waveController.StartWave(true);
        
        

        yield return new WaitUntil(() => waveController.AreAllEnemiesDead() || players.currentHealth < 0);

        
        if(players.currentHealth <= 0)
            gameCoroutine = StartCoroutine(GameOver());
        else
        {
            if (!waveController.LevelCompleted())
            {
                countdown = timeUntilRoundStart;
                waveController.NextWave();
                gameCoroutine = StartCoroutine(Preaping());

            }
            else if(waveController.LevelCompleted())
                gameCoroutine = StartCoroutine(LevelComplete());
        }
      

    }

    public IEnumerator GameOver()
    {
        gameState = GameState.GameOver;
        uiEvents.RaiseGameStateChanged(gameState);

        yield return null;

    }

    public IEnumerator LevelComplete()
    {
        gameState = GameState.LevelComplete;
        uiEvents.RaiseGameStateChanged(gameState);

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
