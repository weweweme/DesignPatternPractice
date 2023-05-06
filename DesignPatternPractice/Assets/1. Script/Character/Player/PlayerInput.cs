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
    
    // RunDiveRoll 트리거를 호출하는 이벤트
    public event Action GetRunDiveRollTrigger;

    [field: Header("Player MovementState")]
    public bool IsRun { get; private set; }

    public bool IsRoll { get; private set; }

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
            IsRun = !IsRun;
            GetCurrentRunState?.Invoke();
        }
    }

    public void OnRunDiveRoll(InputAction.CallbackContext context)
    {
        if (context.started && PrimitiveInputVec.z > 0f && IsRun )
        {
            GetRunDiveRollTrigger?.Invoke();
        }
    }

    public void SetActiveIsRollState()
    {
        IsRoll = !IsRoll;
    }
}
