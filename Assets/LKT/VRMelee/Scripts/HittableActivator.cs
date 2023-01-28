using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FYP2A.VR.Melee
{
    public class HittableActivator : MonoBehaviour
    {
        static List<MeleeTarget> targetsInsideAllActivators = new List<MeleeTarget>();
        static List<MeleeTarget> oldTIAA = new List<MeleeTarget>();
        static bool staticUpdateExecuted = false;

        List<MeleeTarget> targetsInside = new List<MeleeTarget>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(StaticUpdate());
            
        }

        IEnumerator StaticUpdate()
        {
            staticUpdateExecuted = false;
            targetsInsideAllActivators.Clear();

            yield return null;

            foreach (MeleeTarget target in targetsInside)
                if (!targetsInsideAllActivators.Contains(target))
                    targetsInsideAllActivators.Add(target);

            yield return null;

            if (!staticUpdateExecuted)
            {
                staticUpdateExecuted = true;

                ActivateHitbox();
                DeactivateHitbox();

                oldTIAA = targetsInsideAllActivators.ToList<MeleeTarget>();
            }

        }

        void ActivateHitbox()
        {
            foreach (MeleeTarget target in targetsInsideAllActivators)
            {
                if (!oldTIAA.Contains(target)) //if target is in new list but not in old list, that means it joined more than one activators' collider.
                    target.EnableHitboxs();
            }
        }

        void DeactivateHitbox()
        {
            foreach (MeleeTarget target in oldTIAA)
            {
                if (!targetsInsideAllActivators.Contains(target)) //if target is in old list but not in new list, that means it leave all activators' collider.
                    target.DisableHitboxs();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            MeleeHitbox hitbox;
            if (other.gameObject.TryGetComponent<MeleeHitbox>(out hitbox))
                if (hitbox.hitboxType == MeleeHitbox.HitboxType.Target && !targetsInside.Contains(hitbox.target))
                    targetsInside.Add(hitbox.target);

        }

        private void OnTriggerExit(Collider other)
        {
            MeleeHitbox hitbox;
            if (other.gameObject.TryGetComponent<MeleeHitbox>(out hitbox))
                if (hitbox.hitboxType == MeleeHitbox.HitboxType.Target && targetsInside.Contains(hitbox.target))
                    targetsInside.Remove(hitbox.target);
        }
    }
}