using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform m_Camera;
    public GameObject m_Rat;
    bool m_look = false;
    float m_time = 0;
    float MaxTime = 2;

    //Calls on players raycast hit
    public void Look()
    {
        m_look = true;
        m_time = MaxTime;
    }

    void Update()
    {
        //Stops cat from looking at player
        if (m_time <= 0)
        {
            m_look = false;
        }
    }

    void FixedUpdate()
    {
        m_time -= Time.deltaTime;

        Animator animator = m_Rat.GetComponent<Animator>();       
        if (animator.GetBool("Grabbed") == false)
        {
            //Makes cat face toy/food
            if (animator.GetBool("Excited") == true)
            {
                GameObject Toy = GameObject.FindWithTag("ActiveToy");
                if (Toy == null)
                {
                    Toy = GameObject.FindWithTag("ActiveBattery");
                }
                Vector3 direction = (Toy.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 240);
            }
            //Makes cat face player
            else if (m_look == true)
            {
                Vector3 direction = (m_Camera.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 80);
            }
            //Restore default head rotation
            else
            {
                Vector3 direction = m_Rat.transform.forward;
                Quaternion BaseLookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, BaseLookRotation, Time.deltaTime * 80);
            }
        }
        //Grabbed head rotation
        else
        {
            Vector3 direction = m_Rat.transform.forward;
            Quaternion BaseLookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, BaseLookRotation, Time.deltaTime * 240);
        }

    }

}
