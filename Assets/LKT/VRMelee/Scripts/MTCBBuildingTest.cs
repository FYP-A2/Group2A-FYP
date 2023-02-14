using FYP2A.VR.Melee.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTCBBuildingTest : MonoBehaviour
{

    public MeleeTargetCommonBuild mtcb;


    // Start is called before the first frame update
    void Start()
    {
        mtcb.AddBuildProgressEvent += Mtcb_addBuildProgressEvent;
    }

    private void Mtcb_addBuildProgressEvent(float buildProgress)
    {
        Debug.Log("mtcb: " + buildProgress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
