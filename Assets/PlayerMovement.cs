using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //float timeacum=0f;
   float pressingAcum=0f;
   public GameObject go;
   private Transform tf;
   private Rigidbody rb;
    private CustomIA CIA;

    void Start(){
        //tf = go.GetComponent<Transform>();
        rb = go.GetComponent<Rigidbody>();
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        CIA = new CustomIA();
        CIA.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
       ReceiveMoveSignal_n_Move();
    }

    public void ReceiveMoveSignal_n_Move(){
       Vector2 inputVector = CIA.Player.WASD.ReadValue<Vector2>();
       if(inputVector.x!=0){
        pressingAcum+=inputVector.x;
       }
       //timeacum+=Time.deltaTime;
       rb.velocity = new Vector3(inputVector.x*15,rb.velocity.y,inputVector.y*15);
       
       go.transform.rotation = Quaternion.Euler(0,inputVector.x*-1*.1f*pressingAcum,0);

       // halfok but not ok.
       //go.transform.rotation = Quaternion.Euler(0, timeacum*25 , 0);  
       //tf.rotation.x = 1;
       //rb.angularVelocity= new Vector3(inputVector.x*-1,0,inputVector.y*-1);
    }
}
