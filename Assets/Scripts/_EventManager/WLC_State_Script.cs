using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLC_State_Script : MonoBehaviour
{
    public enum WLC_State{Continuable=0,Win=1,Lose=2}
    public WLC_State _WLC_State=WLC_State.Continuable;
}
