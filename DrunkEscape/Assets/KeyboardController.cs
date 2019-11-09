using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       var forward_key = Input.GetKey(KeyCode.W);

       if (forward_key)
       {
          transform.Translate(0.1f, 0, 0);
       }
    }

    public GameObject source;

    public BoxCollider box_collider;

    public int speed;

    public Transform target;
}
