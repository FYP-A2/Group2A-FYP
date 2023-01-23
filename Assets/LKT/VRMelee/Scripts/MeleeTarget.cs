using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.Melee
{
    [RequireComponent(typeof(MeleeHitbox))]
    public class MeleeTarget : MonoBehaviour
    {

        [Header("Melee Target Property")]

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
            Debug.Log("t start");
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

        public virtual void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
        {

        }


        public void TakeFightDamageCheck(float damage)
        {
            if (fightable)
                TakeFightDamage(damage);

        }

        public virtual void TakeFightDamage(float damage)
        {

        }


        public void TakeHewDamageCheck(float damage)
        {
            if (hewable)
                TakeHewDamage(damage);
        }

        public virtual void TakeHewDamage(float damage)
        {

        }

    }
}
