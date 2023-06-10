using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : MonoBehaviour
{
    [Header("Follow Values")]
    public float startFollowDist = 1f;
    public float stopFollowDist = 6f;
    public float dogSpeed = 3f;

    public GameObject player;

    private bool following = false;
    private float distanceToPlayer;
    private Animator anim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        following = false;
        agent.speed = dogSpeed;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Sets the dog's animation to idle
        UpdateIdleState();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Begins following the player if the distance between them is less than 1
        if (distanceToPlayer < startFollowDist)
        {
            following = true;
        }
        else if (distanceToPlayer > stopFollowDist)
        {
            following = false;
        }

        // Performs an action based on whether or not the dog is following the player
        if (following)
        {
            UpdateFollowState();
            //Debug.Log("Following");
        }
        else
        {
            UpdateIdleState();
            //Debug.Log("Idle");
        }
    }

    // Performs all the dogs actions when following the player
    private void UpdateFollowState()
    {
        if (distanceToPlayer < 1)
            anim.SetInteger("dogState", 0);
        else
        {
            agent.SetDestination(player.transform.position);
            anim.SetInteger("dogState", 1);
        }
    }

    // Performs all the dogs actions when in the idle state
    private void UpdateIdleState()
    {
        anim.SetInteger("dogState", 0);
        agent.SetDestination(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}
