using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {
      _mr = GetComponent<MeshRenderer>();
   }

   // Update is called once per frame
   void Update()
   {
   }

   void OnTriggerEnter(Collider collider)
   {
      if (collider.name == "Player") _mr.enabled = false;
   }
   void OnTriggerExit(Collider collider)
   {
      if (collider.name == "Player") _mr.enabled = false;
   }

   private MeshRenderer _mr;
}