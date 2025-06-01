using System;
using Grid;
using Grid.Tests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using TileData = Grid.TileData;

public class Pointer : MonoBehaviour
{
    public GameObject pointerPng;
    [SerializeField]
    private LayerMask detectionLayer;
    private Vector3 lastPosition;
    
    public InputAction ClickAction;
    public PointerStates PointerState; 
    
    public SOGridEvents gridEvents;

    public UnitList avaliableUnits;
    public TileData Greenery;
    private void Awake()
    {
        ClickAction.Enable();
        ClickAction.performed += ClickActionOnperformed;
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
            case PointerStates.TreePlacement:
                GridManager.Instance.AssignUnit(avaliableUnits.units[0]);
                break;
            case PointerStates.Naturing:
                GridManager.Instance.PaintTile(Greenery.tiles[Random.Range(0, Greenery.tiles.Length)] );
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
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
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
    TreePlacement,
    Naturing
}

