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
                    "Follow the arrow below. Use the left-hand joystick to move, or use the right-hand joystick to teleport.",
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
                    "Press A button on the right controller to activate the tool stand.",
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
                    "Swing the hand to swivel the tool stand go left or right.",
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
                    "Reach out and touch the wooden handle of the axe, Then hold the Middle Finger button to grab the axe.",
                    eventChecklist);

                tntOutline.gripL.enabled = true;
                tntOutline.gripR.enabled = true;
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
                    "Go near the tree and chop it, then the minigame will start, you need to chop to the green area as soon as possible.",
                    eventChecklist);

                tntOutline.axe.enabled = false;
                tntOutline.gripL.enabled = false;
                tntOutline.gripR.enabled = false;

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
                    "Follow the arrow below to the next area.",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("Stone").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("Stone")[0].transform;

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
                    "Press A button on the right controller to activate the tool stand.",
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
                    "Swing the hand to swivel the tool stand go left or right.",
                    eventChecklist);

                tntOutline.a.enabled = false;

                tntOutline.pickaxe.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Stone4)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Reach out and touch the wooden handle of the pickaxe, Then hold the Middle Finger button to grab the pickaxe.",
                    eventChecklist);

                tntOutline.gripL.enabled = true;
                tntOutline.gripR.enabled = true;
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
                    "Walk up to the stone and dig it, then the minigame will start, you need to dig the stone in rhythm.",
                    eventChecklist);

                tntOutline.pickaxe.enabled = false;

                tntOutline.gripL.enabled = false;
                tntOutline.gripR.enabled = false;

                tntOutline.stone.enabled = true;
            }
        }



        else if (mode._TNT_State == Mode.TNT_State.Repair1)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("Tutorial 3: Repair", announcer);
                _DA.DrawOnce(
                    "Follow the arrow below to the next area.",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("Repair").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("Repair")[0].transform;

                tntOutline.tree.enabled = false;

                tntOutline.joystickL.enabled = true;
                tntOutline.joystickR.enabled = true;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("Repair", _Player.GetComponent<Player>());
        }

        else if (mode._TNT_State == Mode.TNT_State.Repair2)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Press A button on the right controller to activate the tool stand.",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);

                tntOutline.joystickL.enabled = false;
                tntOutline.joystickR.enabled = false;

                tntOutline.a.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Repair3)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Swing the hand to swivel the tool stand go left or right.",
                    eventChecklist);

                tntOutline.a.enabled = false;

                tntOutline.hammer.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Repair4)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Reach out and touch the wooden handle of the hammer, Then hold the Middle Finger button to grab the hammer.",
                    eventChecklist);

                tntOutline.gripL.enabled = true;
                tntOutline.gripR.enabled = true;
            }

            if (tntOutline.hammer.GetComponent<XRGrabInteractable>().isSelected)
                TNTModeJumpState();
        }

        else if (mode._TNT_State == Mode.TNT_State.Repair5)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Walk near the gate and hammer it to start the minigame, aim for the middle of the wheel and keep hammering.",
                    eventChecklist);

                tntOutline.hammer.enabled = false;

                tntOutline.gripL.enabled = false;
                tntOutline.gripR.enabled = false;

                tntOutline.gate.enabled = true;
                Breakable gate = tntOutline.gate.GetComponent<GateRepair>().gate;
                gate.hp = 0;
                gate.GetComponent<Renderer>().enabled = false;
                gate.GetComponent<Collider>().enabled = false;
            }
        }




        else if (mode._TNT_State == Mode.TNT_State.Build1)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.AnimateOnce("Tutorial 4: Build", announcer);
                _DA.DrawOnce(
                    "Follow the arrow below to the next area.",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(true);
                if (AreaTrigger.FindAreasByID("Buildable").Count > 0)
                    _Player.GetComponent<Player>().lookAt.target = AreaTrigger.FindAreasByID("Buildable")[0].transform;

                tntOutline.tree.enabled = false;

                tntOutline.joystickL.enabled = true;
                tntOutline.joystickR.enabled = true;
            }

            //update
            TNTModeCheckPlayerInAreaAndJumpState("Buildable", _Player.GetComponent<Player>());
        }

        else if (mode._TNT_State == Mode.TNT_State.Build2)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Reach out and touch the book. Then hold the Middle Finger button to open it.",
                    eventChecklist);

                _Player.GetComponent<Player>().lookAt.gameObject.SetActive(false);

                tntOutline.joystickL.enabled = false;
                tntOutline.joystickR.enabled = false;

                tntOutline.book.enabled = true;
                tntOutline.gripL.enabled = true;
                tntOutline.gripR.enabled = true;
            }
            
        }

        else if (mode._TNT_State == Mode.TNT_State.Build3)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Turn to the second page and aim at the second page button with the line shot from your hand",
                    eventChecklist);

                tntOutline.book.enabled = false;
                tntOutline.gripL.enabled = false;
                tntOutline.gripR.enabled = false;

                tntOutline.page2Button.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Build4)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Aim at the button in the top left corner and press the Index Finger button",
                    eventChecklist);

                tntOutline.page2Button.enabled = false;

                tntOutline.triggerL.enabled = true;
                tntOutline.triggerR.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Build5)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Aim at the floor, choose the right spot and click the index finger button to place the defence tower",
                    eventChecklist);

                tntOutline.triggerL.enabled = false;
                tntOutline.triggerR.enabled = false;

                tntOutline.buildGround.enabled = true;
            }
        }

        else if (mode._TNT_State == Mode.TNT_State.Build6)
        {
            //once
            if (!tntOncePlayed)
            {
                tntOncePlayed = true;

                _DA.DrawOnce(
                    "Building",
                    eventChecklist);

                tntOutline.buildGround.enabled = false;
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
                timer.Init(5);
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
                SceneManager.LoadScene("StartMenu");
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
                    Assign_Timer_n_Announcer_ver_RS(45, ref NeedOf_Asn_Timer_n_Announcer);
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
                if (stageObject.currentStage <= 2)
                {
                    BeginRoundState_for_Timer_n_Announcer();

                    if (timer.GetCurrState() == Timer.State.PAUSE)
                    {
                        newSpawnManager.PathDisplay(stageObject.currentStage);
                        Assign_Timer_n_Announcer_ver_WaitingForNextStage(15, ref NeedOf_Asn_Timer_n_Announcer);
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
            if (stageObject.currentStage==3)
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
                _DA.AnimateOnce("Game will start in "+ second + " seconds", announcer);
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

            if (stageObject.currentStage >= 1 && stageObject.currentStage < 3)
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

            if (stageObject.currentStage >= 1 && stageObject.currentStage < 3)
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
        SceneManager.LoadScene("StartMenu");
    }
    #endregion

    #region // State: Defeat
    public void ShowGameOver_Once(ref bool onceBoolean)
    {
        PlayerPrefs.SetString("msg", "lose");
        SceneManager.LoadScene("StartMenu");
    }
    #endregion

    //dummy.
    public void dosth_Once(ref bool onceBoolean) { onceBoolean = false; }
}
