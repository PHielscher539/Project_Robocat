using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Interaction : MonoBehaviour
{
    public Animator animator;
    public GameObject Audio;

    //rage & love
    float m_time = 0;
    float MaxTime = 8;

    //happy
    float stime = 3;
    float sMaxTime = 3;
    bool streicheln = false;

    float Endtime = 0;
    float EndMaxTime = 5;

    public bool truerat;
    public bool redrat;

    void OnCollisionEnter(Collision hitInfo)
    {
        //Makes Cat angry
        if (hitInfo.collider.tag == "ActiveToy")
        {
            if (hitInfo.collider.GetComponent<GrabObject>().defused == false)
            {
                animator.SetBool("Rage", true);
                m_time = MaxTime;
            }
        }
        //Makes cat eat batteries
        if (hitInfo.collider.tag == "ActiveBattery")
        {
            if (truerat == true)
            {
                animator.SetBool("Love", true);
                animator.SetBool("Backflip", true);
                animator.SetBool("Excited", false);
                m_time = MaxTime;
                Destroy(hitInfo.collider.gameObject);
            }
        }

    }

    public void Streicheln()
    {
        streicheln = true;
    }

    public void StreichelEnd()
    {
        stime = sMaxTime;
        Endtime = EndMaxTime;
        streicheln = false;
    }

    public void SetGrab()
    {
        animator.SetBool("Grabbed", true);
        animator.SetBool("Happy", false);
        animator.SetBool("Rage", false);
        animator.SetBool("Love", false);
        Audio.GetComponent<AudioManager>().Play("Meow");

    }

    public void ExitGrab()
    {
        animator.SetBool("Grabbed", false);
    }

    public void BackflipEnd()
    {
        animator.SetBool("Backflip", false);
    }

    void Update()
    {
        if (redrat == true)
        {
            animator.SetBool("LieDown", true);
        }
        //HAPPY
        if (streicheln == true && animator.GetBool("Grabbed") == false)
        {
            if (stime >= 0)
            {
                stime -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("Happy", true);
            }
        }
        else
        {
            if (Endtime >= 0)
            {
                Endtime -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("Happy", false);
            }
        }
        //RAGE
        if (m_time >= 0)
        {
            m_time -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Rage", false);
            animator.SetBool("Love", false);
        }
    }

}
