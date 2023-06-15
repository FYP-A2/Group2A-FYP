using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Mode : MonoBehaviour
{
   public enum GameMode {PAUSE=-1,DEV_MODE=0,TNT_MODE=1,FULL_MODE=2}
   public GameMode gameMode = GameMode.PAUSE;
   public TMP_Text TMP_Text;
   public _1st_Director _FirstDirector;

   public enum TNT_State{
        Tree1 = 0, Tree2 = 1, Tree3 = 2, Tree4 = 3, Tree5 = 4,
        Stone1 = 5, Stone2 = 6, Stone3 = 7, Stone4 = 8, Stone5 = 9,
        Repair1 = 10, Repair2 = 11, Repair3 = 12, Repair4 = 13, Repair5 = 14,
        Build1 = 15, Build2 = 16, Build3 = 17, Build4 = 18, Build5 = 19, Build6 = 20,
        WaitEnd = 21, End = 22
    }
   public bool TNTModeTriggered=false,devModeTriggered=false,started_once=false;

   public bool Mode2_Triggered=false;

   public TNT_State _TNT_State = 0;

   public enum FullMode_State
   {
      RoundBeforeStart = 0, RoundStart=1, RoundOutOfFinishTime = 2
   }

   public FullMode_State _FullMode_State = FullMode_State.RoundBeforeStart;


   public void F11(){ gameMode=GameMode.DEV_MODE; if (TMP_Text != null) TMP_Text.SetText("DEV MODE ON"); }
   public void F12(){ gameMode=GameMode.FULL_MODE; if (TMP_Text != null) TMP_Text.SetText("FULL MODE ON"); Mode2_Triggered=true;}

   void Update(){
      if(gameMode==GameMode.TNT_MODE ){ TNTModeTriggered=true; }
      
      if(gameMode==GameMode.DEV_MODE ){ devModeTriggered=true; }

      //if(gameMode==GameMode.FULL_MODE && !started_once){ FullModeTriggered=true; }

      //if(gameMode==GameMode.FULL_MODE){_FirstDirector.FullModeUpdate();}

      if(TNTModeTriggered){ Enter_TNTMode(); }
      if(devModeTriggered){ Enter_DevMode(); }

      if(Mode2_Triggered){ 
         Mode2_Triggered=false;
         Time.timeScale=1;
         gameMode=GameMode.FULL_MODE;
      }
      if(gameMode==GameMode.FULL_MODE){
         _FirstDirector.FullModeUpdateLoop();
      }
      if(gameMode==GameMode.TNT_MODE){
         _FirstDirector.TNTModeUpdate();
      }

      

   }

   public void Enter_TNTMode(){
      TNTModeTriggered=false;
      Time.timeScale=1;
      _FirstDirector.TNTModeUpdate();

      started_once=true;
   }


   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }


   public void FM_SetState_RoundStart(){
      this._FullMode_State = FullMode_State.RoundStart;
   }
   public void FM_SetState_RoundOutOfFinishTime(){
      this._FullMode_State = FullMode_State.RoundOutOfFinishTime;
   }
   public FullMode_State FM_GetCurrState(){
      return _FullMode_State;
   }
}
