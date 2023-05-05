using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // 현재 Movement상태인지 호출하는 이벤트
    public event Action GetCurrentMovementState;
    // 현재 Run상태인지 호출하는 이벤트
    public event Action GetCurrentRunState;
    
    public Vector3 PrimitiveInputVec { get; private set; }
    
    // 플레이어 이동
    public void OnMovement(InputAction.CallbackContext context)
    {
        PrimitiveInputVec = context.ReadValue<Vector3>();
        GetCurrentMovementState?.Invoke();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started || context.canceled)
        {
            GetCurrentRunState?.Invoke();
        }
    }
}
