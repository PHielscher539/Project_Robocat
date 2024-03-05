using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class AiController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    [Range(0, 100)] public float speed;
    public float baseSpeed;
    [Range(1, 500)] public float walkRadius;
    float m_time = 0;
    float max_time = 3;
    bool lazy = false;
    float lazytime = 7;
    float max_lazytime = 7;

    void Start()
    {
        if (agent != null)
        {
            agent.speed = speed;
            baseSpeed = speed;
        }
    }

    //Cat lies down on tables
    void OnTriggerEnter(Collider thing)
    {
        if (thing.tag == "SleepPlace")
        {
            animator.SetBool("LieDown", true);
        }
        else if (thing.tag != "Player" && lazy == false)
        {
            animator.SetBool("LieDown", false);
        }
    }

    public void RunAfterObject(Vector3 pos)
    {
        if (agent.enabled == true)
        {
            agent.SetDestination(pos);
        }
    }


    void Update()
    {
        //Locks cat movement during animations
        if (animator.GetBool("Grabbed") == true || animator.GetBool("Happy") == true || animator.GetBool("Love") == true || animator.GetBool("Rage") == true || animator.GetBool("LieDown") == true)
        {
            agent.enabled = false;
            animator.SetBool("Walk", false);
            m_time = max_time;
        }

        if (animator.GetBool("Excited") == true)
        {
            GameObject Toy = GameObject.FindWithTag("ActiveToy");
            if (Toy == null)
            {
                Toy = GameObject.FindWithTag("ActiveBattery");
            }
            //Breaks cat out of default loop if toy is picked up
            if (Toy.GetComponent<GrabObject>().released == true)
            {
                agent.enabled = true;
                animator.SetBool("Walk", true);
                lazytime = max_lazytime;
                animator.SetBool("LieDown", false);
                lazy = false;
            }
            else
            {
                agent.enabled = false;
                animator.SetBool("Walk", false);
            }

            m_time = max_time;
        }

        if (m_time <= 0)
        {
            agent.enabled = true;
            if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
            {
                int i = Random.Range(0, 5);
                //Cat walks
                if (i == 1 || i == 2 || i == 3)
                {
                    animator.SetBool("Walk", true);
                    agent.SetDestination(RandomNavMeshLocation());
                }
                //Cat lies down
                else if(i == 4)
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("LieDown", true);
                    lazy = true;
                    m_time = max_time;
                }
                //Cat stands still
                else
                {
                    animator.SetBool("Walk", false);
                    m_time = max_time;
                }
            }
        }
        else
        {
            m_time -= Time.deltaTime;
        }

        //Cat lying down cooldown
        if (lazy == true)
        {
            lazytime -= Time.deltaTime;
            m_time = max_time;
        }
        if (lazytime <= 0)
        {
            lazytime = max_lazytime;
            animator.SetBool("LieDown", false);
            lazy = false;
        }

    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
