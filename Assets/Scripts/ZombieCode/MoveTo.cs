using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public GameObject targetObject;
    Transform goal;
    public float targetingRange = 18;
    public bool moving;
    public Animator animator;
    public float attackRange = 5;
    public float distance;
    UnityEngine.AI.NavMeshAgent agent;
    ZombieHealth zombieHealth;
    void Start()
    {
        goal = targetObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        moving = true;
        zombieHealth = GetComponent<ZombieHealth>();
        zombieHealth.TakeDamage(10);
    }

    void Update()
    {
        Vector3 direction = targetObject.transform.position - transform.position;
        Vector3 normalizedDirection = direction.normalized;
        distance = Vector3.Distance(transform.position, goal.position);

        float angle = Vector3.Angle(transform.forward, normalizedDirection);
        if (distance < attackRange)
        {
            animator.SetTrigger("Attack");
        }
        if (angle < 90 && distance < targetingRange && moving)
        {
            agent.destination = goal.position;
            animator.SetTrigger("PlayerSeen");
        }
        else if (distance < 10 && moving)
        {
            agent.destination = goal.position;
            animator.SetTrigger("PlayerSeen");
        }
        else
        {
            animator.SetTrigger("PlayerLeft");
            Debug.Log("Idle");
        }
    }
    public void Moving(bool isMoving)
    {
        moving = isMoving;
        Debug.Log("Moving method called. Value of 'moving': " + moving);
    }
}
