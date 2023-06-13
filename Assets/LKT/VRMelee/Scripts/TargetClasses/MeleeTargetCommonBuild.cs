using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static Unity.VisualScripting.Member;

namespace FYP2A.VR.Melee.Target
{

    public class MeleeTargetCommonBuild : MeleeTarget
    {
        public delegate void AddBuildProgressHandler(float buildProgress);

        public event AddBuildProgressHandler AddBuildProgressEvent;

        [Header("")]
        [Header("Melee Target Common Build Motion")]

        [SerializeField]
        Transform hitArea;
        [SerializeField]
        float hitAreaOffsetRandom = 0.1f;
        [SerializeField]
        float hitAreaOffsetMax = 0.5f;

        float hitTimeCD = 0;
        float defaultHitTimeCD = 0.2f;
        float sourceHitboxVelocityNow = 0;
        float sourceHitboxVelocityMin = 3f;

        [Header("")]
        public float HitArea0ProgressAdd = 15;
        public float HitArea1ProgressAdd = 5;
        public float HitArea2ProgressAdd = 1;

        [SerializeField]
        Color colorAfterHit = Color.white;
        Color cA0;
        Color cA1;
        Color cA2;

        int HitArea0Enter = 0;
        int HitArea1Enter = 0;
        int HitArea2Enter = 0;

        bool thisUpdateRoundEnter = false;

        [SerializeField]
        Slider progressionBar;
        float progressionBarMax = 100;
        float progressionBarNow = 50;

        MeleeSource sourceNow;

        GateRepair repairNow;
        float repairActiveCD = 0;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            AddBuildProgressEvent += MeleeTargetCommonBuild_addBuildProgressEvent;

            cA0 = hitboxes[0].GetComponent<Renderer>().material.color;
            cA1 = hitboxes[1].GetComponent<Renderer>().material.color;
            cA2 = hitboxes[2].GetComponent<Renderer>().material.color;

            SuspendRepair();
        }

        // Update is called once per frame
        void Update()
        {
            //if hammer is really entered.
            if (thisUpdateRoundEnter)
            {
                if (hitTimeCD <= 0 && sourceHitboxVelocityNow > sourceHitboxVelocityMin)
                {
                    if (HitArea0Enter > 0)
                    {
                        AddBuildProgressEvent(HitArea0ProgressAdd);
                        StartCoroutine(DisplayHitted(hitboxes[0], cA0, colorAfterHit, 0.2f));
                    }
                    else if (HitArea1Enter > 0)
                    {
                        AddBuildProgressEvent(HitArea1ProgressAdd);
                        StartCoroutine(DisplayHitted(hitboxes[1], cA1, colorAfterHit, 0.2f));
                    }
                    else if (HitArea2Enter > 0)
                    {
                        AddBuildProgressEvent(HitArea2ProgressAdd);
                        StartCoroutine(DisplayHitted(hitboxes[2], cA2, colorAfterHit, 0.2f));
                    }

                    hitAreaRandomOffset();
                    hitTimeCD += defaultHitTimeCD;

                    StartCoroutine(HapticTest(sourceNow.GetGrabbingHand()));
                }

                thisUpdateRoundEnter = false;
            }

            if (hitTimeCD > 0)
            {
                hitTimeCD -= Time.deltaTime;
            }

            if (repairNow != null && repairActiveCD > 0)
            {
                repairActiveCD -= Time.deltaTime;
            }

            if (repairNow != null && repairActiveCD <= 0)
            {
                SuspendRepair();
            }
        }

        public override void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {
            if (source.gameObject.name != "Hammer")
                return;

            // if Hammer enter the hitboxes and all previous Hammer exited, then do real Hammer enter in update. 
            if (CheckAllAreaNoEnter())
            {
                sourceHitboxVelocityNow = sourceHitbox.velocity.magnitude;
                sourceNow = source;
                thisUpdateRoundEnter = true;
            }

            if (hitboxes.IndexOf(targetHitbox) == 0)
                HitArea0Enter++;
            if (hitboxes.IndexOf(targetHitbox) == 1)
                HitArea1Enter++;
            if (hitboxes.IndexOf(targetHitbox) == 2)
                HitArea2Enter++;

            repairActiveCD = 3;
        }

