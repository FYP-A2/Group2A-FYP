using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using X = UnityEngine.InputSystem.InputAction.CallbackContext;

public class _InputEvents_Mine : MonoBehaviour
{
   #region Declarations

   Custom_IA CIA;
   
   [Header("Game Components")]
   public Player __Player;
   public Camera cam;

   [Header("HUD")]
   public HUD __HUD;

   [Header("TP")]
   public Teleport __TP;

   [Header("Announcer")]
   public GameObject __Announcer;



   #endregion

   #region Unity Event Sequences (Callers)
   void Awake(){ CIA = new Custom_IA(); CIA.Player.Enable(); CIA.HUD.Enable(); CIA.TP.Enable(); } 
   void Start(){ CIA.Player.Jump.performed+=JumpCall; CIA.HUD.F1.performed+=F1Call; CIA.HUD.F2.performed+=F2Call; CIA.HUD.F3.performed+=F3Call; CIA.HUD.F4.performed+=F4Call;
      CIA.TP.F5.performed+=F5Call;CIA.TP.F6.performed+=F6Call;CIA.TP.F7.performed+=F7Call;CIA.TP.F8.performed+=F8Call;
   }
   void FixedUpdate() { MoveCall(CIA.Player.WASD.ReadValue<Vector2>()); CamRotate(CIA.Player.MouseX.ReadValue<float>(),CIA.Player.MouseY.ReadValue<float>()); __Player.ShowTargetPoint(CIA.Player.LookAt.ReadValue<Vector2>());}
   #endregion

   #region Our custom-defined events based on data.
   public void JumpCall(X x){JumpCall();} public void JumpCall() { __Player.Jump(); }

   public void F1Call(X x){F1Call();} public void F1Call(){__HUD.F1();}
   public void F2Call(X x){F2Call();} public void F2Call(){__HUD.F2();}
   public void F3Call(X x){F3Call();} public void F3Call(){__HUD.F3();}
   public void F4Call(X x){F4Call();} public void F4Call(){__HUD.F4();}

   public void F5Call(X x){F5Call();} public void F5Call(){__TP.F5();}
   public void F6Call(X x){F6Call();} public void F6Call(){__TP.F6();}
   public void F7Call(X x){F7Call();} public void F7Call(){__TP.F7();}
   public void F8Call(X x){F8Call();} public void F8Call(){}

   //Larger rotation included.
   public void MoveCall(Vector2 rightness_n_forwardness){
      __Player.Move(rightness_n_forwardness);
   }

   //Smaller rotation included.
   public void CamRotate(float MouseX,float MouseY){   
      Vector3 temp;
      temp= new Vector3(MouseY*.09f,MouseX*-.09f,0);
      cam.transform.eulerAngles -= temp;
   }
   #endregion



}
