﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<Text>().text = Stats.Points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
