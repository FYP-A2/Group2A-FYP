using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
  public Mode mode;
  public Timer timer;
  public Announcer announcer;
  public bool triggered=false,started_once=false;

   void Update(){
      if(!started_once){
         if(mode.gameMode==Mode.GameMode.FULL_MODE){triggered=true;}
      }
      
      if(triggered){
         triggered=false;
         announcer.Announce("Welcome");
         started_once=true;

      }
      

   }



}
