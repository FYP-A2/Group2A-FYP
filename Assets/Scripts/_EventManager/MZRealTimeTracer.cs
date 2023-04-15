// * It seems that no visible bugs found.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;


public class MZRealTimeTracer : MonoBehaviour
{
    bool debuggerON=true;
    StreamWriter sw;

    public MyJSONClass mjc_livedata;
    public MyJSONClass2 mjc_checkpoints;
    public _1st_Director FirstDirector;

    void Start(){
        if(debuggerON && File.Exists(@"C:\FYP-Json\LiveData.json")){
            StartCoroutine(ReadWriteLiveData());
            StartCoroutine(ReadWriteCheckpoints());
        }
    }
    #region ReadWriteLiveData Coroutine
    IEnumerator ReadWriteLiveData(){
        mjc_livedata = new MyJSONClass();

        



        while(true){
            //Read obj attr's, transfer into json, save it.

            mjc_livedata.currentMode=FirstDirector.mode.gameMode.ToString();
        
            mjc_livedata.current_WLC = FirstDirector._WLC_State.WLC_State.ToString();

            mjc_livedata.current_RoundProcess=FirstDirector.mode._FullMode_State.ToString();

            mjc_livedata.specialEvent_InChecking="Testing";
            
            string output = JsonConvert.SerializeObject(mjc_livedata);
            //Debug.Log("Sending"+output);

            try{
                sw = new StreamWriter(@"C:\FYP-Json\LiveData.json");
                sw.Write(output);
                sw.Close();
            }
            catch(Exception ex){Debug.Log("IOE.");}







            yield return new WaitForSeconds(1);
        }
    }
    #endregion

    IEnumerator ReadWriteCheckpoints(){
        mjc_checkpoints = new MyJSONClass2();

        



        while(true){
            //Read obj attr's, transfer into json, save it.
            
            string output = JsonConvert.SerializeObject(mjc_checkpoints);
            //Debug.Log("Sending"+output);

            try{
                sw = new StreamWriter(@"C:\FYP-Json\Checkpoints.json");
                sw.Write(output);
                sw.Close();
            }
            catch(Exception ex){Debug.Log("IOE.");}

            yield return new WaitForSeconds(1);
        }
    }

    public void _ApproveCheckpoint_1(){ mjc_checkpoints.cp01="Checkpoint 1 Approved: "+""; }
    public void _ApproveCheckpoint_2(){ mjc_checkpoints.cp01="Checkpoint 2 Approved: "+""; }
    public void _ApproveCheckpoint_3(){ mjc_checkpoints.cp01="Checkpoint 3 Approved: "+""; }
    public void _ApproveCheckpoint_4(){ mjc_checkpoints.cp01="Checkpoint 4 Approved: "+""; }
    public void _ApproveCheckpoint_5(){ mjc_checkpoints.cp01="Checkpoint 5 Approved: "+""; }
    public void _ApproveCheckpoint_6(){ mjc_checkpoints.cp01="Checkpoint 6 Approved: "+""; }
    public void _ApproveCheckpoint_7(){ mjc_checkpoints.cp01="Checkpoint 7 Approved: "+""; }
    public void _ApproveCheckpoint_8(){ mjc_checkpoints.cp01="Checkpoint 8 Approved: "+""; }
    public void _ApproveCheckpoint_9(){ mjc_checkpoints.cp01="Checkpoint 9 Approved: "+""; }
    public void _ApproveCheckpoint_10(){ mjc_checkpoints.cp01="Checkpoint 10 Approved: "+""; }
    public void _ApproveCheckpoint_11(){ mjc_checkpoints.cp01="Checkpoint 11 Approved: "+""; }
    public void _ApproveCheckpoint_12(){ mjc_checkpoints.cp01="Checkpoint 12 Approved: "+""; }
    public void _ApproveCheckpoint_13(){ mjc_checkpoints.cp01="Checkpoint 13 Approved: "+""; }
    public void _ApproveCheckpoint_14(){ mjc_checkpoints.cp01="Checkpoint 14 Approved: "+""; }
    public void _ApproveCheckpoint_15(){ mjc_checkpoints.cp01="Checkpoint 15 Approved: "+""; }

}
public class MyJSONClass {
    public string currentMode { get; set; }
    public string current_WLC { get; set; }
    public string current_RoundProcess { get; set; }
    public string specialEvent_InChecking { get;set; }
    public string _rmn_time { get;set; }
    public string _player_ready { get;set; }
    public string _current_stage { get;set; }
}

public class MyJSONClass2 {
    public string cp01 {get;set;}
    public string cp02 {get;set;}
    public string cp03 {get;set;}
    public string cp04 {get;set;}
    public string cp05 {get;set;}
    public string cp06 {get;set;}
    public string cp07 {get;set;}
    public string cp08 {get;set;}
    public string cp09 {get;set;}
    public string cp10 {get;set;}
    public string cp11 {get;set;}
    public string cp12 {get;set;}
    public string cp13 {get;set;}
    public string cp14 {get;set;}
    public string cp15 {get;set;}
}

