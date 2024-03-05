using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    //chase timer
    float m_time = 0;
    float MaxTime = 5;

    public bool released = false;
    public GameObject GreenRat;

    //angry behavior
    public bool defused = true;

    public void Grabbed()
    {
        //makes green cat pay attention
        Animator animator = GreenRat.GetComponent<Animator>();
        animator.SetBool("Excited", true);
        defused = false;
        if (gameObject.tag == "InactiveBattery")
        {
            gameObject.tag = "ActiveBattery";
        }
        else if (gameObject.tag == "InactiveToy")
        {
            gameObject.tag = "ActiveToy";
        }
    }

    public void Released()
    {
        //speeds up cat if chasing toy
        if (gameObject.tag == "ActiveToy")
        {
            AiController AI = GreenRat.GetComponent<AiController>();
            AI.speed = AI.speed * 3;
            AI.agent.speed = AI.speed;
        }
        //disables angry behavior, enables chasing
        defused = true;
        released = true;       
    }

    void Update()
    {
        //cat chases objects
        if (released == true)
        {
            m_time -= Time.deltaTime;
            AiController AI = GreenRat.GetComponent<AiController>();
            AI.RunAfterObject(transform.position);
        }
        //cat stops chasing
        if (m_time <= 0)
        {
            m_time = MaxTime;
            released = false;
            Animator animator = GreenRat.GetComponent<Animator>();
            animator.SetBool("Excited", false);
            AiController AI = GreenRat.GetComponent<AiController>();
            AI.speed = AI.baseSpeed;
            AI.agent.speed = AI.speed;
            if (gameObject.tag == "ActiveToy")
            {
                gameObject.tag = "InactiveToy";
            }
            if (gameObject.tag == "ActiveBattery")
            {
                gameObject.tag = "InactiveBattery";
            }
        }
    }
}
