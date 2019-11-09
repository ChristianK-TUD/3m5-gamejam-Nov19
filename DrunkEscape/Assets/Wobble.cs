using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       starty = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       var offset = Vector3.zero;
       offset.y = waveHeight * Mathf.Sin( ( Time.time * waveSpeed ) );
       offset.y += starty;

       offset.x = transform.position.x;
       offset.z = transform.position.z;

       transform.position = offset;
    }

    public float waveHeight;
    public float waveSpeed;

    private float starty;
}
