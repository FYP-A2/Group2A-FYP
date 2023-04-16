// Seems OK : Declaration of Objects
// Seems OK : Declaration of Booleans
// Seems OK : Once/SeveralTimes Prototype Examples
// Seems OK : Start()
// ? : TNTModeUpdate() - Ignore
// ! : FullModeUpdateLoop() - Testing
// StateTransition: WLC(C)+FMS(B4) = Timer On


using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class _1st_Director : MonoBehaviour{
   //SAFE?
   #region Scripts & Objects Binding;
   [HideInInspector] public GameObject _Player;
   [HideInInspector] public Mode mode;
   public Timer timer;
   [HideInInspector] public Announcer announcer;
   [HideInInspector] public bool announcer_triggered;
   [HideInInspector] public EventChecklist eventChecklist;
    public DA_State_Class _DA;
   [HideInInspector] public Area_State_Script _Area_State;
   [HideInInspector] public WLC_State_Script _WLC_State;
   [HideInInspector] public Stage stageObject;
   public MZRealTimeTracer _M; //MZDBG

   public NewSpawnManager newSpawnManager;
   Director_SpawnManager director_SpawnManager = new Director_SpawnManager();
   #endregion

   #region Event-once Booleans
   bool InARoundState=false;
   bool boo_MoveOnToStart=false;
   bool boo_dosth_NeedToStart=false;
   bool boo_ShowGameVictory_NeedToStart=false;
   bool boo_DeliverExtraRewards_NeetToStart=false;
   bool boo_ShowGameOver_NeedToStart=false;
   bool boo_SpawnMonsters_NeedToStart=false;

   #endregion

   #region Need of Assigning
   bool NeedOf_Asn_Timer_n_Announcer=false; //STATE: 1
   #endregion

   #region OncePrototype
   public void _Once(ref bool need){
      if(need){
         need=false;
         dosth_Once(ref boo_dosth_NeedToStart);
      }
   }

   //SeveralTimes prototype is not needed if you have a counter logic already.
   public void _SeveralTimes(ref bool need,bool condition){
      if(need && condition){dosth_Once(ref boo_dosth_NeedToStart);}
      else{need=false;}
   }

   // About booleans' naming: prob. *** boo_XXXX_Started;
   #endregion


   #region Game Start & By-Mode Updates
   //void Start(){ Time.timeScale=0; }

   public void TNTModeUpdate(){
      if(mode._TNT_State==Mode.TNT_State.Waiting_GoTreeArea){

         _DA.AnimateOnce("Welcome to the Full Game Mode.",announcer);
         _DA.DrawOnce("1. Follow the Arrow & Go Outside of the Castle",eventChecklist);

         _Area_State.CheckIsInSquareArea(418,458,1722,1762,"Tree Zone");
      };


   }

   //Actually it's FullGameModeUpdate(), temp. changed to TNTModeUpdate for debugging.
   public void FullModeUpdateLoop()
   {
                                                   
        //Debug.Log("1");
      //PURPOSE: Big frame of Round Loop.
      if(_WLC_State.WLC_State == WLC_State_Script.State.Continuable){ 

         //State: RoundBeforeStart
         if (mode.FM_GetCurrState() == Mode.FullMode_State.RoundBeforeStart) {

            BeginRoundState_for_Timer_n_Announcer();

            _M.mjc_livedata._rmn_time=timer.remaining_time.ToString();
            _M.mjc_livedata._player_ready=stageObject.playerReady.ToString();


            // !!!!!!!!!!!!!!!!!!!!
            // !!!!!!!!!!!!!!!!!!!!! Timer must need boolean!!!!!!!!!!
            // !!!!!!!!!!!!!!!!!!!!
            if(timer.GetCurrState() == Timer.State.PAUSE){
               Assign_Timer_n_Announcer_ver_RBS(10,ref NeedOf_Asn_Timer_n_Announcer);
            }
            if(timer.GetCurrState() == Timer.State.FINISHED){
               timer.Reset();
               ExitRoundState();
               _DA.Reset();
               mode.FM_SetState_RoundStart();

               

            }


            


            //mode._FullMode_State = Mode.FullMode_State.RoundStart;
         }
         else if(mode.FM_GetCurrState()  == Mode.FullMode_State.RoundStart){

            BeginRoundState_for_Timer_n_Announcer();

            if(timer.GetCurrState() == Timer.State.PAUSE){
               Assign_Timer_n_Announcer_ver_RBS(20,ref NeedOf_Asn_Timer_n_Announcer);
            }
            if(timer.GetCurrState() == Timer.State.FINISHED){
               mode.FM_SetState_RoundStart();
               

            }        

         }
         else if(mode.FM_GetCurrState()  == Mode.FullMode_State.RoundOutOfFinishTime){

         }
      
      }






      // The state below exit the Round Loop, to Endpoints.
      else if(_WLC_State.WLC_State == WLC_State_Script.State.Win){
         //State: Victory
         ShowGameVictory_Once(ref boo_ShowGameVictory_NeedToStart);
      }
      else if(_WLC_State.WLC_State == WLC_State_Script.State.Lose){
         //State: Defeat
         ShowGameOver_Once(ref boo_ShowGameOver_NeedToStart);
      }
   }

   #endregion




   #region // State: RoundBeforeStart
   public void Assign_Timer_n_Announcer_ver_RBS(float second, ref bool need)
   {
      if(need){
         need=false;

         if(stageObject.currentStage>=1 && stageObject.currentStage <12){
            timer.Init(second);
            _DA.AnimateOnce("Game will start in 30 seconds, press [button] to start now", announcer);
            timer.SetStateRunning();
         }
         if(stageObject.currentStage==12){}
      }
   }
   #endregion





   public void ExitRoundState(){
      InARoundState=false;
   }
   public void BeginRoundState_for_Timer_n_Announcer(){
      if(!InARoundState) {InARoundState=true; NeedOf_Asn_Timer_n_Announcer=true;}
   }










    #region // State: RoundStart
    public void SpawnMonsters_Once(Stage stage, ref bool onceBoolean){

      onceBoolean=false;
      if (director_SpawnManager.roundStartSpawned == false)
         foreach(MonsterDictionary e in newSpawnManager.enemySpawnData)
               StartCoroutine(newSpawnManager.NewSpawnPrefabs(e,stage));

    }
    public void MoveOnToStart_Once(ref bool onceBoolean){
      onceBoolean=false;
      _DA.AnimateOnce("Now Start",announcer);
      //_DA.SetAnimationOutdated();
      
    }
    #endregion






    #region // State: RoundOutOfFinishTime
    public void DeliverExtraRewards_Once(int stage, ref bool onceBoolean)
   {
      onceBoolean=false;
      dosth_Once(ref boo_dosth_NeedToStart);
   }
   #endregion







   #region // State: Victory
   public void ShowGameVictory_Once(ref bool onceBoolean){
      onceBoolean=false;
      dosth_Once(ref boo_dosth_NeedToStart);
   }
   #endregion

   #region // State: Defeat
   public void ShowGameOver_Once(ref bool onceBoolean){
      onceBoolean=false;
      dosth_Once(ref boo_dosth_NeedToStart);
   }
   #endregion

   //dummy.
   public void dosth_Once(ref bool onceBoolean){onceBoolean=false;}
}
