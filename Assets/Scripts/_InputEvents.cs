using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _InputEvents : MonoBehaviour,IPlayerMovements_WithCamRotations
{
   //Declarations
   Custom_IA CIA;
   public GameObject player;
   public Rigidbody player_RB;
   public Camera cam;
   public Animator player_AN;

   //Data passing and arithmetic calculations (Getters)
   public Vector2 GetRightness_n_Forwardness(){ return CIA.Player.WASD.ReadValue<Vector2>(); }
   public float GetMouseX(){return CIA.Player.MouseX.ReadValue<float>();}
   public float GetMouseY(){return CIA.Player.MouseY.ReadValue<float>();}
   public bool GetSpace(){return CIA.Player.Space.triggered;}


   //Our custom-defined events based on data.
   public void Move(Vector2 rightness_n_forwardness,Animator animator){
      //Included larger rotation.
      float grav=1*Time.deltaTime;
      Vector2 temp= rightness_n_forwardness;
      player_RB.velocity= player.transform.forward*temp.y*14;
      
      player_RB.velocity= new Vector3(player_RB.velocity.x,grav*-5.8f,player_RB.velocity.z);
      player.transform.Rotate(0,temp.x*.13f,0);
   }
   public void CamRotate(float MouseX,float MouseY){
      //This is smaller rotation.
      Vector3 temp;
      temp= new Vector3(MouseY*.09f,MouseX*-.09f,0);
      cam.transform.eulerAngles -= temp;
   }
   public void Jump(){
      if(GetSpace()){player_RB.AddForce(Vector3.up*300);}
   }


   //Unity Event Sequences (Callers)
   void Awake() { CIA = new Custom_IA(); CIA.Player.Enable(); }
   void Start() { player_AN = player.GetComponent<Animator>(); }
   void Update() { Move(GetRightness_n_Forwardness(),player_AN); CamRotate(GetMouseX(),GetMouseY()); Jump();}

}
