using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using AndroidActivityIndicatorStyle = UnityEngine.AndroidActivityIndicatorStyle;
using Random = System.Random;
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

       const float maxOffset = 2;
       const float minOffset = -2;

       var r = new Random();

       _drunkRandomOffset += ((float)r.NextDouble() - 0.5f) * Time.deltaTime;

       if (_drunkRandomOffset > maxOffset) _drunkRandomOffset = maxOffset;
       if (_drunkRandomOffset < minOffset) _drunkRandomOffset = minOffset;

       //rotate randomly when drunk
       if (_isForward || _isBackward) movement.x += inventory.getAlcoholLevel() * Mathf.Sin(Time.time * 4 + _drunkRandomOffset + 1) * Thrust;

       if (_isLeft || _isRight) movement.z += inventory.getAlcoholLevel() * Mathf.Sin(Time.time * 4 + _drunkRandomOffset) * Thrust;
       
       _rb.AddForce(movement);

       if (Math.Abs(_rb.velocity.magnitude) > 0.1) transform.forward = _rb.velocity;
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

    void OnTriggerEnter(Collider collider)
    {
       Debug.Log(collider.name);
       if (collider.name == "Key")
       {
          inventory.AddItem(key, "Key");
       }
       else if (collider.name.Contains("wooden_door"))
       {
          if (inventory.RemoveItem("Key"))
          {
             collider.gameObject.GetComponentInParent<DoorScript>().Open();
          }
       }
       else if (collider.name == "Booze")
       {
          inventory.AddAlcohol(0.5f);
       }
    }

    private float _drunkRandomOffset;

    private Vector3 _cameraOffset;

    private bool _isForward;
    private bool _isRight;
    private bool _isLeft;
    private bool _isBackward;

    public GameObject Camera;
    public Inventory inventory;
    private Rigidbody _rb;

    public Sprite key;

    public float MaxSpeed;
    public float MinSpeed;
    public float Thrust;
    public float SlowDownFactor;
}
