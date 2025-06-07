using System;
using System.Collections;
using GameFlow;
using Grid;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitatore : MonoBehaviour
{
    public GameObject GameMainMenu;
    public GameObject LevelUI;
    public SoUIEvents SoUIEvents;

    public GameObject pointer;
    public GameObject tileMap;
    public GameObject controllers;
    public Camera mainCamera;
    public GameObject Tutorial;
  

    private GridManager grid;
    private Pointer pointerScript;
    private Players playerScript;
    private Healthbar healthbarScript;
    
    private GameObject _MainMenu;
    private GameObject _LevelUI;
    private GameObject _Pointer;
    private GameObject _tileMap;
    private GameObject _controllers;
    private GameController _GameController;
    private GameTutorial _GameTutorial;
    private GameObject TutorialObject;


    private void Awake()
    {
        StartCoroutine(LoadLevel());
        SoUIEvents.OnGameStart += SoUIEventsOnOnGameStart;

    }

    private void SoUIEventsOnOnGameStart()
    {
        BeginGame();
    }

    private void Start()
    {
    }

    private IEnumerator LoadLevel()
    {
        _MainMenu = Instantiate(GameMainMenu);
        _LevelUI = Instantiate(LevelUI);
       
        
        _tileMap = Instantiate(tileMap);
        grid = tileMap.GetComponentInChildren<GridManager>();
        grid.Init();
        yield return new WaitUntil(() =>grid.collider != null);
        grid.SetUpGrid();

        playerScript = FindFirstObjectByType<Players>();
        healthbarScript = FindObjectOfType<Healthbar>();
        Debug.Log("finding Players");
        
        yield return new WaitUntil(() =>playerScript != null && healthbarScript != null);
        playerScript.SetHealthBar(healthbarScript);
        
        
        Debug.Log("finding pointer");
        _Pointer = Instantiate(pointer);
        pointerScript = _Pointer.GetComponent<Pointer>();
        pointerScript.SetUpPointer(mainCamera);
        _Pointer.SetActive(false);
        TutorialObject = Instantiate(Tutorial);
        _GameTutorial = Tutorial.GetComponent<GameTutorial>();
        Debug.Log("finding GameController");
        _controllers = Instantiate(controllers);
        _GameController = _controllers.GetComponent<GameController>();
        _GameController.GetPlayerHealth(playerScript);

        yield return new WaitUntil(()=> _GameController != null );
    }

    public void BeginGame()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _MainMenu.SetActive(false);
        
        _GameController.StartGame();
        _Pointer.SetActive(true);
        _LevelUI.SetActive(true);

        _GameTutorial.StartTutorial();
        yield return null;
  
        

    }

}
