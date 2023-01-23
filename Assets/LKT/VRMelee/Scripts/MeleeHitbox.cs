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
        public HitboxType hitboxType;
        public MeleeSource source;
        public MeleeTarget target;
        public Vector3 velocity;
        Vector3 oldPosition;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            velocity = (transform.position - oldPosition) / Time.deltaTime;
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

            Debug.Log("OnCollisionEnter");
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
            Debug.Log("OnTriggerEnter");
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