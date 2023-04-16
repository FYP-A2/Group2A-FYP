using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public void Test()
    {
        GetComponent<FlyMode>().EnterFlyMode();
    }
    public void Test02()
    {
        GetComponent<FlyMode>().ExitFlyMode();
    }
}
