using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public AreaTrigger at1;
    public Player p1;

    public void Test()
    {
        GetComponent<FlyMode>().EnterFlyMode();
    }
    public void Test02()
    {
        GetComponent<FlyMode>().ExitFlyMode();
    }

    private void Update()
    {
        if (AreaTrigger.CheckPlayerInArea(AreaTrigger.FindAreas(x=>x.area_ID=="Hi"), p1))
            Debug.Log("InArea");
    }
}
