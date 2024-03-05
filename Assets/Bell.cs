using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    float m_time = 5;
    float max_time = 5;
    public GameObject Audio;

    public void Ring()
    {
        GameObject Rat = GameObject.FindWithTag("Rat");
        Animator animator = Rat.GetComponent<Animator>();
        animator.SetBool("IsSound", true);
        m_time = max_time;
        Audio.GetComponent<AudioManager>().Play("Bell");
    }
    void Update()
    {
        if (m_time > 0)
        {
            m_time -= Time.deltaTime;
        }
        else
        {
            GameObject Rat = GameObject.FindWithTag("Rat");
            Animator animator = Rat.GetComponent<Animator>();
            animator.SetBool("IsSound", false);
        }

    }
}
