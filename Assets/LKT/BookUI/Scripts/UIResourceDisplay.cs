using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceDisplay : MonoBehaviour
{
   public ResourceGroupType playerResource;
   public ResourceGroupTypeSO compareResource;

   public enum GetByWhat { NAME, INDEX }
   public GetByWhat getByWhat = GetByWhat.NAME;

   public string resourceName;
   public int resourceIndex = 0;

   [Header("Display")]
   public Text textAmount;
   public Image imageIcon;
   public Color colorNormal;
   public Color colorAbnormal;

    [Header("Cheat Mode")]
    public bool cheatMode;

    private void Start()
   {
      if (playerResource == null)
      {
         playerResource = ResourceGroupType.Instance;
      }

      textAmount.color = colorNormal;
   }
   private void Update()
   {
      if (playerResource == null)
         return;

      if (compareResource == null)
      {
         if (getByWhat == GetByWhat.NAME)
         {
            textAmount.text = playerResource.GetAmount(resourceName).ToString();
            imageIcon.sprite = playerResource.FindResourceByName(resourceName).icon;
         }
         else
         {
            textAmount.text = playerResource.GetAmount(resourceIndex).ToString();
            imageIcon.sprite = playerResource.resources[resourceIndex].icon;
         }
      }
      else
      {
         textAmount.text = compareResource.resources[resourceIndex].amount.ToString();
         imageIcon.sprite = compareResource.resources[resourceIndex].icon;

         if (compareResource.resources[resourceIndex].amount > playerResource.GetAmount(resourceIndex))
            textAmount.color = colorAbnormal;
         else
            textAmount.color = colorNormal;
      }

   }

    public void CheatAddResource()
    {
        if (!cheatMode)
            return;
        if (getByWhat == GetByWhat.NAME)
            playerResource.Add(resourceName, 1);
        else
            playerResource.Add(resourceIndex, 1);
    }
}
