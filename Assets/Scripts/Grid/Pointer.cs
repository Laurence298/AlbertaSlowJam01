using System;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    public GameObject pointerPng;
    [SerializeField]
    private LayerMask detectionLayer;

    private Vector3 lastPosition;
    
    
    public SOGridEvents gridEvents;

    private void Awake()
    {
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
    Placement
}

