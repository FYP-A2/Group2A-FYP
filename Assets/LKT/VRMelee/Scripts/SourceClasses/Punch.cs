using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP2A.VR.Melee.Source
{
    public class Punch : MeleeSource
    {
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
        }


        public override float GetFightDamage(MeleeTarget target)
        {
            return fightDamage * velocity.magnitude;
        }
    }
}
