using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace FYP2A.VR.Melee
{
    public class MeleeSource : MonoBehaviour
    {
        
        [Header("Melee Source Property")]

        [SerializeField]
        protected MeleeHitbox hitbox;

        [SerializeField]
        protected List<MeleeHitbox> hitboxes = new List<MeleeHitbox>();

        [SerializeField]
        protected bool canFight;

        [SerializeField]
        protected bool canHew;

        [SerializeField]
        protected float fightDamage;

        [SerializeField]
        protected float hewDamage;

        [SerializeField]
        protected Vector3 velocity;
        protected Vector3 oldPosition;

        [SerializeField]
        protected float nextFightDelay = 0.3f;
        [SerializeField]
        protected float nextHewDelay = 1f;

        [SerializeField]
        protected GameObject _owner;
        public GameObject owner { get { return _owner; } }


        [Serializable]
        public struct TargetCD
        {
            public MeleeTarget target;
            public float cd;
        }
        [SerializeField]
        public List<TargetCD> foughtTargets = new List<TargetCD>();
        [SerializeField]
        public List<TargetCD> hewedTargets = new List<TargetCD>();


        // Start is called before the first frame update
        protected void Start()
        {
            if (hitbox == null)
                TryGetComponent<MeleeHitbox>(out hitbox);

            if (hitbox != null)
                hitbox.SetMeleeProperty(this);

            if (hitboxes.Count > 0)
            {
                foreach (MeleeHitbox hb in hitboxes)
                    hb.SetMeleeProperty(this);
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            if (foughtTargets.Count > 0) 
                HittedTargetCountdown(foughtTargets,out foughtTargets);
            if (hewedTargets.Count > 0)
                HittedTargetCountdown(hewedTargets,out hewedTargets);        
        }

        public virtual void HitTo(MeleeTarget target,MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {
            if (target == null)
                return;

            velocity = sourceHitbox.velocity;

            if (canFight && !CheckTargetInList(target, foughtTargets))
            {
                target.TakeFightDamage(GetFightDamage(target));
                foughtTargets.Add(new TargetCD { target = target,cd = nextFightDelay });
            }

            if (canHew && !CheckTargetInList(target, foughtTargets))
            {
                target.TakeHewDamage(GetHewDamage(target));
                hewedTargets.Add(new TargetCD { target = target, cd = nextHewDelay });
            }


        }

        public virtual void HitLeave(MeleeTarget target, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {

        }

        private void HittedTargetCountdown(List<TargetCD> targets, out List<TargetCD> o)
        {

            List<TargetCD> result = new List<TargetCD>();
            TargetCD t;

            for (int i = 0; i < targets.Count; i++)
            {
                t = targets[i];
                t.cd -= Time.deltaTime;
                if (t.cd > 0)
                    result.Add(t);
            }

            o = result;
        }

        public bool CheckTargetInList(MeleeTarget target, List<TargetCD> list)
        {
            if (list.Count == 0)
                return false;

            foreach (TargetCD t in list)
            {
                if (t.target == target)
                    return true;
            }
            return false;
        }

        public virtual float GetFightDamage(MeleeTarget target)
        {
            return fightDamage;
        }

        public virtual float GetHewDamage(MeleeTarget target)
        {
            return hewDamage;
        }

        public XRBaseController GetGrabbingHand()
        {
            if (XRInteractorList.directInteractorLeft.hasSelection && XRInteractorList.directInteractorLeft.IsSelecting(GetComponent<XRGrabInteractable>()))
                return XRInteractorList.baseControllerLeft;
            else if (XRInteractorList.directInteractorRight.hasSelection && XRInteractorList.directInteractorRight.IsSelecting(GetComponent<XRGrabInteractable>()))
                return XRInteractorList.baseControllerRight;
            else
                return null;
        }

    }
}
