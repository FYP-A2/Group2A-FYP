using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mode : MonoBehaviour
{
   public enum GameMode {PAUSE=-1,DEV_MODE=0,TNT_MODE=1,FULL_MODE=2}
   public GameMode gameMode = GameMode.PAUSE;
   public TMP_Text TMP_Text;
   public _1st_Director _FirstDirector;

   public enum TNT_State{
      Waiting_GoTreeArea=1,Waiting_CutTree=2,
      Waiting_GoStoneArea=3,Waiting_MineStone=4,
      Waiting_GoBuildArea=5,Waiting_BuildPhyTower=6,
      Waiting_BuildFinished=7
   }
   public bool TNTModeTriggered=false,devModeTriggered=false,started_once=false;

   public TNT_State _TNT_State = TNT_State.Waiting_GoTreeArea;

   public void F11(){ gameMode=GameMode.DEV_MODE; TMP_Text.SetText("DEV MODE ON"); }
   public void F12(){ gameMode=GameMode.TNT_MODE; TMP_Text.SetText("TNT MODE ON"); }

   void Update(){
      if(gameMode==GameMode.TNT_MODE && !started_once){ TNTModeTriggered=true; }
      
      if(gameMode==GameMode.DEV_MODE && !started_once){ devModeTriggered=true; }

      if(TNTModeTriggered){ Enter_TNTMode(); }
      if(devModeTriggered){ Enter_DevMode(); }

      if(gameMode==GameMode.TNT_MODE){
         //TNTModeUpdate();
      }

      

   }

   public void Enter_TNTMode(){
      TNTModeTriggered=false;
      Time.timeScale=1;
      //TNTModeUpdate();

      started_once=true;
   }
   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }
   
}
