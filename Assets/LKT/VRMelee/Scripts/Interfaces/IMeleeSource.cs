using FYP2A.VR.Melee;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IMeleeSource
{
    public float GetFightDamage(MeleeTarget target);
    //to get the living things damage from this(MeleeSource) to the MeleeTarget;

    public float GetHewDamage(MeleeTarget target);
    //to get the building/environment damage from this(MeleeSource) to the MeleeTarget;

    public XRBaseController GetGrabbingHand();
}
