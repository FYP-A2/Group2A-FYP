using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
   #region Scripts & Objects Binding;
   public GameObject _Player;
   public Mode mode;
   public Timer timer;
   public Announcer announcer;
   public bool announcer_triggered;
   public EventChecklist eventChecklist;
   #endregion

   #region Finite State Machine (inc. instancing)
   public bool TNTModeTriggered=false,devModeTriggered=false,started_once=false;
   public enum DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2} //Draw & Animation
   public enum TNT_State{
      Waiting_WalkAround=1,
      Waiting_GoTreeArea=2,Waiting_CutTree=3,
      Waiting_GoStoneArea=4,Waiting_MineStone=5,
      Waiting_GoBuildArea=6,Waiting_BuildPhyTower=7,
      Waiting_BuildFinished=8,WaitingReady=9,
      Waiting_StartFirstRound=10,Waiting_AllMobsDown=11,
   }
   public enum WinLoseContinue_State{ Continue=1, Won=2, Lost=3, }

   public enum Stage{ Stage_1=1, Stage_2=2 }

   public TNT_State _TNT_State = TNT_State.Waiting_WalkAround;

   public DA_State DrawState=DA_State.NOTHING, AnimationState=DA_State.NOTHING;
   #endregion

   #region  DA stuffs (Empty DrawOnce & Empty Animate Once Examples)
   public void DrawOnce(){}
	public enum DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2}
   public class Director : MonoBehaviour
{
   #region Scripts & Objects Binding;
   public GameObject _Player;
   public Mode mode;
   public Timer timer;
   public Announcer announcer;
   public bool announcer_triggered;
   public EventChecklist eventChecklist;
   #endregion

   #region Finite State Machine (inc. instancing)
   public bool TNTModeTriggered=false,devModeTriggered=false,started_once=false;
   public enum DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2} //Draw & Animation
   public enum TNT_State{
      Waiting_WalkAround=1,
      Waiting_GoTreeArea=2,Waiting_CutTree=3,
      Waiting_GoStoneArea=4,Waiting_MineStone=5,
      Waiting_GoBuildArea=6,Waiting_BuildPhyTower=7,
      Waiting_BuildFinished=8,WaitingReady=9,
      Waiting_StartFirstRound=10,Waiting_AllMobsDown=11,
   }
   public enum WinLoseContinue_State{ Continue=1, Won=2, Lost=3, }

   public enum Stage{ Stage_1=1, Stage_2=2 }

   public TNT_State _TNT_State = TNT_State.Waiting_WalkAround;

   public DA_State DrawState=DA_State.NOTHING, AnimationState=DA_State.NOTHING;
   #endregion

   #region  DA stuffs (Empty DrawOnce & Empty Animate Once Examples)
   public void DrawOnce(){
      //state:nothing,outdated,uptodate
      //if nothing or outdated *settext*
      //change state to uptodate
      if(DrawState==DA_State.NOTHING || DrawState==DA_State.OUTDATED){
         //Draw();
         DrawState=DA_State.UP_TO_DATE;
      }
   }
   public void SetDrawOudated(){
      DrawState=DA_State.OUTDATED;
   }
   public void AnimateOnce(){
      if(AnimationState==DA_State.NOTHING || AnimationState==DA_State.OUTDATED){
         //Draw();
         AnimationState=DA_State.UP_TO_DATE;
      }
   }
   public void SetAnimationOutdated(){
      AnimationState=DA_State.OUTDATED;
   
   }
   #endregion

   #region Unity Start & Update, & Entry Points
   void Start(){
      Time.timeScale=0;
   }
   void Update(){
      if(mode.gameMode==Mode.GameMode.TNT_MODE && !started_once){
         TNTModeTriggered=true;
      }
      if(mode.gameMode==Mode.GameMode.DEV_MODE && !started_once){
         devModeTriggered=true;
      }

      if(TNTModeTriggered){ Enter_TNTMode(); }
      if(devModeTriggered){ Enter_DevMode(); }

      if(mode.gameMode==Mode.GameMode.TNT_MODE){
         TNTModeUpdate();
      }

   }

   public void Enter_TNTMode(){
      TNTModeTriggered=false;
      Time.timeScale=1;
      TNTModeUpdate();

      started_once=true;
   }
   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }
   #endregion

   #region Algorithms & Functions
   public void CheckIsInSquareArea(int x1,int x2,int y1,int y2,string place){

      float px=_Player.transform.position.x;
      float py=_Player.transform.position.z;
      if(px>x1 && px<x2 && py>y1 && py<y2 && !announcer_triggered){
         announcer_triggered=true; Debug.Log("a");
         announcer.Announce("You arrived "+place+".");
         _TNT_State++;
         return;
      }

   }
   #endregion


   #region RealDirectorPart
   public void TNTModeUpdate(){
      if(_TNT_State==TNT_State.Waiting_WalkAround){

         AnimateOnce("Welcome to the Full Game Mode.",announcer);
         DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);

         CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
         //StartCoroutine(CoroutineCheckIsInSquareArea(eventChecklistState,418,458,1722,1762,"Tree Zone"));
      };


   }
   #endregion
}
   
   #endregion

   #region Unity Start & Update, & Entry Points
   void Start(){
      Time.timeScale=0;
   }
   void Update(){
      if(mode.gameMode==Mode.GameMode.TNT_MODE && !started_once){
         TNTModeTriggered=true;
      }
      if(mode.gameMode==Mode.GameMode.DEV_MODE && !started_once){
         devModeTriggered=true;
      }

      if(TNTModeTriggered){ Enter_TNTMode(); }
      if(devModeTriggered){ Enter_DevMode(); }

      if(mode.gameMode==Mode.GameMode.FULL_MODE){
         TNTModeUpdate();
      }

   }

   public void Enter_TNTMode(){
      TNTModeTriggered=false;
      Time.timeScale=1;
      TNTModeUpdate();

      started_once=true;
   }
   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }
   #endregion

   #region Algorithms & Functions
   public void CheckIsInSquareArea(int x1,int x2,int y1,int y2,string place){

      float px=_Player.transform.position.x;
      float py=_Player.transform.position.z;
      if(px>x1 && px<x2 && py>y1 && py<y2 && !announcer_triggered){
         announcer_triggered=true; Debug.Log("a");
         announcer.Announce("You arrived "+place+".");
         _TNT_State++;
         return;
      }

   }
   #endregion


   #region RealDirectorPart
   public void TNTModeUpdate(){
      if(_TNT_State==TNT_State.Waiting_WalkAround){

         AnimateOnce("Welcome to the Full Game Mode.",announcer);
         DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);

         CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
         //StartCoroutine(CoroutineCheckIsInSquareArea(eventChecklistState,418,458,1722,1762,"Tree Zone"));
      };


   }
   #endregion
}