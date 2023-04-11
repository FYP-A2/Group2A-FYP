using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DA_State : MonoBehaviour{
   
   public enum _DA_State{NOTHING=0,OUTDATED=1,UP_TO_DATE=2} //Draw & Animation
   public _DA_State DrawState=_DA_State.NOTHING, AnimationState=_DA_State.NOTHING;

   #region  DA stuffs (Empty DrawOnce & Empty Animate Once Examples)
   public void DrawOnce(){ AnimationState=_DA_State.UP_TO_DATE; }
   public void SetAnimationOutdated(){ AnimationState=_DA_State.OUTDATED; }
   #endregion

   #region DA Series (Real Part, Polymorphism)
   public void DrawOnce(string msg,EventChecklist eventChecklist){
      if(DrawState==_DA_State.NOTHING || DrawState==_DA_State.OUTDATED){
         DrawState=_DA_State.UP_TO_DATE;
         eventChecklist.SetText(msg);        
      }
   }
   public void AnimateOnce(string msg, Announcer announcer){
      if(AnimationState==_DA_State.NOTHING || AnimationState==_DA_State.OUTDATED){
         AnimationState=_DA_State.UP_TO_DATE;
         announcer.Announce(msg);
      }
   }
   #endregion


}
