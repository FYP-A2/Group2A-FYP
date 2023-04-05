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
   public NewSpawnManager newSpawnManager;
    Director_SpawnManager director_SpawnManager = new Director_SpawnManager();
    #endregion




    #region Game Start & By-Mode Updates
   void Start(){ Time.timeScale=0; }

   public void TNT1ModeUpdate(){
      if(mode._TNT_State==Mode.TNT_State.Waiting_GoTreeArea){

         _DA_State.AnimateOnce("Welcome to the Full Game Mode.",announcer);
         _DA_State.DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);

         _Area_State.CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
      };


   }

   //Actually it's FullGameModeUpdate(), temp. changed to TNTModeUpdate for debugging.
   public void TNTModeUpdate()
   {
        //Debug.Log("1");
      //PURPOSE: Big frame of Round Loop.
      if(_WLC_State._WLC_State == WLC_State_Script.WLC_State.Continuable){ 
         //State: RoundBeforeStart
         if (mode._FullMode_State == Mode.FullMode_State.RoundBeforeStart) {
            if (timer.remaining_time <= 0 || stage.playerReady){ 
               PrepareTimerStart(stage.currentStage);

            } 
         }
         else if(mode._FullMode_State == Mode.FullMode_State.RoundStart){              
            SpawnMonsters(4);
            director_SpawnManager.roundStartSpawned= true;
            //Debug.Log("RoundStart");
            //dosth();
         }
         else if(mode._FullMode_State == Mode.FullMode_State.RoundOutOfFinishTime){
            dosth();
         }
      
      }






      // The state below exit the Round Loop, to Endpoints.
      else if(_WLC_State._WLC_State == WLC_State_Script.WLC_State.Win){
         //State: Victory
         ShowGameVictory();
      }
      else if(_WLC_State._WLC_State == WLC_State_Script.WLC_State.Lose){
         //State: Defeat
         ShowGameOver();
      }
   }
  

   #endregion






   #region Event of State: RoundBeforeStart
   public void PrepareTimerStart(int level)
   {
      if(level==0){
         stage.playerReady=false;
         mode._FullMode_State = Mode.FullMode_State.RoundStart; 
         timer.TimerStartToCountDown(30);
         _DA_State.AnimateOnce("Game will start in 30 seconds, press [button] to start now", announcer);
      }
      if(level>1 && level <13){}
      if(level==13){}
   }
    #endregion





    #region  Event of State: RoundStart
    public void SpawnMonsters(int level)
    {
        if (director_SpawnManager.roundStartSpawned == false)
            StartCoroutine(newSpawnManager.NewSpawnPrefabs(level));
    }
    #endregion






    #region Event of State: RoundOutOfFinishTime
    public void DeliverExtraReward(int level)
   {

   }
   #endregion







   #region Event of State: Victory
   public void ShowGameVictory(){}
   #endregion

   #region Event of State: Defeat
   public void ShowGameOver(){}
   #endregion

   //dummy.
   public void dosth(){}
}

public class Director_SpawnManager : MonoBehaviour
{
    public bool roundStartSpawned = false;
}
