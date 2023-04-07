using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
      float TimeRange { get => inTimeRange + outTimeRange; }
      float timeMeter = 0.25f;

      [SerializeField]
      public Queue<bool> tablature;
      public int tablatureSize = 20;
      public GameObject meterHintPrefabs;
      public float meterHintScale = 1;
      public float meterHintDisplayDuration = 0.7f;

      // Start is called before the first frame update
      new void Start()
      {
         base.Start();
         CreateTablature(tablatureSize);
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
         nowHp -= nowDamage;
         if (nowHp == 0)
            MinigameSuccess();
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
         CreateTablature(tablatureSize);

         //push one
         while (tablature.Count > 0)
         {
            StartCoroutine(PushLevel2());
            StartCoroutine(MeterHintAnimation(meterHintDisplayDuration));

            //stop push if failed
            //break 

            yield return new WaitForSeconds(timeMeter);
         }
      }

      IEnumerator PushLevel2()
      {
         float time = 0;

         //delay
         yield return new WaitForSeconds(meterHintDisplayDuration - inTimeRange);

         //set isHitTime 
         isHitTime = true;

         //cal the accuracy
         while(time < TimeRange)
         {
            if (time < inTimeRange)
               HitAccuracy = time / inTimeRange;
            else
               HitAccuracy = (time - inTimeRange) / outTimeRange;


            //stop cal if failed
            //set not isHitTime
            //break 

            yield return null;
         }

         //set not isHitTime 
         isHitTime = false;
      }


      IEnumerator MeterHintAnimation(float duration)
      {
         Transform t1 = Instantiate(meterHintPrefabs, transform).transform;
         Transform t2 = Instantiate(meterHintPrefabs, transform).transform;
         Vector3 t1StartPos = new Vector3(0, meterHintScale, 0);
         Vector3 t2StartPos = new Vector3(0, -meterHintScale, 0);
         float time = 0f;
         while (time < 1)
         {
            t1.localPosition = Vector3.Lerp(t1StartPos, Vector3.zero, Mathf.Pow(32, time-1));
            t2.localPosition = Vector3.Lerp(t2StartPos, Vector3.zero, Mathf.Pow(32, time-1));

            //call DropMeterHint if failed
            //break 

            time += Time.deltaTime/duration;
            yield return null;
         }

         Destroy(t1.gameObject);
         Destroy(t2.gameObject);
      }

      IEnumerator DropMeterHint(Transform mh) //drop hint when game failed
      {
         yield return null;


      }

      void CreateTablature(int size)
      {
         tablature.Clear();
         int randomMax = 2;

         tablature.Enqueue(false);
         tablature.Enqueue(false);
         tablature.Enqueue(true);

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

            tablature.Enqueue(result);
         }
         tablature.Enqueue(false);
         tablature.Enqueue(true);

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