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

    MyJSONClass mjc;
    public _1st_Director FirstDirector;

    void Start(){
        if(debuggerON && File.Exists(@"C:\FYP-Json\LiveData.json")){
            StartCoroutine(ReadWriteLiveData());
        }
    }

    IEnumerator ReadWriteLiveData(){
        mjc = new MyJSONClass();

        



        while(true){
            mjc.currentMode=FirstDirector.mode.gameMode.ToString();
        
            mjc.current_WLC = FirstDirector._WLC_State.WLC_State.ToString();

            mjc.current_RoundProcess=FirstDirector.mode._FullMode_State.ToString();

            mjc.specialEvent_InChecking="Testing";
            
            string output = JsonConvert.SerializeObject(mjc);
            Debug.Log("Sending"+output);

            try{
                sw = new StreamWriter(@"C:\FYP-Json\LiveData.json");
                sw.Write(output);
                sw.Close();
            }
            catch(Exception ex){Debug.Log("IOE.");}

            yield return new WaitForSeconds(1);
        }
    }
}
class MyJSONClass {
    public string currentMode { get; set; }
    public string current_WLC { get; set; }
    public string current_RoundProcess { get; set; }
    public string specialEvent_InChecking { get;set; }
}
