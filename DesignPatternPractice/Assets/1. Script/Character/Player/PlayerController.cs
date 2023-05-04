using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    
    private Rigidbody _rigid;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigid.velocity = _input.PrimitiveInputVec * 2f;
    }
}
