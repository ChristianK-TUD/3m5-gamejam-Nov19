﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EntityController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       starty = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       if (wobble) Wobble();
       if (rotate) Rotate();
    }

    void OnTriggerEnter(Collider collider)
    {
       if (collider.name == "monk")
          gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Wobble()
    {
       var offset = Vector3.zero;
       offset.y = waveHeight * Mathf.Sin( ( Time.time * waveSpeed ) ) + starty;

       offset.x = transform.position.x;
       offset.z = transform.position.z;

       transform.position = offset;
    }

    void Rotate()
    {
       transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public bool wobble;
    public float waveHeight;
    public float waveSpeed;

    public bool rotate;
    public float rotationSpeed;

    private float starty;
}
