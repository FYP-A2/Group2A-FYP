using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DA_State_Class : MonoBehaviour{
   
   public enum DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2} //Draw & Animation
   public DA_State DrawState=DA_State.NOTHING, AnimationState=DA_State.NOTHING;

   #region  DA stuffs (Empty DrawOnce & Empty Animate Once Examples)
   public void DrawOnce(){ AnimationState=DA_State.UP_TO_DATE; }
   public void SetAnimationOutdated(){ AnimationState=DA_State.OUTDATED; }
   #endregion

   #region DA Series (Real Part, Polymorphism)
   public void DrawOnce(string msg,EventChecklist eventChecklist){
      if(DrawState==DA_State.NOTHING || DrawState==DA_State.OUTDATED){
         DrawState=DA_State.UP_TO_DATE;
         eventChecklist.SetText(msg);        
      }
   }
   public void AnimateOnce(string msg, Announcer announcer, float displayDuration = 5f){
      if(AnimationState==DA_State.NOTHING || AnimationState==DA_State.OUTDATED){
         AnimationState=DA_State.UP_TO_DATE;
         announcer.Announce(msg, displayDuration);
      }
   }

   public void Reset(){
      DrawState = DA_State.NOTHING;
      AnimationState = DA_State.NOTHING;
   }

   
   #endregion


}
