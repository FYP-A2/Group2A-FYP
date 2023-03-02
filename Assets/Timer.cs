using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
   public TMP_Text TMP_Text;

   public void TimerStartToCountDown(float timeSeconds){
      StartCoroutine(CountDown(timeSeconds));
   }

   public IEnumerator CountDown(float timeSeconds){
      float settedTime=timeSeconds;
      float accumTime=0;
      while(accumTime<settedTime){
         yield return new WaitForSeconds(1);
         accumTime++;
         TimeSpan timeSpan = new TimeSpan(0,0,(int)(settedTime-accumTime));
         TMP_Text.SetText(timeSpan.ToString(@"mm\:ss"));

      }
      if(accumTime>=settedTime){
         TMP_Text.SetText("Time's up!");
      }
      yield return 0;
   }
}
