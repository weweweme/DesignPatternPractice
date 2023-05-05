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
    public bool IsRun { get; private set; }

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
    }

    private void SetMovementAnimation()
    {
        IsMoving = (_input.PrimitiveInputVec != Vector3.zero) ? true : false;
        _bodyAnimator.SetBool(PlayerAnimationHashLiteral.IsMoveState, IsMoving);
        _bodyAnimator.SetFloat(PlayerAnimationHashLiteral.Horizontal, _input.PrimitiveInputVec.x);
        _bodyAnimator.SetFloat(PlayerAnimationHashLiteral.Vertical, _input.PrimitiveInputVec.z);
        
        SetMovementSpeed?.Invoke();

        if (_input.PrimitiveInputVec.x != 0 && _input.PrimitiveInputVec.z == 0)
        {
            _bodyAnimator.gameObject.transform.rotation = Quaternion.Euler(0f, 60f, 0f);
        }
        else
        {
            _bodyAnimator.gameObject.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        }
    }

    private void SetRunAnimation()
    {
        IsRun = !IsRun;
        _bodyAnimator.SetBool(PlayerAnimationHashLiteral.IsRun, IsRun);
        SetMovementSpeed?.Invoke();
    }

    private void OnDestroy()
    {
        _input.GetCurrentMovementState -= SetMovementAnimation;
        _input.GetCurrentRunState -= SetRunAnimation;
    }        
}
