using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class _1st_Director : MonoBehaviour{
   //SAFE?
   #region Scripts & Objects Binding;
   [HideInInspector] public GameObject _Player;
   [HideInInspector] public Mode mode;
   [HideInInspector] public Timer timer;
   [HideInInspector] public Announcer announcer;
   [HideInInspector] public bool announcer_triggered;
   [HideInInspector] public EventChecklist eventChecklist;
   [HideInInspector] public DA_State _DA_State;
   [HideInInspector] public Area_State_Script _Area_State;
   [HideInInspector] public WLC_State_Script _WLC_State;
   [HideInInspector] public Stage stage;
   #endregion



   
   #region Game Start & By-Mode Updates
   void Start(){ Time.timeScale=0; }

   public void TNTModeUpdate(){
      if(mode._TNT_State==Mode.TNT_State.Waiting_GoTreeArea){

         _DA_State.AnimateOnce("Welcome to the Full Game Mode.",announcer);
         _DA_State.DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);

         _Area_State.CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
      };


   }

   public void FullModeUpdate()
   {
      if (mode._FullMode_State == Mode.FullMode_State.RoundBeforeStart)
      {
         PrepareTimerStart();

         if (timer.remaining_time <= 0)
            mode._FullMode_State = Mode.FullMode_State.RoundStart;
      }
      else if(mode._FullMode_State == Mode.FullMode_State.RoundStart)
      {


         //if ()
         // mode._FullMode_State = Mode.FullMode_State.RoundOutOfFinishTime;
      }
      else if (mode._FullMode_State == Mode.FullMode_State.RoundOutOfFinishTime)
      {
         DeliverExtraReward();

         //if ()
         // mode._FullMode_State = Mode.FullMode_State.RoundBeforeStart;
      }
   }
   #endregion

   void PrepareTimerStart()
   {
      timer.TimerStartToCountDown(30);
      _DA_State.AnimateOnce("Game will start in 30 seconds, press [button] to start now", announcer);
   }

   //when player input "Ready", call this
   void StageReady()
   {
      if (mode._FullMode_State == Mode.FullMode_State.RoundBeforeStart)
      {
         mode._FullMode_State = Mode.FullMode_State.RoundStart;
      }
   }

   void DeliverExtraReward()
   {

   }
}

