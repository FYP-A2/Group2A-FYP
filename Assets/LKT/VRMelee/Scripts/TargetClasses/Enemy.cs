using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.Melee.Target
{
    public class Enemy : MeleeTarget
    {
        [Header("Enemy")]
        public float hp;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public override void TakeFightDamage(float damage)
        {
            base.TakeFightDamage(damage);

            hp -= damage;
            if (hp < 0)
                hp = 0;

            Debug.Log("damaged " + gameObject.name + " : " + damage);
        }

    }
}