using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerAnimationController _animationController; 
        
    private PlayerStatus _status = new PlayerStatus();
    
    private Rigidbody _rigid;
    private Animator _bodyAnimator;

    private float _currentMoveSpeed;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animationController = GetComponent<PlayerAnimationController>();
        
        _rigid = GetComponent<Rigidbody>();
        _bodyAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _animationController.SetMovementSpeed -= CurrentMovementSpeed;
        _animationController.SetMovementSpeed += CurrentMovementSpeed;
    }

    private void CurrentMovementSpeed()
    {
        if ( !_input.IsRun )
        {
            _currentMoveSpeed = _status.WalkSpeed;
        }
        else
        {
            _currentMoveSpeed = _status.RunSpeed;
        }
    }
    
    private void FixedUpdate()
    {
        GroundMovement();
        CurrentBodyRotation();
    }

    /// <summary>
    /// 평지에서의 이동속도
    /// </summary>
    private void GroundMovement()
    {
        _rigid.velocity = _input.PrimitiveInputVec * _currentMoveSpeed;
    }

    /// <summary>
    /// 현재 Body 오브젝트의 Rotation을 결정하는 함수
    /// </summary>
    private void CurrentBodyRotation()
    {
        if (!_input.IsRoll)
        {
            if (_input.PrimitiveInputVec.x != 0 && _input.PrimitiveInputVec.z == 0)
            {
                _bodyAnimator.gameObject.transform.rotation = Quaternion.Euler(0f, 60f, 0f);
            }
            else
            {
                _bodyAnimator.gameObject.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
            }
        }
        else if (_input.IsRoll)
        {
            _bodyAnimator.gameObject.transform.rotation = _zeroQuaternion;
        }
    }

    private Quaternion _zeroQuaternion = Quaternion.Euler(0, 0, 0);
    private void OnDestroy()
    {
        _animationController.SetMovementSpeed -= CurrentMovementSpeed;
    }
}
