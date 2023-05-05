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

    private float _currentMoveSpeed;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animationController = GetComponent<PlayerAnimationController>();
        
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _animationController.SetMovementSpeed -= CurrentMovementSpeed;
        _animationController.SetMovementSpeed += CurrentMovementSpeed;
    }

    private void CurrentMovementSpeed()
    {
        if ( !_animationController.IsRun)
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
    }

    private void GroundMovement()
    {
        _rigid.velocity = _input.PrimitiveInputVec * _currentMoveSpeed;
    }

    private void OnDestroy()
    {
        _animationController.SetMovementSpeed -= CurrentMovementSpeed;
    }
}
