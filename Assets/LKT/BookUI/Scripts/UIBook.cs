using FYP2A.VR.PlaceTurret;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UICommonButton;

public class UIBook : MonoBehaviour
{

   public Player player;
   public ResourceGroupType PlayerResource { get => player.resourceGroup; }
   public PlaceTurret placeTurret;
   public List<UIResourceDisplay> uiResourceDisplays = new List<UIResourceDisplay>();
   public List<UICommonButton> uIButtons = new List<UICommonButton>();
   public UIResourceDisplayHover uiHover;
   public UIMap uiMap;
   public Camera uiMapCam;

    // Start is called before the first frame update
    void Start()
   {
      if (uiResourceDisplays != null)
         foreach (var display in uiResourceDisplays)
            display.playerResource = PlayerResource;

      if (uIButtons != null)
         foreach (var button in uIButtons)
         {
            button.EventSelect += Select;
            button.book = this;
         }
            

      uiHover.book = this;
      uiHover.ownedResource = PlayerResource;

      uiMap.book = this;
      uiMap.cam = uiMapCam;
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void SelectExitAllButton()
   {
      if (uIButtons != null)
         foreach (var button in uIButtons)
            button.OnSelectExit();
   }

   public void Select(int n, SelectType t)
   {
      if (t == SelectType.tower)
         SelectTurret(n);
      else if (t == SelectType.pearl)
         SelectPearl(n);
   }

   public void SelectTurret(int n)
   {
      placeTurret.SetPreviewTurret(n);
   }

   public void SelectPearl(int n)
   {

   }
}
