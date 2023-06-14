using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo1 : MonoBehaviour
{
    public GameObject targetObject;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public AudioSource audioSource5;
    Transform goal;
    public float targetingRange = 10;
    public Animator anim;
    public float attackRange = 5;
    public int attackDamage = 20;
    public float distance;
    UnityEngine.AI.NavMeshAgent agent;
    ZombieHealth1 zombieHealth;
    Rigidbody rb;
    Vector3 targetPosition;
    bool dead;
    public bool respawn;
    public bool respawnAgain;
    public int enemyHealth = 100;

    public enum ZombStates
    {
        Idle,
        Chase,
        Hit,
        Attack,
        Death
    }

    public ZombStates currentState;

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        goal = targetObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        zombieHealth = GetComponent<ZombieHealth1>();
        currentState = ZombStates.Idle;
        rb = GetComponent<Rigidbody>();
        dead = false;
    }

    void Update()
    {
        Vector3 direction = goal.position - transform.position;
        distance = Vector3.Distance(transform.position, goal.position);


        switch (currentState)
        {
            case ZombStates.Idle:
                anim.SetInteger("animState", 0);
                audioSource5.Stop();
                agent.destination = transform.position;
                UpdateStates();
                break;
            case ZombStates.Chase:
                anim.SetInteger("animState", 1);
                audioSource5.Stop();
                UpdateStates();
                break;
            case ZombStates.Hit:
                Invoke("UpdateStates", 1f);
                anim.SetInteger("animState", 2);
                audioSource2.Stop();
                agent.destination = transform.position;
                break;
            case ZombStates.Attack:
                anim.SetInteger("animState", 3);
                FaceTarget(goal.position);
                audioSource5.Stop();
                audioSource2.Stop();
                UpdateStates();
                break;
            case ZombStates.Death:
                anim.Play("Death");
                audioSource2.Stop();
                audioSource3.Stop();
                audioSource4.Stop();
                GetComponent<Collider>().enabled = false;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                agent.destination = transform.position;
                if (dead == false)
                {
                    Invoke("DeactivateObject", 4);
                    dead = true;
                }
                break;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = goal.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, targetingRange))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void UpdateStates()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        if (currentState != ZombStates.Idle && currentState != ZombStates.Hit)
        {
            Vector3 playerToEnemy = transform.position - goal.position;
            Vector3 targetPosition = goal.position + playerToEnemy.normalized * (attackRange - 1);
            agent.destination = targetPosition;
        }

        if (distance <= attackRange)
        {
            ChangeStates(ZombStates.Attack);
        }
        else if (distance > attackRange && distance < targetingRange && !dead && CanSeePlayer())
        {
            ChangeStates(ZombStates.Chase);
        }
        else if (distance > targetingRange)
        {
            ChangeStates(ZombStates.Idle);
            StopSound();
        }
    }

    public void ChangeStates(ZombStates newState)
    {
        currentState = newState;
    }

    void DeactivateObject()
    {
        dead = true;
        gameObject.SetActive(false);
        StopSound();
        //if (!respawn)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    Invoke("Respawn", 6f);
        //    agent.destination = transform.position;
        //}
    }

    public void DealDamage()
    {
        targetObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp
            (transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    public void StopSound()
    {
        audioSource2.Stop();
        audioSource3.Stop();
        audioSource4.Stop();
        audioSource5.Stop();
    }

    public void WalkSound()
    {
       // audioSource2.Play();
    }
    public void HitSound()
    {
       // audioSource3.Play();
    }
    public void AttackSound()
    {
       // audioSource4.Play();
    }
    public void DeathSound()
    {
       // audioSource5.Play();
    }
    //void Respawn()
    //{
    //    gameObject.GetComponent<ZombieHealth>().TakeHealth(enemyHealth);
    //    gameObject.GetComponent<ZombieHealth>().ChangeBool();
    //    rb.isKinematic = true;
    //    rb.constraints = RigidbodyConstraints.FreezeAll;
    //    anim.SetTrigger("Respawn");
    //    anim.SetInteger("animState", 1);
    //    agent.destination = transform.position;
    //    Invoke("UpdateStates", 0.8f);
    //    dead = false;
    //    if (!respawnAgain)
    //    {
    //        respawn = false;
    //    }
    //}
}
