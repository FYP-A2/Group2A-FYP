using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_State : MonoBehaviour
{
   public _1st_Director _FirstDirector;

   public enum _Area_State{ InCastle=0, Field=1, Forest=2, FrozenLand=3, Volcano=4}
   // Start is called before the first frame update
   #region Algorithms & Functions
   public void CheckIsInSquareArea(int x1,int x2,int y1,int y2,string place){

      float px=_1st_Director._Player.transform.position.x;
      float py=_1st_Director.Player.transform.position.z;
      if(px>x1 && px<x2 && py>y1 && py<y2 && !_1st_Director.announcer_triggered){
         _1st_Director.announcer_triggered=true; Debug.Log("a");
         _1st_Director.announcer.Announce("You arrived "+place+".");
         _1st_Director.mode._TNT_State++;
         return;
      }

   }
   #endregion
}
