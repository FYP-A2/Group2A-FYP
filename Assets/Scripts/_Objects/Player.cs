using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IPlayer
{
   [Header("Game Components")]
   public GameObject GO;

   [Header("Attributes")]
   [SerializeField]public int HP=100;
   
   [Header("Movement Multipliers")]
   public float movementSpeed=15;
   public float rotationSpeed=50;

   public void Move(Vector2 rightness_n_forwardness_R){
      Vector2 temp = rightness_n_forwardness_R;
      GO.transform.Translate(0,0,temp.y*Time.fixedDeltaTime*movementSpeed,Space.Self);
      GO.transform.Rotate(0,temp.x*Time.fixedDeltaTime*rotationSpeed,0,Space.Self);
   }
   public void Jump(){
      this.GetComponent<Rigidbody>().AddForce(Vector3.up*1000);
   }
   
}
