/*using System.Collections;
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


   float g=9.81f;
   float v;

   //Data passing and arithmetic calculations (Getters)
   public Vector2 GetRightness_n_Forwardness(){ return CIA.Player.WASD.ReadValue<Vector2>(); }
   public float GetMouseX(){return CIA.Player.MouseX.ReadValue<float>();}
   public float GetMouseY(){return CIA.Player.MouseY.ReadValue<float>();}
   public bool GetSpace(){return CIA.Player.Space.triggered;}


   //Our custom-defined events based on data.
   public void Move(Vector2 rightness_n_forwardness,Animator animator){
      
      Vector2 temp= rightness_n_forwardness;
      if(temp.y>0 && GetSpace()){player_RB.AddForce(Vector3.up*500*1.7f);}
      //Included larger rotation.
      // worked player.transform.position += Vector3.forward * rightness_n_forwardness.y * .02f;
      player.transform.Translate(0,0,temp.y*.07f,Space.Self);
      
      player.transform.Rotate(0,temp.x*.23f,0,Space.Self);
      
   }
   public void CamRotate(float MouseX,float MouseY){
      //This is smaller rotation.
      Vector3 temp;
      temp= new Vector3(MouseY*.09f,MouseX*-.09f,0);
      cam.transform.eulerAngles -= temp;
   }
   public void Jump(){     
      if(GetSpace()){player_RB.AddForce(Vector3.up*20,ForceMode.Impulse);}
   }


   //Unity Event Sequences (Callers)
   void Awake() { CIA = new Custom_IA(); CIA.Player.Enable(); }
   void Start() { player_AN = player.GetComponent<Animator>(); }
   void Update() { Move(GetRightness_n_Forwardness(),player_AN); CamRotate(GetMouseX(),GetMouseY()); }
   private void FixedUpdate() {
      Jump();
   }
}
*/
