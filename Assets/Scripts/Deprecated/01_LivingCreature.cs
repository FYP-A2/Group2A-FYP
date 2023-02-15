using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature{

   private int id;
   public GameObject usedModel;
   private GameObject GO;
   private int HP;
   private int dieFlag=0;

   public void Spawn(){
      Object.Instantiate(usedModel,position:new Vector3(610,62,1876),rotation:Quaternion.identity);
      //listMB.Add(GO);
   }

   //Pre-existed(?)

   /*public void Die(Sound? sound,Animation? animation){
      PlayDieSound(sound);
      PlayDieAnimation(animation);
      Destroy(this,5);
   }
      
      public void PlayDieSound(AudioClip? ac){

      }
      
      public void PlayDieAnimation(Animator? animator){

      }*/

   

}



/*

#region InGameLogics
   void Update() {
      if (HP==0){
         dieFlag=1;
      }
      if(dieFlag==1){
         //this.Die(null,null);
         dieFlag=0;
      }
   }
   #endregion
   */