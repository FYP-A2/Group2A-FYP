using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FYP2A.VR.Melee.Target
{

    public class MeleeTargetCommonBuild : MeleeTarget
    {
        public delegate void AddBuildProgressHandler(float buildProgress);

        public event AddBuildProgressHandler AddBuildProgressEvent;

        [Header("")]
        [Header("Melee Target Common Build Motion")]
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

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            AddBuildProgressEvent += MeleeTargetCommonBuild_addBuildProgressEvent;

            cA0 = hitboxes[0].GetComponent<Renderer>().material.color;
            cA1 = hitboxes[1].GetComponent<Renderer>().material.color;
            cA2 = hitboxes[2].GetComponent<Renderer>().material.color;
        }

        // Update is called once per frame
        void Update()
        {
            if (thisUpdateRoundEnter)
            {

                if (HitArea0Enter > 0)
                {
                    AddBuildProgressEvent(HitArea0ProgressAdd);
                    StartCoroutine(DisplayHitted(hitboxes[0], cA0, colorAfterHit, 0.3f));
                }
                else if (HitArea1Enter > 0) {
                    AddBuildProgressEvent(HitArea1ProgressAdd);
                    StartCoroutine(DisplayHitted(hitboxes[1], cA1, colorAfterHit, 0.3f));
                }
                else if (HitArea2Enter > 0)
                {
                    AddBuildProgressEvent(HitArea2ProgressAdd);
                    StartCoroutine(DisplayHitted(hitboxes[2], cA2, colorAfterHit, 0.3f));
                }


                thisUpdateRoundEnter = false;
            }
        }

        public override void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {
            if (source.gameObject.name != "Hammer")
                return;

            if (CheckAllAreaNoEnter())
            {
                thisUpdateRoundEnter = true;
            }

            if (hitboxes.IndexOf(targetHitbox) == 0)
                HitArea0Enter++;
            if (hitboxes.IndexOf(targetHitbox) == 1)
                HitArea1Enter++;
            if (hitboxes.IndexOf(targetHitbox) == 2)
                HitArea2Enter++;
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

        bool CheckAllAreaNoEnter()
        {
            return HitArea0Enter == 0 && HitArea1Enter == 0 && HitArea2Enter == 0;
        }

        private void MeleeTargetCommonBuild_addBuildProgressEvent(float buildProgress)
        {

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
    }

}
