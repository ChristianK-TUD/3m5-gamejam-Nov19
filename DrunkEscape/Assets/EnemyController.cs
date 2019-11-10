using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Patrouille patrouille;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool playerInRange = false;
    public Transform playerPos;
    private RaycastHit hit;
    public LayerMask raycastLayer;
    public float maxViewDistance = 20;
    public AudioClip soundChanting;
    private AudioSource audioSource;
    private int chantCooldown;
    public int minChantCooldown;
    public int maxChantCooldown;

    private int debug;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        audioSource = GetComponent<AudioSource>();
    }


    void GotoNextPoint()
    {
        if (patrouille.transforms.Length == 0)
            return;
        agent.destination = patrouille.transforms[destPoint].position;
        destPoint = (destPoint + 1) % patrouille.transforms.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "monk")
        {
            playerPos = other.gameObject.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "monk")
            playerInRange = false;
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        if (Physics.Raycast(getEyePosition(transform.position), getEyePosition(playerPos.position) - getEyePosition(this.transform.position), out hit, maxViewDistance, raycastLayer.value))
        {
            if (hit.collider.gameObject.name == "monk")
            {
                Vector3 angle = new Vector3();
                angle = (playerPos.transform.position - transform.position).normalized;
                Debug.Log(Vector3.Dot(angle, transform.forward) > 0);
                if (Vector3.Dot(angle, transform.forward) > 0)
                { // Player is in front of monk and caught --> GAME OVER
                    Debug.Log("Caught!");
                    SceneManager.LoadScene("MenuScene");
                    return;
                }
                if (playerInRange) // Player in range but not visible in front - hearing?
                {

                }
            }
        }

        // Sound playing
        if(audioSource.isPlaying)
        {
            return;
        }
        else
        {
            if(chantCooldown > 0)
            {
                if(chantCooldown == 1)
                {
                    audioSource.PlayOneShot(soundChanting);
                }
                chantCooldown--;
            }
            else
            {
                // Playing just finished
                chantCooldown = new System.Random().Next(minChantCooldown, maxChantCooldown);
            }
        }
    }

    private Vector3 getEyePosition(Vector3 groundPosition)
    {
        return new Vector3(groundPosition.x, groundPosition.y + 1.5f, groundPosition.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, playerPos.transform.position);
    }
}
