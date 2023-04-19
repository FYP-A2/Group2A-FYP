using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

namespace FYP2A.VR.Melee.Target
{
    public class MeleeTargetStone : MeleeTarget
    {

        [Header("")]
        [Header("Melee Target Stone")]

        [SerializeField]
        public Stone stone;
        [HideInInspector]
        public float hp = 5;
        [HideInInspector]
        public float nowHp;
        bool minigameOn = false;

        bool minigameCanInput = false;

        float nowDamage;

        bool isHitTime = false;
        float HitAccuracy = 0f;
        bool hitThisRound = false;
        bool failed = false;
        bool end = false;

        public float inTimeRange = 0.1f;
        public float outTimeRange = 0.1f;
        float TimeRange { get => inTimeRange + outTimeRange; }
        public float timeMeter = 0.25f;

        [SerializeField]
        public Queue<bool> tablature = new Queue<bool>();
        public int tablatureSize = 20;
        public GameObject meterHintPrefabs;
        public float meterHintScale = 1;
        public float meterHintDisplayDuration = 0.7f;
        public Color meterHintColor1 = Color.white;
        public Color meterHintColor2 = Color.yellow;

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
            Debug.Log("hit: " + source.name);

            if (source.gameObject.name != "Pickaxe")
                return;

            if (minigameOn && minigameCanInput && isHitTime)
            {
                HitCorrect(source);
            }

            else if (minigameOn && minigameCanInput && !isHitTime)
            {
                HitMiss();
            }

            else if (!minigameOn)
                MinigameOn();

        }

        void HitCorrect(MeleeSource source)
        {
            Debug.Log("correct,  accuracy: " + HitAccuracy + " || nowHP: " + (nowHp - nowDamage));

            nowHp -= nowDamage;
            hitThisRound = true;
            if (nowHp <= 0)
                MinigameSuccess(source);
        }

        void HitMiss()
        {
            Debug.Log("miss");

            failed = true;
            MinigameFailed();
        }




        void MinigameOn()
        {
            minigameOn = true;
            MinigameStart();
        }

        void MinigameOff()
        {
            Debug.Log("gameOff");

            minigameOn = false;
            end = true;
            //StopAllCoroutines();
        }

        void MinigameStart()
        {
            Debug.Log("game start");

            nowHp = hp;
            isHitTime = false;
            hitThisRound = false;
            failed = false;
            minigameCanInput = true;
            end = false;

            StartCoroutine(PushLevel());
        }

        IEnumerator PushLevel()
        {
            yield return new WaitForSeconds(0.7f);
            CreateTablature(tablatureSize);

            //push one
            while (tablature.Count > 0)
            {
                //stop push if failed
                //break
                if (failed || end)
                    yield break;

                if (tablature.Dequeue() == true)
                {
                    StartCoroutine(PushLevel2());
                    StartCoroutine(MeterHintAnimation(meterHintDisplayDuration));
                }

                yield return new WaitForSeconds(timeMeter);
            }
        }

        IEnumerator PushLevel2() //check can hit and cal accuracy
        {
            float time = 0;

            //delay
            yield return new WaitForSeconds(meterHintDisplayDuration - inTimeRange);

            //set isHitTime 
            isHitTime = true;
            hitThisRound = false;

            //cal the accuracy
            while (time < TimeRange)
            {
                //stop cal if failed
                //set not isHitTime
                //break
                if (failed || end)
                {
                    isHitTime = false;
                    yield break;
                }

                //break if hitThisRound
                if (hitThisRound)
                {
                    isHitTime = false;
                    yield break;
                }

                //cal accuracy
                if (time < inTimeRange)
                    HitAccuracy = time / inTimeRange;
                else
                    HitAccuracy = (time - inTimeRange) / outTimeRange;

                time += Time.deltaTime;
                yield return null;
            }

            //set not isHitTime 
            isHitTime = false;

            //miss
            HitMiss();
        }


        IEnumerator MeterHintAnimation(float duration)
        {
            Transform t1 = Instantiate(meterHintPrefabs, transform).transform;
            Transform t2 = Instantiate(meterHintPrefabs, transform).transform;
            Renderer r1 = t1.GetComponent<Renderer>();
            Renderer r2 = t2.GetComponent<Renderer>();
            Vector3 t1StartPos = new Vector3(0, meterHintScale, 0);
            Vector3 t2StartPos = new Vector3(0, -meterHintScale, 0);
            float time = 0f;
            while (time < 1)
            {
                //call DropMeterHint if failed
                //break 
                if (failed || end)
                {
                    StartCoroutine(DropMeterHint(t1));
                    StartCoroutine(DropMeterHint(t2));
                    yield break;
                }

                t1.localPosition = Vector3.Lerp(t1StartPos, Vector3.zero, Mathf.Pow(32, time - 1));
                t2.localPosition = Vector3.Lerp(t2StartPos, Vector3.zero, Mathf.Pow(32, time - 1));
                if (time > 0.5f)
                {
                    float x = Mathf.Pow(32, (time - 0.5f) * 2 - 1);
                    r1.material.color = Color.Lerp(meterHintColor1, meterHintColor2, x);
                    r2.material.color = Color.Lerp(meterHintColor1, meterHintColor2, x);
                }
                time += Time.deltaTime / duration;
                yield return null;
            }

            Destroy(t1.gameObject);
            Destroy(t2.gameObject);
        }

        IEnumerator DropMeterHint(Transform mh) //drop hint when game failed
        {
            yield return null;
            Vector3 pos = mh.localPosition;
            mh.GetComponent<Renderer>().material.color = Color.red;


            float duration = 0.2f;

            float time = 0f;
            while (time < 1)
            {
                mh.localPosition = Vector3.Lerp(pos, new Vector3(0,-meterHintScale*2,0), Mathf.Pow(32, time - 1));

                time += Time.deltaTime / duration;
                yield return null;
            }

            Destroy(mh.gameObject);
        }

        void CreateTablature(int size)
        {
            tablature.Clear();
            int randomMax = 2;
            int createdBeat = 0;

            for (int i = 0; i < size; i++)
            {
                bool result = false;

                if (Random.Range(0, randomMax) == 0)
                {
                    result = true;
                    randomMax += 4;
                    createdBeat++;
                }
                else if (randomMax > 2)
                    randomMax -= 4;

                tablature.Enqueue(result);


                if (createdBeat >= hp)
                    break;
            }

        }


        void MinigameSuccess(MeleeSource source)
        {
            minigameCanInput = false;
            MinigameOff();
            stone.HewComplete(source._owner.GetComponent<Player>());
        }

        void MinigameFailed()
        {
            minigameCanInput = false;
            MinigameOff();
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