using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IPlayer
{
   [Header("Game Components")]
   public GameObject GO;
   public GameObject targetPointVisualization;

    [Header("Attributes")]
    public ResourceGroupTypeSO resourceGroupDefault;
    [SerializeReference] public ResourceGroupType resourceGroup;

   
   [Header("Movement Multipliers")]
   public float movementSpeed=15;
   public float rotationSpeed=50;

    void Start()
    {
        resourceGroup = new ResourceGroupType(resourceGroupDefault.resources);

        resourceGroup.SetAmount("dfsf", 10);
        resourceGroup.SetAmount(1, 20);
    }



   public void Move(Vector2 rightness_n_forwardness_R){
      Vector2 temp = rightness_n_forwardness_R;
      GO.transform.Translate(0,0,temp.y*Time.fixedDeltaTime*movementSpeed,Space.Self);
      GO.transform.Rotate(0,temp.x*Time.fixedDeltaTime*rotationSpeed,0,Space.Self);
   }
   public void Jump(){
      this.GetComponent<Rigidbody>().AddForce(Vector3.up*1000);
   }

   #region Setter
   //Support negative numbers for using resources.
   public void AddWood(int addAmount){ resourceGroup.woodAmount+=addAmount; }
   public void AddStone(int addAmount){ resourceGroup.stoneAmount+=addAmount; }
   public void AddCoin(int addAmount){ resourceGroup.coinAmount+=addAmount; }
   
   public void AddIceOre(int addAmount){ resourceGroup.iceOreAmount+=addAmount; }
   public void AddFireOre(int addAmount){ resourceGroup.fireOreAmount+=addAmount; }
   public void AddPhysicalOre(int addAmount){ resourceGroup.physicalOreAmount+=addAmount; }
   public void AddElectroOre(int addAmount){ resourceGroup.electroOreAmount+=addAmount; }
   public void AddToxicOre(int addAmount){ resourceGroup.toxicOreAmount+=addAmount; }
   
   public void AddIcePearl(int addAmount){ resourceGroup.icePearlAmount+=addAmount; }
   public void AddFirePearl(int addAmount){ resourceGroup.firePearlAmount+=addAmount; }
   public void AddPhysicalPearl(int addAmount){ resourceGroup.physicalPearlAmount+=addAmount; }
   public void AddElectroPearl(int addAmount){ resourceGroup.electroPearlAmount+=addAmount; }
   public void AddToxicPearl(int addAmount){ resourceGroup.toxicPearlAmount+=addAmount; }
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
