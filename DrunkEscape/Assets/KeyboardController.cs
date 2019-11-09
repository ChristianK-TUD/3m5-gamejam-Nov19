﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class KeyboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
       _cameraOffset = Camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       _isForward = Input.GetKey(KeyCode.W);
       _isRight = Input.GetKey(KeyCode.D);
       _isLeft = Input.GetKey(KeyCode.A);
       _isBackward = Input.GetKey(KeyCode.S);

       move();

       relocate_camera();

       limit_speed();
    }

    void move()
    {
       var movement = Vector3.zero;
       if (_isForward) movement.z += 1;
       if (_isRight) movement.x += 1;
       if (_isLeft) movement.x -= 1;
       if (_isBackward) movement.z -= 1;

       movement = movement.normalized * Thrust;

       if (Math.Abs(movement.magnitude) < 0.001)
       {
          //slow down
          if (_rb.velocity.magnitude < MinSpeed)
          {
             _rb.velocity = Vector3.zero;
          }
          else
          {
             movement = - _rb.velocity.normalized * SlowDownFactor;
          }
       }

       _rb.AddForce(movement);
    }

    void relocate_camera()
    {
       Camera.transform.position = transform.position + _cameraOffset;
    }

    void limit_speed()
    {
       if (_rb.velocity.magnitude > MaxSpeed)
       {
          _rb.velocity = _rb.velocity.normalized * MaxSpeed;
       }
    }
   
    private Vector3 _cameraOffset;

    private bool _isForward;
    private bool _isRight;
    private bool _isLeft;
    private bool _isBackward;

    public GameObject Camera;
    private Rigidbody _rb;

    public float MaxSpeed;
    public float MinSpeed;
    public float Thrust;
    public float SlowDownFactor;
}
