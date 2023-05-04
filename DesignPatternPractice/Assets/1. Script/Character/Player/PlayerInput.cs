using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action GetCurrentMovementState;
    
    public Vector3 PrimitiveInputVec { get; private set; }
    
    // 플레이어 이동
    public void OnMovement(InputAction.CallbackContext context)
    {
        PrimitiveInputVec = context.ReadValue<Vector3>();
        GetCurrentMovementState?.Invoke();
    }
}
