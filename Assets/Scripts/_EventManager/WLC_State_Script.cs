using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLC_State_Script : MonoBehaviour
{
    public enum State{Continuable=0,Win=1,Lose=2}
    public State WLC_State=State.Continuable;
}
