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
using UnityEngine.SceneManagement;

public class _1st_Director : MonoBehaviour
{
    //SAFE?
    #region Scripts & Objects Binding;
    public GameObject _Player;
    public Breakable _Core;
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
    bool InARoundState = false;
    bool boo_MoveOnToStart = false;
    bool boo_dosth_NeedToStart = false;
    bool boo_ShowGameVictory_NeedToStart = false;
    bool boo_DeliverExtraRewards_NeetToStart = false;
    bool boo_ShowGameOver_NeedToStart = false;
    bool boo_SpawnMonsters_NeedToStart = false;

    #endregion

    #region Need of Assigning
    bool NeedOf_Asn_Timer_n_Announcer = false; //STATE: 1
    #endregion

    private void Start()
    {
        _Player.GetComponent<Player>().director = this;
    }

    #region OncePrototype
    public void _Once(ref bool need)
    {
        if (need)
        {
            need = false;
            dosth_Once(ref boo_dosth_NeedToStart);
        }
    }

    //SeveralTimes prototype is not needed if you have a counter logic already.
    public void _SeveralTimes(ref bool need, bool condition)
    {
        if (need && condition) { dosth_Once(ref boo_dosth_NeedToStart); }
        else { need = false; }
    }

    // About booleans' naming: prob. *** boo_XXXX_Started;
    #endregion


    #region Game Start & By-Mode Updates
    //void Start(){ Time.timeScale=0; }

