using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChange : MonoBehaviour
{
    public Material Excite;
    public Material Happy;
    public Material Angery;
    public Material Love;
    public Material Default;
    public GameObject Face;
    public Animator animator;



    // Changes face based on emotion
    void Update()
    {
        if (animator.GetBool("Grabbed") == true)
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Default;
        }
        else if (animator.GetBool("Rage") == true)
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Angery;
        }
        else if (animator.GetBool("Love") == true)
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Love;
        }
        else if (animator.GetBool("Excited") == true)
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Excite;
        }
        else if (animator.GetBool("Happy") == true)
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Happy;
        }
        else
        {
            Face.GetComponent<SkinnedMeshRenderer>().material = Default;
        }
    }
}
