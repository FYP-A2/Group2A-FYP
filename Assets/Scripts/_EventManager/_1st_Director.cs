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
   #endregion
}

