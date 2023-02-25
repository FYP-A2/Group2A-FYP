using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.Melee
{
    public class MeleeTarget : MonoBehaviour , IMeleeTarget
    {

        [Header("Melee Target Property")]

        [SerializeField]
        private bool enableHitboxs = true;

        
        [SerializeField]
        private bool affectByActivator = true;

        [SerializeField]
        protected MeleeHitbox hitbox;

        [SerializeField]
        protected List<MeleeHitbox> hitboxes = new List<MeleeHitbox>();

        [SerializeField]
        protected bool fightable;

        [SerializeField]
        protected bool hewable;

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


        public virtual void EnableHitboxs()
        {
            if (!enableHitboxs && affectByActivator)
            {
                if (hitbox != null)
                    hitbox.enabled = true;

                foreach (MeleeHitbox meleeHitbox in hitboxes)
                    if (meleeHitbox != null)
                        meleeHitbox.enabled = true;

                enableHitboxs = true;
            }
        }

        public virtual void DisableHitboxs()
        {
            if (enableHitboxs && affectByActivator)
            {
                if (hitbox != null)
                    hitbox.enabled = false;

                foreach (MeleeHitbox meleeHitbox in hitboxes)
                    if (meleeHitbox != null)
                        meleeHitbox.enabled = false;

                enableHitboxs = false;
            }
        }

        public virtual void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {

        }

        public virtual void HitLeave(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {

        }


        public void TakeFightDamageAfterCheck(float damage)
        {
            if (fightable)
                TakeFightDamage(damage);

        }

        public virtual void TakeFightDamage(float damage)
        {

        }


        public void TakeHewDamageAfterCheck(float damage)
        {
            if (hewable)
                TakeHewDamage(damage);
        }

        public virtual void TakeHewDamage(float damage)
        {

        }

    }
}