        public override void HitLeave(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {
            if (source.gameObject.name != "Hammer")
                return;

            if (hitboxes.IndexOf(targetHitbox) == 0)
                HitArea0Enter--;
            if (hitboxes.IndexOf(targetHitbox) == 1)
                HitArea1Enter--;
            if (hitboxes.IndexOf(targetHitbox) == 2)
                HitArea2Enter--;
        }

        //public override void EnableHitboxs()
        //{
        //    base.EnableHitboxs();
        //}

        bool CheckAllAreaNoEnter()
        {
            return HitArea0Enter == 0 && HitArea1Enter == 0 && HitArea2Enter == 0;
        }

        private void MeleeTargetCommonBuild_addBuildProgressEvent(float buildProgress)
        {

        }

        void hitAreaRandomOffset()
        {
            hitArea.localPosition += new Vector3(Random.Range(-hitAreaOffsetRandom, hitAreaOffsetRandom), 0, Random.Range(-hitAreaOffsetRandom, hitAreaOffsetRandom));

            if (hitArea.localPosition.x < -hitAreaOffsetMax)
                hitArea.localPosition += new Vector3(hitAreaOffsetMax / 2, 0, 0);
            if (hitArea.localPosition.x > hitAreaOffsetMax)
                hitArea.localPosition += new Vector3(-hitAreaOffsetMax / 2, 0, 0);

            if (hitArea.localPosition.z < -hitAreaOffsetMax)
                hitArea.localPosition += new Vector3(0, 0, hitAreaOffsetMax / 2);
            if (hitArea.localPosition.z > hitAreaOffsetMax)
                hitArea.localPosition += new Vector3(0, 0, -hitAreaOffsetMax / 2);

        }

        IEnumerator DisplayHitted(MeleeHitbox targetHitbox,Color colorStart, Color colorEnd, float duration)
        {
            float t = 0;

            Material m = targetHitbox.GetComponent<Renderer>().material;

            while (t < 1)
            {
                m.color = Color.Lerp(colorStart,colorEnd,t);

                t += Time.deltaTime / duration;

                yield return null;
            }

            while (t > 0)
            {
                m.color = Color.Lerp(colorStart, colorEnd, t);

                t -= Time.deltaTime / duration;

                yield return null;
            }
        }

        public void SetProgressionDisplay(float now)
        {
            progressionBarNow = now;
            UpdateProgressionBar();
        }

        public void SetProgressionDisplay(float now,float max)
        {
            progressionBarNow = now;
            progressionBarMax = max;
            UpdateProgressionBar();
        }

        void UpdateProgressionBar()
        {
            progressionBar.maxValue = progressionBarMax;
            progressionBar.value = progressionBarNow;
        }

        IEnumerator HapticTest(XRBaseController xrbc)
        {
            float t = 0;

            while (t < 1 && xrbc != null)
            {
                xrbc.SendHapticImpulse(WitchOfAgnesi(1f,0.26f,0.05f,t), Time.deltaTime);

                t += Time.deltaTime;
                yield return null;  
            }
        }

        float WitchOfAgnesi(float a,float b,float c,float x)
        {
            return (Mathf.Pow(a, 2) * x/c) / ((Mathf.Pow(x/c, 2) + a * b));
        }

        public void SetRepair(GateRepair repair)
        {
            if (repairNow != null)
                repairNow.SuspendRepair();
            repairNow = repair;
            repairActiveCD = 3f;

            hitArea.gameObject.SetActive(true);
        }

        //suspend when there is no activity for a long time
        public void SuspendRepair()
        {
            if (repairNow != null)
            {
                repairNow.SuspendRepair();
                repairNow = null;
            }


            hitArea.gameObject.SetActive(false);
        }

        public void EndRepair(GateRepair repair)
        {
            Debug.Log("end repair");
            if (repairNow.Equals(repair))
                repairNow = null;

            hitArea.gameObject.SetActive(false);
        }
    }

}
