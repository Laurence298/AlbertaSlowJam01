using System;
using AYellowpaper.SerializedCollections;
using GameFlow;
using Grid;
using Grid.Tests;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using TileData = Grid.TileData;

public class Pointer : MonoBehaviour
{
    public Camera cam;
    public GameObject pointerPng;
    [SerializeField]
    private LayerMask detectionLayer;
    private Vector3 lastPosition;
    
    public InputAction ClickAction;
    public InputAction ActionSwitch;

    
    public PointerStates PointerState; 
    
    public SOGridEvents gridEvents;

    // pointer things
    public UnitType avaliableUnits;
    
    // green tile
    public TileData Greenery;
    public MoneyCounter moneyCounter;

    
    [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;
    private bool isHoldingInput;
    

    private GameObject unit;
    public Tutorail tutorail;

    public void SetUpPointer(Camera camera)
    {
        cam = camera;
        ClickAction.Enable();
        moneyCounter.StartingAMoung();
        ClickAction.started += ClickActionOnperformed;
        soUIEvents.OnClickUnitUI += SoUIEventsOnOnClickUnitSoUI;
        soUIEvents.OnPaintSelectedUI += SoUIEventsOnOnPaintSelectedSoUI;
    }

    private void SoUIEventsOnOnPaintSelectedSoUI()
    {
        PointerState = PointerStates.Greening;
        tutorail.TutorialOneDone = true;

    }

    private void SoUIEventsOnOnClickUnitSoUI(UnitType arg0)
    {
        tutorail.TutorialThreeDone = true;

        avaliableUnits = arg0;
        PointerState = PointerStates.UnitPlacement;
    }

    public bool OverUi()
    {
       return EventSystem.current.IsPointerOverGameObject();
    }

    private void Start()
    {
        
    }

    private void ClickActionOnperformed(InputAction.CallbackContext obj)
    {
        switch (PointerState)
        {
            case PointerStates.Navigation:
                break;
            case PointerStates.UnitPlacement:
                if(OverUi())
                    return;
                
                if(!moneyCounter.CanPurchaseUnit(avaliableUnits))
                    return;

                unit =   GridManager.Instance.PlaceUnitAtPointer(avaliableUnits);
                if(unit != null)
                     moneyCounter.PurchaseUnit(avaliableUnits);
                tutorail.TutorialFourDone = true;

                PointerState = PointerStates.Navigation;
                break;
            case PointerStates.Greening:
                if(OverUi())
                    return;
                if(!moneyCounter.CanPurchaseGrass())
                    return;
                tutorail.TutorialTwoDone = true;

                GridManager.Instance.PlaceTileAtPointer( );
                moneyCounter.PurchaseGrass();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        GetSelectedMapPosition();
    }

    public void GetSelectedMapPosition()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y);
        Vector2 origin = new Vector2(worldPosition.x, worldPosition.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, Mathf.Infinity, detectionLayer);

        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }
        
        gridEvents.OnMousePositionChanged(lastPosition);

    }

 
}

public enum PointerStates
{
    Navigation,
    UnitPlacement,
    Greening
}

