using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    private RaycastHit HitInfo;
    public GameObject RatHead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Transform cameraTransform = Camera.main.transform;

        //Makes cat face player
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f) && HitInfo.transform.tag == "Rat")
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
            FaceCamera Rat = RatHead.GetComponent<FaceCamera>();
            Rat.Look();
        }
    }
}
