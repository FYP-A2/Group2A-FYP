using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
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
            yield return null;
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