using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovements_WithCamRotations{
   //Attr: Please view on Asana.

   //Getters
   Vector2 GetRightness_n_Forwardness();
   float GetMouseX();
   float GetMouseY();
   bool GetSpace();

   //Custom-defined events based on data.
   //void Move(Vector2 rightness_n_forwardness, Animator animator);
   void CamRotate(float MouseX, float MouseY);
   void Jump();

   //Unity Event Sequences (Callers) : Please view on Asana.

}