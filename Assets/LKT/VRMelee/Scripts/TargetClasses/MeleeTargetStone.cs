using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace FYP2A.VR.Melee.Target
{
   public class MeleeTargetStone : MeleeTarget
   {

      [Header("")]
      [Header("Melee Target Stone")]

      [SerializeField]
      float hp = 5;
      float nowHp;
      bool minigameOn = false;

      bool minigameCanInput = false;

      float nowDamage;

      bool isHitTime = false;
      float HitAccuracy = 0f;

      float inTimeRange = 0.1f;
      float outTimeRange = 0.1f;
      float timeMeter = 0.25f;

      [SerializeField]
      public List<bool> tablature = new List<bool>();
      public GameObject meterHintPrefabs;

      // Start is called before the first frame update
      new void Start()
      {
         base.Start();
         CreateTablature(20);
      }

      // Update is called once per frame
      void Update()
      {

      }


      public override void TakeHewDamage(float damage)
      {
         StartCoroutine(SetNowDamage(damage));
      }

      IEnumerator SetNowDamage(float damage)
      {
         nowDamage = damage;

         yield return null;

         nowDamage = 0;
      }

      public override void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
      {
         if (source.gameObject.name != "Pickaxe")
            return;

         if (minigameOn && minigameCanInput && isHitTime)
         {
            HitCorrect();
         }

         //if (minigameOn && minigameCanInput && !isHitTime)
         //{
         //    StopAllMinigameCheckTimeOut();
         //    MinigameFailed(targetHitbox);
         //}

         if (!minigameOn)
            MinigameOn();

      }

      void HitCorrect()
      {
         
      }

      void HitMiss()
      {

      }




      void MinigameOn()
      {
         minigameOn = true;
         MinigameStart();
      }

      void MinigameOff()
      {
         minigameOn = false;
         StopAllCoroutines();
      }

      void MinigameStart()
      {
         nowHp = hp;

         StartCoroutine(PushLevel());
      }

      IEnumerator PushLevel()
      {
         yield return new WaitForSeconds(0.7f);

         //push
         
      }

      void CreateTablature(int size)
      {
         tablature.Clear();
         int randomMax = 2;

         tablature.Add(false);
         tablature.Add(false);
         tablature.Add(true);

         for (int i = 0; i < size; i++)
         {
            bool result = false;

            if (Random.Range(0, randomMax) == 0)
            {
               result = true;
               randomMax+= 4;
            }
            else if (randomMax > 2)
               randomMax-= 4;

            tablature.Add(result);
         }
         tablature.Add(false);
         tablature.Add(true);
      }








      void MinigameSuccess()
      {
         minigameCanInput = false;

         //stone getitem
      }

      void MinigameFailed()
      {
         minigameCanInput = false;
         Invoke("MinigameOff", 1);
      }




      public override void EnableHitboxs()
      {
         base.EnableHitboxs();
         MinigameOff();
      }

      public override void DisableHitboxs()
      {
         base.DisableHitboxs();
         MinigameOff();
      }
   }
}