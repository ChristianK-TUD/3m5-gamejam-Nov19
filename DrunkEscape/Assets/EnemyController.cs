using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public Vector3[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private ConeCollider perception;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        perception = GetComponent<ConeCollider>();
        GotoNextPoint();
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
        this.enabled = false;
        Debug.Log("Caught!");
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
