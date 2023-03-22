using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
   public GameObject _Player;
   public Mode mode;
   public Timer timer;
   public Announcer announcer;
   public bool announcer_triggered;
   public EventChecklist eventChecklist;
   public bool TNTModeTriggered=false,devModeTriggered=false,started_once=false;
   public enum State{Step0to1=0,Step1Finished=1,Step2Finished=2,Step3Finished=3,Step4Finished=4,
      Step5Finished=5,Step6Finished=6,Step7Finished=7,Step8Finished=8,Step9Finished=9}

   public State eventChecklistState = State.Step0to1;

   public enum DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2}
   public DA_State DrawState=DA_State.NOTHING,AnimationState=DA_State.NOTHING;

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

   #region DA Series (Real Part, Polymorphism)
   public void DrawOnce(string msg,EventChecklist eventChecklist){
      if(DrawState==DA_State.NOTHING || DrawState==DA_State.OUTDATED){
         DrawState=DA_State.UP_TO_DATE;
         eventChecklist.SetText(msg);        
      }
   }
   public void AnimateOnce(string msg, Announcer announcer){
      if(AnimationState==DA_State.NOTHING || AnimationState==DA_State.OUTDATED){
         AnimationState=DA_State.UP_TO_DATE;
         announcer.Announce(msg);
      }
   }
   #endregion


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
         FullModeUpdate();
      }

   }

   public void Enter_TNTMode(){
      TNTModeTriggered=false;
      Time.timeScale=1;
      FullModeUpdate();
      
      started_once=true;
   }
   public void Enter_DevMode(){
      devModeTriggered= false;
      Time.timeScale=1;
      started_once=true;
   }

   public void DevModeContent(){

   }

   public void FullModeUpdate(){
      if(eventChecklistState==State.Step0to1){
         
         AnimateOnce("Welcome to the Full Game Mode.",announcer);
         DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);
         
      CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
         //StartCoroutine(CoroutineCheckIsInSquareArea(eventChecklistState,418,458,1722,1762,"Tree Zone"));
      };

      
   }

   public void CheckIsInSquareArea(int x1,int x2,int y1,int y2,string place){
      
      float px=_Player.transform.position.x;
      float py=_Player.transform.position.z;
      if(px>x1 && px<x2 && py>y1 && py<y2 && !announcer_triggered){
         announcer_triggered=true; Debug.Log("a");
         announcer.Announce("You arrived "+place+".");
         eventChecklistState++;
         return;
      }
      
   }

}
