using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Diagnostics;

public class Timer : MonoBehaviour
{
    public TMP_Text TMP_Text;
    public CommonTMP VR_TMP;

    public float remaining_time;
    public float setted_time;

    public enum State { PAUSE = 0, RUNNING = 1, FINISHED = 2 }
    public State _State = State.PAUSE;



    void Update()
    {
        if (_State == State.RUNNING)
        {
            if (remaining_time > 0)
            {
                remaining_time -= Time.deltaTime;
                TimeSpan timeSpan = new TimeSpan(0, 0, (int)(remaining_time));
                if (TMP_Text != null)
                    TMP_Text.SetText(timeSpan.ToString(@"mm\:ss"));
                if (VR_TMP != null)
                    VR_TMP.Display(timeSpan.ToString(@"mm\:ss"),0,true);
            }

            if (remaining_time <= 0)
            {
                if (TMP_Text != null)
                    TMP_Text.SetText("Time's up!");
                if (VR_TMP != null)
                    VR_TMP.Display("Time's up!",4f);
                _State = State.FINISHED;
            }
        }
    }

    public void Init(float setted_time)
    {
        this.setted_time = setted_time;
        remaining_time = setted_time;

        VR_TMP.Display(new TimeSpan(0, 0, (int)(remaining_time)).ToString(@"mm\:ss"));
    }

    public void Reset()
    {
        _State = State.PAUSE;
    }

    public string ToString()
    {
        return _State.ToString() + "S:" + setted_time + " R:" + remaining_time;
    }
    public State GetCurrState()
    {
        return _State;
    }
    public void SetStateRunning()
    {
        this._State = State.RUNNING;
    }
    public void SetStateFinished()
    {
        this._State = State.RUNNING;
    }
}
