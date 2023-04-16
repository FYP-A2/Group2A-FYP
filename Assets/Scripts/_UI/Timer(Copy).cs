using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer_Copy : MonoBehaviour
{
   public TMP_Text TMP_Text;
   public float remaining_time=30;
   public float setted_time=30;

   public void CountDown(float setted_time){
      this.setted_time=setted_time;
      float remaining_time = setted_time;
      
   }

   void Update() {
      if(remaining_time>0){
         remaining_time-=Time.deltaTime;
         TimeSpan timeSpan = new TimeSpan(0,0,(int)(remaining_time));
         TMP_Text.SetText(timeSpan.ToString(@"mm\:ss"));
      }
      if(remaining_time<=0){
         TMP_Text.SetText("Time's up!");
         
      }
   }
}