    bool tntOncePlayed = false;
    public void TNTModeUpdate()
    {
        if (mode._TNT_State == Mode.TNT_State.Waiting_GoTreeArea)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("Welcome to the Full Game Mode.", announcer);
                _DA.DrawOnce("1. Follow the Arrow & Go to the forest", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("TNT.TreeArea1").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("TNT.TreeArea1")[0].transform;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("TNT.TreeArea1", _Player.GetComponent<Player>());
        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_CutTree)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("2. Press A to select the Axe\nHew the tree", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
            }

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_GoStoneArea)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("You get 10 wood", announcer);
                _DA.DrawOnce("3. Follow the Arrow & Find a stone", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("TNT.StoneArea1").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("TNT.StoneArea1")[0].transform;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("TNT.StoneArea1", _Player.GetComponent<Player>());

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_MineStone)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("4.Press A to select the pickaxe\nMine the stone", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
            }

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_GoBuildArea)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("You get 5 Stone", announcer);
                _DA.DrawOnce("5.Follow the Arrow", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("TNT.BuildArea1").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("TNT.BuildArea1")[0].transform;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("TNT.BuildArea1", _Player.GetComponent<Player>());

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_BuildPhyTower)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("6. Grab book and turn to page 2\nselect the arrow Tower base", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
            }

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_BuildFinished)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("7. ...building...", eventChecklist);
            }

        }


        else if (mode._TNT_State == Mode.TNT_State.Waiting_Ready)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("Build Done!!", announcer);
                _DA.DrawOnce("8. You are now free to collect resources, Monster Will attack the core soon", eventChecklist);
                timer.Init(10);
                timer.SetStateRunning();
            }

            if (timer.GetCurrState() == Timer.State.FINISHED)
            {
                timer.Reset();

                bool temp = false;
                //spawn monster
                //SpawnMonsters_Once(stageObject, ref temp);
                //TNTModeJumpState();

                //skip TNT
                mode.gameMode = Mode.GameMode.FULL_MODE;
                eventChecklist.ClearText();
                _DA.Reset();
            }

        }

        else if (mode._TNT_State == Mode.TNT_State.Waiting_EndCondition)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("monster is arriving, defence the core", eventChecklist);
            }

            if (GameObject.FindGameObjectsWithTag("Monster").Length < 3)
            {
                TNTModeJumpState();
            }

        }

        else if (mode._TNT_State == Mode.TNT_State.End)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce("End", eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
            }

            GameObject[] monsterList = GameObject.FindGameObjectsWithTag("Monster");
            if (monsterList.Length > 0)
                _Player.GetComponent<Player>().lookAt.target = monsterList[0].transform;
        }
    }

    void TNTModeCheckPlayerInAreaAndJumpState(string area_ID, Player p)
    {
        if (AreaTrigger.CheckPlayerInAreaByID(area_ID, p))
        {
            announcer.Announce("You arrived " + AreaTrigger.FindAreasByID(area_ID)[0].area_Name);
            TNTModeJumpState();
        }
    }

    public void TNTModeJumpState()
    {
        mode._TNT_State++;
        tntOncePlayed = false;
        _DA.Reset();
    }

    //Actually it's FullGameModeUpdate(), temp. changed to TNTModeUpdate for debugging.
    public void FullModeUpdateLoop()
    {

        //Debug.Log("1");
        //PURPOSE: Big frame of Round Loop.
        if (_WLC_State.WLC_State == WLC_State_Script.State.Continuable)
        {

            //State: RoundBeforeStart
            if (mode.FM_GetCurrState() == Mode.FullMode_State.RoundBeforeStart)
            {

                BeginRoundState_for_Timer_n_Announcer();

                //_M.mjc_livedata._rmn_time=timer.remaining_time.ToString();
                //_M.mjc_livedata._player_ready=stageObject.playerReady.ToString();


                // !!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!! Timer must need boolean!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!
                if (timer.GetCurrState() == Timer.State.PAUSE)
                {
                    newSpawnManager.PathDisplay(stageObject.currentStage - 1);
                    Assign_Timer_n_Announcer_ver_RBS(30, ref NeedOf_Asn_Timer_n_Announcer);
                }
                if (timer.GetCurrState() == Timer.State.FINISHED)
                {
                    timer.Reset();
                    ExitRoundState();
                    _DA.Reset();

                    mode.FM_SetState_RoundStart();
                    director_SpawnManager.roundStartSpawned = false;
                }





                //mode._FullMode_State = Mode.FullMode_State.RoundStart;
            }
            else if (mode.FM_GetCurrState() == Mode.FullMode_State.RoundStart)
            {

                BeginRoundState_for_Timer_n_Announcer();
                SpawnMonsters_Once(stageObject, ref director_SpawnManager.roundStartSpawned);

                if (timer.GetCurrState() == Timer.State.PAUSE)
                {
                    Assign_Timer_n_Announcer_ver_RS(30, ref NeedOf_Asn_Timer_n_Announcer);
                }
                if (timer.GetCurrState() == Timer.State.FINISHED)
                {
                    mode.FM_SetState_RoundOutOfFinishTime();
                    ExitRoundState();
                    _DA.Reset();
                }

            }
            else if (mode.FM_GetCurrState() == Mode.FullMode_State.RoundOutOfFinishTime)
            {
                //if stage < 11, wait, add stage number and set round start
                if (stageObject.currentStage <= 11)
                {
                    BeginRoundState_for_Timer_n_Announcer();

                    if (timer.GetCurrState() == Timer.State.PAUSE)
                    {
                        newSpawnManager.PathDisplay(stageObject.currentStage);
                        Assign_Timer_n_Announcer_ver_WaitingForNextStage(10, ref NeedOf_Asn_Timer_n_Announcer);
                    }
                    if (timer.GetCurrState() == Timer.State.FINISHED)
                    {
                        stageObject.currentStage++;                        
                        mode.FM_SetState_RoundStart();
                        ExitRoundState();
                        _DA.Reset();
                        director_SpawnManager.roundStartSpawned = false;
                    }
                }
                
                
            }

            //if core destory, set _WLC_State.WLC_State = WLC_State_Script.State.Lose
            if (_Core.GetHP() <= 0)
            {
                _WLC_State.WLC_State = WLC_State_Script.State.Lose;
            }

            //if stage == 12, check monster amount < 3 , set  _WLC_State.WLC_State = WLC_State_Script.State.Win
            if (stageObject.currentStage==12)
            {
                Monster[] ms = FindObjectsOfType<Monster>();
                if (ms.Length < 4) {
                    _WLC_State.WLC_State = WLC_State_Script.State.Win;
                }
            }
        }






        // The state below exit the Round Loop, to Endpoints.
        else if (_WLC_State.WLC_State == WLC_State_Script.State.Win)
        {
            //State: Victory
            ShowGameVictory_Once(ref boo_ShowGameVictory_NeedToStart);
        }
        else if (_WLC_State.WLC_State == WLC_State_Script.State.Lose)
        {
            //State: Defeat
            ShowGameOver_Once(ref boo_ShowGameOver_NeedToStart);
        }
    }

    #endregion




    #region // State: RoundBeforeStart
    public void Assign_Timer_n_Announcer_ver_RBS(float second, ref bool need)
    {
        if (need)
        {
            need = false;

            if (stageObject.currentStage >= 1 && stageObject.currentStage < 12)
            {
                timer.Init(second);
                _DA.AnimateOnce("Game will start in "+ second + " seconds, press [button] to start now", announcer);
                timer.SetStateRunning();
            }
        }
    }
    #endregion

    public void Assign_Timer_n_Announcer_ver_RS(float second, ref bool need)
    {
        if (need)
        {
            need = false;

            if (stageObject.currentStage >= 1 && stageObject.currentStage < 12)
            {
                timer.Init(second);
                _DA.AnimateOnce("Wave " + stageObject.currentStage, announcer);
                timer.SetStateRunning();
            }
        }
    }

    public void Assign_Timer_n_Announcer_ver_WaitingForNextStage(float second, ref bool need)
    {
        if (need)
        {
            need = false;

            if (stageObject.currentStage >= 1 && stageObject.currentStage < 12)
            {
                timer.Init(second);
                _DA.AnimateOnce("Ready for Wave " + (stageObject.currentStage + 1), announcer);
                timer.SetStateRunning();
            }
        }
    }



    public void ExitRoundState()
    {
        InARoundState = false;
    }
    public void BeginRoundState_for_Timer_n_Announcer()
    {
        if (!InARoundState) {
            InARoundState = true;
            NeedOf_Asn_Timer_n_Announcer = true;
            timer.Reset();
        }
    }










    #region // State: RoundStart
    public void SpawnMonsters_Once(Stage stage, ref bool onceBoolean)
    {


        if (director_SpawnManager.roundStartSpawned == false)
        {           
            foreach (MonsterDictionary e in newSpawnManager.enemySpawnData)
                StartCoroutine(newSpawnManager.NewSpawnPrefabs(e, stage));
        }
        onceBoolean = true;

    }
    public void MoveOnToStart_Once(ref bool onceBoolean)
    {
        onceBoolean = false;
        _DA.AnimateOnce("Now Start", announcer);
        //_DA.SetAnimationOutdated();

    }
    #endregion






    #region // State: RoundOutOfFinishTime
    public void DeliverExtraRewards_Once(int stage, ref bool onceBoolean)
    {
        onceBoolean = false;
        dosth_Once(ref boo_dosth_NeedToStart);
    }
    #endregion







    #region // State: Victory
    public void ShowGameVictory_Once(ref bool onceBoolean)
    {
        PlayerPrefs.SetString("msg", "win");
        SceneManager.LoadScene("EndScene");
    }
    #endregion

    #region // State: Defeat
    public void ShowGameOver_Once(ref bool onceBoolean)
    {
        PlayerPrefs.SetString("msg", "lose");
        SceneManager.LoadScene("EndScene");
    }
    #endregion

    //dummy.
    public void dosth_Once(ref bool onceBoolean) { onceBoolean = false; }
}
