using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IPlayerMovements,I_TPV
{
   [Header("Game Components")]
   public GameObject GO;
   public GameObject targetPointVisualization;
    public LookAt lookAt;
    public _1st_Director director;

    [Header("Attributes")]
    [SerializeReference] public ResourceGroupType resourceGroup;

   
   [Header("Movement Multipliers")]
   public float movementSpeed=15;
   public float rotationSpeed=50;

   #region Movements
   public void Move(Vector2 rightness_n_forwardness_R){
      Vector2 temp = rightness_n_forwardness_R;
      GO.transform.Translate(0,0,temp.y*Time.fixedDeltaTime*movementSpeed,Space.Self);
      GO.transform.Rotate(0,temp.x*Time.fixedDeltaTime*rotationSpeed,0,Space.Self);
   }
   public void Jump(){
      this.GetComponent<Rigidbody>().AddForce(Vector3.up*1000);
   }
   #endregion

   #region Setter
   public void AddResource(string resourceName, int n) { resourceGroup.Add(resourceName,n); }
   public void AddResource(int resourceIndex, int n) { resourceGroup.Add(resourceIndex, n); }
   #endregion


   #region TargetPointVisualization
   public void ShowTargetPoint(Vector2 mousePosition){
      RaycastHit rayCastHitInfo;
      Ray ray = Camera.main.ScreenPointToRay(mousePosition);
      if(Physics.Raycast(ray, out rayCastHitInfo))
      {
        targetPointVisualization.SetActive(true);
        targetPointVisualization.transform.position = rayCastHitInfo.point;
      }
      else{
         targetPointVisualization.SetActive(false);   
      }
   }
   #endregion TargetPointVisualization


}
