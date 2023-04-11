using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FYP2A.VR.Melee
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class MeleeHitbox : MonoBehaviour
    {
        public enum HitboxType
        {
            Source, Target
        }
        internal HitboxType hitboxType;
        internal MeleeSource source;
        internal MeleeTarget target;
        public Vector3 velocity { get => (oldCenterOfMass - GetComponent<Rigidbody>().worldCenterOfMass)/Time.deltaTime; }
        public Vector3 upDirecton { get => transform.up; }
        Vector3 oldCenterOfMass;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void LateUpdate()
        {
            oldCenterOfMass = GetComponent<Rigidbody>().worldCenterOfMass;
        }

        public void SetMeleeProperty(MeleeSource source)
        {
            hitboxType = HitboxType.Source;
            this.source = source;
        }

        public void SetMeleeProperty(MeleeTarget target)
        {
            hitboxType = HitboxType.Target;
            this.target = target;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hitboxType != HitboxType.Source)
                return;

            MeleeHitbox targetHitbox;
            if (collision.gameObject.TryGetComponent<MeleeHitbox>(out targetHitbox))
                if (targetHitbox.hitboxType == HitboxType.Target)
                {
                    source.HitTo(targetHitbox.target, this, targetHitbox);
                    targetHitbox.target.HitBy(source, this, targetHitbox);
                }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (hitboxType != HitboxType.Source)
                return;

            MeleeHitbox targetHitbox;
            if (other.gameObject.TryGetComponent<MeleeHitbox>(out targetHitbox))
                if (targetHitbox.hitboxType == HitboxType.Target)
                {
                    source.HitTo(targetHitbox.target, this, targetHitbox);
                    targetHitbox.target.HitBy(source, this, targetHitbox);
                }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (hitboxType != HitboxType.Source)
                return;

            MeleeHitbox targetHitbox;
            if (collision.gameObject.TryGetComponent<MeleeHitbox>(out targetHitbox))
                if (targetHitbox.hitboxType == HitboxType.Target)
                {
                    source.HitLeave(targetHitbox.target, this, targetHitbox);
                    targetHitbox.target.HitLeave(source, this, targetHitbox);
                }
        }

        private void OnTriggerExit(Collider other)
        {
            if (hitboxType != HitboxType.Source)
                return;

            MeleeHitbox targetHitbox;
            if (other.gameObject.TryGetComponent<MeleeHitbox>(out targetHitbox))
                if (targetHitbox.hitboxType == HitboxType.Target)
                {
                    source.HitLeave(targetHitbox.target, this, targetHitbox);
                    targetHitbox.target.HitLeave(source, this, targetHitbox);
                }
        }
    }
}