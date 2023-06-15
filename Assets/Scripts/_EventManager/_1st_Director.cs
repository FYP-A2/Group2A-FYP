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
using UnityEngine.XR.Interaction.Toolkit;

public class _1st_Director : MonoBehaviour
{
    //SAFE?
    #region Scripts & Objects Binding;
    public GameObject _Player;
    public Breakable _Core;
    public Mode mode;
    public Timer timer;
    public Announcer announcer;
    public bool announcer_triggered;
    public EventChecklist eventChecklist;
    public DA_State_Class _DA;
    public Area_State_Script _Area_State;
    public WLC_State_Script _WLC_State;
    public Stage stageObject;
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
    TutorialOutline tntOutline;
    public void TNTModeUpdate()
    {
        if (mode._TNT_State == Mode.TNT_State.Tree1)
        {
            //once
            if (!tntOncePlayed)
            {
                TryGetComponent(out tntOutline);
                tntOncePlayed = true;

                _DA.AnimateOnce("Welcome to the Tutorial", announcer);
                _DA.DrawOnce(
                    "Follow the arrow below. Use the left-hand joystick to move, or use the right-hand joystick to teleport.\r\n請跟隨下方箭頭移動, 使用左手的操縱桿移動, 或者使用右手的操縱桿傳送。",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("Tree").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("Tree")[0].transform;

                tntOutline.joystickL.enabled = true;
                tntOutline.joystickR.enabled = true;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("Tree", _Player.GetComponent<Player>());
        } 

        else if (mode._TNT_State == Mode.TNT_State.Tree2)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Press A button on the right controller to activate the tool stand.\r\n按右側控制器上的 A 按鈕啟動工具架\r\n",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);

                tntOutline.joystickL.enabled = false;
                tntOutline.joystickR.enabled = false;

                tntOutline.a.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Tree3)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Swing the hand to swivel the tool stand go left or right.\r\n擺動右手去令工具架旋轉",
                    eventChecklist);

                tntOutline.a.enabled = false;

                tntOutline.axe.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Tree4)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Reach out and touch the wooden handle of the axe, Then hold the Middle Finger button to grab the axe.\r\n伸手去觸摸斧頭的木柄,並按住中指去拾起它",
                    eventChecklist);

                tntOutline.triggerL.enabled = true;
                tntOutline.triggerR.enabled = true;
            }

            if (tntOutline.axe.GetComponent<XRGrabInteractable>().isSelected)
                TNTModeJumpState();
        }

        else if (mode._TNT_State == Mode.TNT_State.Tree5)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Go near the tree and chop it, then the minigame will start, you need to chop to the green area as soon as possible.\r\n走到樹的附近,然後劈它一下,小遊戲就會開始,你需要盡快劈到綠色的區域",
                    eventChecklist);

                tntOutline.triggerL.enabled = false;
                tntOutline.triggerR.enabled = false;

                tntOutline.tree.enabled = true;
            }
        }




        else if (mode._TNT_State == Mode.TNT_State.Stone1)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("Tutorial 2: Stone", announcer);
                _DA.DrawOnce(
                    "Follow the arrow below to the next area.\r\n跟隨下方箭頭前往下一個區域",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("Stone").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("stone")[0].transform;

                tntOutline.tree.enabled = false;

                tntOutline.joystickL.enabled = true;
                tntOutline.joystickR.enabled = true;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("Stone", _Player.GetComponent<Player>());
        }

        else if (mode._TNT_State == Mode.TNT_State.Stone2)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Press A button on the right controller to activate the tool stand.\r\n按右側控制器上的 A 按鈕啟動工具架\r\n",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);

                tntOutline.joystickL.enabled = false;
                tntOutline.joystickR.enabled = false;

                tntOutline.a.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Stone3)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Swing the hand to swivel the tool stand go left or right.\r\n擺動右手去令工具架旋轉",
                    eventChecklist);

                tntOutline.a.enabled = false;

                tntOutline.axe.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Stone4)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Reach out and touch the wooden handle of the pickaxe, Then hold the Middle Finger button to grab the pickaxe.\r\n伸手去觸摸鋤頭的木柄,並按住中指去拾起它",
                    eventChecklist);

                tntOutline.triggerL.enabled = true;
                tntOutline.triggerR.enabled = true;
            }

            if (tntOutline.pickaxe.GetComponent<XRGrabInteractable>().isSelected)
                TNTModeJumpState();
        }

        else if (mode._TNT_State == Mode.TNT_State.Stone5)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Walk up to the stone and dig it, then the minigame will start, you need to dig the stone in rhythm.\r\n走到石頭的附近,然後挖掘它一下,小遊戲就會開始,你需要跟住節奏挖掘石頭",
                    eventChecklist);

                tntOutline.triggerL.enabled = false;
                tntOutline.triggerR.enabled = false;

                tntOutline.stone.enabled = true;
            }
        }

        //else if (mode._TNT_State == Mode.TNT_State.Waiting_CutTree)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("2. Press A to select the Axe\nHew the tree", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
        //    }
        //
        //}
        //
        //else if (mode._TNT_State == Mode.TNT_State.Waiting_GoStoneArea)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.AnimateOnce("You get 10 wood", announcer);
        //        _DA.DrawOnce("3. Follow the Arrow & Find a stone", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
        //        if (AreaTrigger.FindAreasByID("TNT.StoneArea1").Count > 0)
        //            _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("TNT.StoneArea1")[0].transform;
        //    }
        //
        //    //update
        //    TNTModeCheckPlayerInAreaAndJumpState("TNT.StoneArea1", _Player.GetComponent<Player>());
        //
        //}
        //
        //else if (mode._TNT_State == Mode.TNT_State.Waiting_MineStone)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("4.Press A to select the pickaxe\nMine the stone", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
        //    }
        //
        //}
        //
        //else if (mode._TNT_State == Mode.TNT_State.Waiting_GoBuildArea)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.AnimateOnce("You get 5 Stone", announcer);
        //        _DA.DrawOnce("5.Follow the Arrow", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
        //        if (AreaTrigger.FindAreasByID("TNT.BuildArea1").Count > 0)
        //            _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("TNT.BuildArea1")[0].transform;
        //    }
        //
        //    //update
        //    TNTModeCheckPlayerInAreaAndJumpState("TNT.BuildArea1", _Player.GetComponent<Player>());
        //
        //}
        //
        //else if (mode._TNT_State == Mode.TNT_State.Waiting_BuildPhyTower)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("6. Grab book and turn to page 2\nselect the arrow Tower base", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);
        //    }
        //
        //}
        //
        //else if (mode._TNT_State == Mode.TNT_State.Waiting_BuildFinished)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("7. ...building...", eventChecklist);
        //    }
        //
        //}
        //
        //
        else if (mode._TNT_State == Mode.TNT_State.WaitEnd)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;
        
                _DA.AnimateOnce("Build Done!!", announcer);
                _DA.DrawOnce("we will send you to main menu soon", eventChecklist);
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
                //mode.gameMode = Mode.GameMode.FULL_MODE;
                //eventChecklist.ClearText();
                //_DA.Reset();

                //tp to lobby
            }
        
        }
        
        //else if (mode._TNT_State == Mode.TNT_State.WaitEnd)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("monster is arriving, defence the core", eventChecklist);
        //    }
        //
        //    if (GameObject.FindGameObjectsWithTag("Monster").Length < 3)
        //    {
        //        TNTModeJumpState();
        //    }
        //
        //}

        //else if (mode._TNT_State == Mode.TNT_State.End)
        //{
        //    //once
        //    if (!tntOncePlayed)
        //    {
        //        tntOncePlayed = true;
        //
        //        _DA.DrawOnce("End", eventChecklist);
        //
        //        _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
        //    }
        //
        //    GameObject[] monsterList = GameObject.FindGameObjectsWithTag("Monster");
        //    if (monsterList.Length > 0)
        //        _Player.GetComponent<Player>().lookAt.target = monsterList[0].transform;
        //}
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
