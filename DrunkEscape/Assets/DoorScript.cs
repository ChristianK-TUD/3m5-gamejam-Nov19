using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
       open = true;

       _rb.constraints = RigidbodyConstraints.FreezePosition | 
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY;
       Debug.Log("Door opened!");
    }

    private bool isOpen()
    {
       return open;
    }

    private Rigidbody _rb;
    public bool open;
}
