using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Util;

public class PlayerAnimationController : MonoBehaviour
{
    public event Action SetMovementSpeed;
    
    private PlayerInput _input;
    private Animator _bodyAnimator;

    [field: Header("Player MovementState")]
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _bodyAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _input.GetCurrentMovementState -= SetMovementAnimation;
        _input.GetCurrentMovementState += SetMovementAnimation;
        _input.GetCurrentRunState -= SetRunAnimation;
        _input.GetCurrentRunState += SetRunAnimation;
        _input.GetRunDiveRollTrigger -= SetRunDiveRollAnimation;
        _input.GetRunDiveRollTrigger += SetRunDiveRollAnimation;
    }

    /// <summary>
    /// Movement애니메이션을 실행시키는 함수
    /// Movement에는 WalkState와 RunState가 존재한다
    /// 특별한 액션이 추가되지 않는다면 Default는 WalkState
    /// </summary>
    private void SetMovementAnimation()
    {
        IsMoving = (_input.PrimitiveInputVec != Vector3.zero) ? true : false;
        _bodyAnimator.SetBool(PlayerAnimationHashLiteral.IsMoveState, IsMoving);
        _bodyAnimator.SetFloat(PlayerAnimationHashLiteral.Horizontal, _input.PrimitiveInputVec.x);
        _bodyAnimator.SetFloat(PlayerAnimationHashLiteral.Vertical, _input.PrimitiveInputVec.z);
        
        SetMovementSpeed?.Invoke();
    }

    /// <summary>
    /// WalkState 중 실행되면 RunState로 진입하게 해주는 함수
    /// </summary>
    private void SetRunAnimation()
    {
        _bodyAnimator.SetBool(PlayerAnimationHashLiteral.IsRun, _input.IsRun);
        
        SetMovementSpeed?.Invoke();
    }

    /// <summary>
    /// RunState중 Roll을 실행시키는 함수
    /// Roll 애니메이션이 종료된 후 다시 RunState로 진입
    /// </summary>
    private void SetRunDiveRollAnimation()
    {
        _bodyAnimator.SetTrigger(PlayerAnimationHashLiteral.IsRunDiveRoll);
    }

    private void OnDestroy()
    {
        _input.GetCurrentMovementState -= SetMovementAnimation;
        _input.GetCurrentRunState -= SetRunAnimation;
        _input.GetRunDiveRollTrigger -= SetRunDiveRollAnimation;
    }                

}
