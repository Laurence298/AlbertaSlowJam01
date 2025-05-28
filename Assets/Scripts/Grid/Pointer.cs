using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    public GameObject pointerPng;
    [SerializeField]
    private LayerMask detectionLayer;

    private Vector3 lastPosition;

    

    public Vector3 GetSelectedMapPosition()
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

        return lastPosition;
    }

 
}

