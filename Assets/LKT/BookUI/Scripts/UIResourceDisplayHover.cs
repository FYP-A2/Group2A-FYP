using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ResourceGroupType;

public class UIResourceDisplayHover : MonoBehaviour
{
   [HideInInspector]
   public UIBook book;
   [HideInInspector]
   public ResourceGroupType ownedResource;

   public List<UIResourceDisplay> UIRDSingles;
   bool activeState = false, move = false;
   float moveTime = 0, moveSpeed = 1.5f;
   float showSpeed = 3f;
   public Vector3 moveEndPos;


   public Image background;



   private void Start()
   {
      
   }

   private void OnEnable()
   {
      background = GetComponent<Image>();
      StartCoroutine(MoveAnimation());
      StartCoroutine(ActiveAnimation());
   }

   //on 
   //off
   public void DisplayOn(Vector2 Pos, ResourceGroupTypeSO neededResource)
   {
      SetPosition(Pos);
      SetResource(neededResource, ownedResource);
      UIActive(true);
   }
   public void DisplayOff()
   {
      UIActive(false);
   }



   void UIActive(bool condition)
   {
      activeState = condition;
      if (condition)
      {
         gameObject.SetActive(true);
      }
   }

   IEnumerator ActiveAnimation()
   {
      float activeAnimationProcess = 0;
      while (true)
      {
         if (activeState && activeAnimationProcess < 1)
         {
            if (activeAnimationProcess < 0)
               activeAnimationProcess = 0;
            background.color = new Color(background.color.r, background.color.g, background.color.b, activeAnimationProcess);
            activeAnimationProcess += Time.deltaTime * showSpeed;
         }
         else if(!activeState && activeAnimationProcess > 0)
         {
            if (activeAnimationProcess > 1)
               activeAnimationProcess = 1;
            background.color = new Color(background.color.r, background.color.g, background.color.b, activeAnimationProcess);
            activeAnimationProcess -= Time.deltaTime * showSpeed;
         }
         if (!activeState && gameObject.activeSelf && activeAnimationProcess <= 0)
         {
            gameObject.SetActive(false);
         }

         yield return null;

      }
   }

   //setpos
   void SetPosition(Vector2 pos)
   {
      moveEndPos = pos;
      moveEndPos.z = 96;
      move = true;
      moveTime = 0;
   }

   IEnumerator MoveAnimation()
   {
      //Vector3 startPos;
      while (true)
      {
         if (move && moveTime < 1)
         {
            moveTime += Time.deltaTime * moveSpeed;
            RectTransform rt = gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector3.Lerp(transform.localPosition, moveEndPos, moveTime);
         }
         if (moveTime >= 1)
         {
            move = false;
         }

         yield return null;
      }
   }

   //setRes
   void SetResource(ResourceGroupTypeSO neededResource, ResourceGroupType ownedResource)
   {
      DisableAllSingle();
      int n = 0;
      for (int i = 0; i<neededResource.resources.Count; i++)
      {
         if (neededResource.resources[i].amount != 0)
         {
            UIRDSingles[n].playerResource = ownedResource;
            UIRDSingles[n].compareResource = neededResource;
            UIRDSingles[n].resourceIndex = i;
            UIRDSingles[n].gameObject.SetActive(true);
            n++;
         }
      }
   }

   //disableAllSingle
   void DisableAllSingle()
   {
      foreach (UIResourceDisplay single in UIRDSingles)
      {
         single.gameObject.SetActive(false);
      }
   }

   //
}
