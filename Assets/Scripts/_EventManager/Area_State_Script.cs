using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_State_Script : MonoBehaviour
{
    [HideInInspector]
    public _1st_Director _FirstDirector;

    public enum Area_State { InCastle = 0, Field = 1, Forest = 2, FrozenLand = 3, Volcano = 4 }
    public Area_State _Area_State = Area_State.InCastle;
    // Start is called before the first frame update
    #region Algorithms & Functions
    public void CheckIsInSquareArea(int x1, int x2, int y1, int y2, string place)
    {

        float px = _FirstDirector._Player.transform.position.x;
        float py = _FirstDirector._Player.transform.position.z;
        if (px > x1 && px < x2 && py > y1 && py < y2 && !_FirstDirector.announcer_triggered)
        {
            _FirstDirector.announcer_triggered = true; Debug.Log("a");
            _FirstDirector.announcer.Announce("You arrived " + place + ".");
            _FirstDirector.mode._TNT_State++;
            return;
        }

    }
    #endregion
}
