using FYP2A.VR.Melee.Source;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.Melee.Target
{
    public class MeleeTargetTree : MeleeTarget
    {

        [Header("")]
        [Header("Melee Target Tree")]
        Tree tree;
        [SerializeField]
        float hp = 5;
        float nowHp;
        bool minigameOn = false;

        bool minigameCanInput = false;

        //Grow treeGrow;


        [Header("Hit Duration & Difficulty")]

        [SerializeField]
        float defaultHitDuration = 0.7f;
        float hitDuration = 0.7f;

        [SerializeField]
        float difficultyFactorMin = 1;
        [SerializeField]
        float difficultyFactorMax = 1.2f;
        float OneOverDifficultyFactor { get => 1 / UnityEngine.Random.Range(difficultyFactorMin, difficultyFactorMax); }



        [Header("Random Rest")]

        [SerializeField]
        float randomRestMin = 0.1f;
        [SerializeField]
        float randomRestMax = 0.5f;
        float RandomRest { get => UnityEngine.Random.Range(randomRestMin, randomRestMax); }



        int nowHitRingNumber = 0;
        int MaxHitRingCount { get => hitboxes.Count; }
        int RandomHitRingNumber { get => UnityEngine.Random.Range(0, MaxHitRingCount); }

        float nowDamage;



        [Header("Color")]

        [SerializeField]
        private Color colorIdle;

        [SerializeField]
        private Color colorInvisible;

        [SerializeField]
        private Color colorHitStart;

        [SerializeField]
        private Color colorHitEnd;

        [SerializeField]
        private Color colorHitCorrect;

        [SerializeField]
        private Color colorHitIncorrect;




        Coroutine minigameCheckTimeOutCoroutine;

        List<Coroutine> colorCoroutine = new List<Coroutine>();
        Coroutine colorCoroutineSingle;

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
            if (tree.TreeState != Tree.State.Matured)
                return;

            if (source.gameObject.name != "Axe")
                return;

            if (minigameOn && minigameCanInput && CheckHitRing(targetHitbox) && CheckHitCorrectRing(targetHitbox))
            {
                StopAllMinigameCheckTimeOut();
                HitCorrect(targetHitbox);
            }

            if (minigameOn && minigameCanInput && CheckHitRing(targetHitbox) && !CheckHitCorrectRing(targetHitbox))
            {
                StopAllMinigameCheckTimeOut();
                MinigameFailed(targetHitbox);
            }

            if (!minigameOn && hitbox.Equals(targetHitbox) && CheckTreeCanBeCollected())
                MinigameOn();

        }



        void MinigameStart()
        {
            hitDuration = defaultHitDuration;
            nowHp = hp;

            StartCoroutine(PushLevelAfterRestF());
        }

        IEnumerator MinigameCheckTimeOut()
        {

            yield return new WaitForSeconds(hitDuration);

            MinigameFailed(hitboxes[nowHitRingNumber]);
        }

        void StartMinigameCheckTimeOut()
        {
            if (minigameCheckTimeOutCoroutine != null)
                StopCoroutine(minigameCheckTimeOutCoroutine);
            minigameCheckTimeOutCoroutine = StartCoroutine(MinigameCheckTimeOut());
        }

        void StopAllMinigameCheckTimeOut()
        {
            if (minigameCheckTimeOutCoroutine != null)
                StopCoroutine(minigameCheckTimeOutCoroutine);
        }

        IEnumerator PushLevelAfterRestF()
        {
            minigameCanInput = false;

            yield return new WaitForSeconds(RandomRest);

            minigameCanInput = true;

            MinigameSetupStage(false);
            StartMinigameCheckTimeOut();
        }

        IEnumerator PushLevelAfterRest()
        {
            minigameCanInput = false;

            yield return new WaitForSeconds(RandomRest);

            minigameCanInput = true;

            MinigameSetupStage(true);
            StartMinigameCheckTimeOut();
        }

        void MinigameSetupStage(bool applyDifficultyFactor = true)
        {
            if (applyDifficultyFactor)
                hitDuration *= OneOverDifficultyFactor;

            nowHitRingNumber = RandomHitRingNumber;

            LerpAllHitboxsColorTo(colorIdle);
            LerpHitboxColor(hitboxes[nowHitRingNumber], colorHitStart, colorHitEnd, hitDuration);
        }

        bool CheckHitRing(MeleeHitbox hitbox)
        {
            return hitboxes.Contains(hitbox);
        }

        bool CheckHitCorrectRing(MeleeHitbox hitbox)
        {
            if (CheckHitRing(hitbox))
                return hitboxes.IndexOf(hitbox) == nowHitRingNumber;

            return false;
        }

        void HitCorrect(MeleeHitbox hitbox)
        {
            StopAllMinigameCheckTimeOut();
            nowHp -= nowDamage;

            if (nowHp > 0)
            {
                StartCoroutine(PushLevelAfterRest());
                LerpOneHitboxColorTo(hitbox, colorHitCorrect, 0.1f);
            }
            else
            {
                MinigameSuccess();
            }

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

        void MinigameOn()
        {
            minigameOn = true;
            MinigameStart();
        }

        void MinigameOff()
        {
            minigameOn = false;
            StopAllCoroutines();
            LerpAllHitboxsColorTo(colorInvisible);
        }





        void MinigameSuccess()
        {
            minigameCanInput = false;
            LerpAllHitboxsColorTo(colorHitCorrect);

            tree.RewardPlayers();

            StartCoroutine(MinigameSuccessWait1sReset());
        }

        IEnumerator MinigameSuccessWait1sReset()
        {
            yield return new WaitForSeconds(1);

            MinigameOff();
        }

        void MinigameFailed(MeleeHitbox hitbox)
        {
            minigameCanInput = false;
            LerpOneHitboxColorTo(hitbox, colorHitIncorrect, 0.1f);
            StartCoroutine(MinigameFailedWait1sReset());
        }

        IEnumerator MinigameFailedWait1sReset()
        {
            yield return new WaitForSeconds(1);

            MinigameOff();
        }

        bool CheckTreeCanBeCollected()
        {
            return true;
            //return CheckTreeGrow().GetGrow();
        }




        void LerpAllHitboxsColorTo(Color color, float duration = 0.3f)
        {
            StopAllColorCoroutine();

            foreach (MeleeHitbox hitbox in hitboxes)
                colorCoroutine.Add(LerpHitboxColorTo(hitbox, color, duration));
        }

        void StopAllColorCoroutine()
        {
            foreach (Coroutine cc in colorCoroutine)
                StopCoroutine(cc);
            colorCoroutine.Clear();

            if (colorCoroutineSingle != null)
                StopCoroutine(colorCoroutineSingle);
        }

        void LerpOneHitboxColorTo(MeleeHitbox hitbox, Color color, float duration)
        {
            if (colorCoroutineSingle != null)
                StopCoroutine(colorCoroutineSingle);

            colorCoroutineSingle = LerpHitboxColorTo(hitbox, color, duration);
        }

        void SetHitboxColor(MeleeHitbox hitbox, Color color)
        {
            hitbox.GetComponent<Renderer>().material.color = color;
        }

        void LerpHitboxColor(MeleeHitbox hitbox, Color colorStart, Color colorEnd, float duration)
        {
            if (colorCoroutineSingle != null)
                StopCoroutine(colorCoroutineSingle);

            colorCoroutineSingle = StartCoroutine(LerpHitboxColorCoroutine(hitbox, colorStart, colorEnd, duration));
        }

        IEnumerator LerpHitboxColorCoroutine(MeleeHitbox hitbox, Color colorStart, Color colorEnd, float duration)
        {
            if (duration <= 0)
                duration = 1f;
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / duration;
                hitbox.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, t);

                yield return null;
            }
        }

        Coroutine LerpHitboxColorTo(MeleeHitbox hitbox, Color color, float duration)
        {
            return StartCoroutine(LerpHitboxColorCoroutine(hitbox, hitbox.GetComponent<Renderer>().material.color, color, duration));
        }
    }
}

