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
        public Vector3 velocity { get => GetComponent<Rigidbody>().velocity; }
        public Vector3 upDirecton { get => transform.up; }
        Vector3 oldPosition;

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
            oldPosition = transform.position;
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
            if (hitboxType == HitboxType.Target)
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
            if (hitboxType == HitboxType.Target)
                return;

            MeleeHitbox targetHitbox;
            if (other.gameObject.TryGetComponent<MeleeHitbox>(out targetHitbox))
                if (targetHitbox.hitboxType == HitboxType.Target)
                {
                    source.HitTo(targetHitbox.target, this, targetHitbox);
                    targetHitbox.target.HitBy(source, this, targetHitbox);
                }
        }
    }
}