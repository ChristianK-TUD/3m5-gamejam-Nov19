﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Open()
    {
       open = true;
       Debug.Log("Door opened!");
    }

    private bool isOpen()
    {
       return open;
    }

    public bool open;
}
