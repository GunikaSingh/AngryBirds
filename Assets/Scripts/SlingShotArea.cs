using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask slingMask;
    public bool WithinArea()
    { 
        Vector2 worldPos=Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Physics2D.OverlapPoint(worldPos,slingMask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
