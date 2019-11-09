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
       camera_offset = camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       is_forward = Input.GetKey(KeyCode.W);
       is_right = Input.GetKey(KeyCode.D);
       is_left = Input.GetKey(KeyCode.A);
       is_backward = Input.GetKey(KeyCode.S);

       move();

       relocate_camera();
    }

    void move()
    {
       var movement = Vector3.zero;
       if (is_forward) movement.z += speed;
       if (is_right) movement.x += speed;
       if (is_left) movement.x -= speed;
       if (is_backward) movement.z -= speed;

       movement = movement.normalized * speed * Time.deltaTime;

       this.transform.position += movement;
    }

    void relocate_camera()
    {
       camera.transform.position = transform.position + camera_offset;
    }
   
    Vector3 camera_offset;

    bool is_forward;
    bool is_right;
    bool is_left;
    bool is_backward;

    public GameObject camera;

    public float speed;

    public Transform target;
}
