using System;
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


    
    [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;
    private bool isHoldingInput;


    public void SetUpPointer(Camera camera)
    {
        cam = camera;
        ClickAction.Enable();
        ClickAction.started += ClickActionOnperformed;
        soUIEvents.OnClickUnitUI += SoUIEventsOnOnClickUnitSoUI;
        soUIEvents.OnPaintSelectedUI += SoUIEventsOnOnPaintSelectedSoUI;
    }

    private void SoUIEventsOnOnPaintSelectedSoUI()
    {
        PointerState = PointerStates.Greening;
        
    }

    private void SoUIEventsOnOnClickUnitSoUI(UnitType arg0)
    {
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
                
                GridManager.Instance.PlaceUnitAtPointer(avaliableUnits);
                PointerState = PointerStates.Navigation;
                break;
            case PointerStates.Greening:
                    GridManager.Instance.PlaceTileAtPointer( );
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

