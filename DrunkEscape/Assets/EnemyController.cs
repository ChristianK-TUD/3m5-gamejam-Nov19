using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Vector3[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private SphereCollider perception;
    private bool playerInRange = false;
    public Transform playerPos;
    private RaycastHit hit;
    public LayerMask raycastLayer;

    private int debug;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        perception = GetComponent<SphereCollider>();
        GotoNextPoint();
        Debug.Log(perception.ToString());
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint];
        destPoint = (destPoint + 1) % points.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "monk")
        {
            playerPos = other.gameObject.transform;
            playerInRange = true;
            //Debug.Log("player in collider");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "monk")
            playerInRange = false;
        //Debug.Log("player leaves collider...");
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        if (Physics.Raycast(transform.position, playerPos.position - this.transform.position, out hit, 40, raycastLayer.value))
        {
            if (hit.collider.gameObject.name == "monk")
            {
                Vector3 angle = new Vector3();
                angle = (playerPos.transform.position - transform.position).normalized;
                Debug.Log(Vector3.Dot(angle, transform.forward) > 0);
                if (Vector3.Dot(angle, transform.forward) > 0)
                { // Player is in front of monk and caught
                    Debug.Log("Caught!");
                    return;
                }
                if (playerInRange) // Player in range but not visible in front - hearing?
                {

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, playerPos.transform.position);
    }
}
