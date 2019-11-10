using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
       if (open) Open();
       else Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
       open = true;
       _rb.constraints = RigidbodyConstraints.None;
    }

    public void Close()
    {
       open = false;
       _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public bool isOpen()
    {
       return open;
    }

    private Rigidbody _rb;
    public bool open;
}
