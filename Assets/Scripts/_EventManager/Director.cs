using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
  public Mode mode;
  public Timer timer;
  public Announcer announcer;
  public EventChecklist eventChecklist;
  public bool fullModeTriggered=false,devModeTriggered=false,started_once=false;

   void Start(){
      Time.timeScale=0;
   }
   void Update(){
      if(mode.gameMode==Mode.GameMode.FULL_MODE && !started_once){
         fullModeTriggered=true;
      }
      if(mode.gameMode==Mode.GameMode.DEV_MODE && !started_once){
         devModeTriggered=true;
      }
      
      if(fullModeTriggered){ Enter_FullMode(); }
      if(devModeTriggered){ Enter_DevMode(); }

   }

   public void Enter_FullMode(){
      fullModeTriggered=false;
      Time.timeScale=1;
      announcer.Announce("Welcome to the Full Game Mode.");
      started_once=true;
   }
   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }



}
