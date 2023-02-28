using FYP2A.VR.Melee.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTCBBuildingTest : MonoBehaviour
{

    public MeleeTargetCommonBuild mtcb;
    public float max = 50;
    public float now = 10;


    // Start is called before the first frame update
    void Start()
    {
        mtcb.AddBuildProgressEvent += Mtcb_addBuildProgressEvent;
        mtcb.SetProgressionDisplay(now,max);
    }

    private void Mtcb_addBuildProgressEvent(float buildProgress)
    {
        Debug.Log("mtcb: " + buildProgress);
        now += buildProgress / 3;
        mtcb.SetProgressionDisplay(now, max);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
