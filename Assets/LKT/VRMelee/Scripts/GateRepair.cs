using FYP2A.VR.Melee;
using FYP2A.VR.Melee.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class GateRepair : MeleeTarget
{
    MeleeTargetCommonBuild mtcb;
    public GameObject gate;
    public bool canBeRepair = false;
    public bool repairing = false;

    public float maxHP = 100;
    public float nowHP = 0;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        canBeRepair = false;
        repairing = false;
        maxHP = 100;
        nowHP = 0;

        mtcb = FindObjectOfType<MeleeTargetCommonBuild>();
    }

    public override void HitBy(MeleeSource source, MeleeHitbox sourceHitbox, MeleeHitbox targetHitbox)
    {
        if (source.gameObject.name != "Hammer")
            return;

        if (!repairing && canBeRepair)
            StartRepair();
    }

    void StartRepair()
    {
        repairing = true;
        mtcb.SetRepair(this);
        mtcb.AddBuildProgressEvent += Mtcb_AddBuildProgressEvent;
    }

    public void SuspendRepair()
    {
        repairing = false;
        mtcb.AddBuildProgressEvent -= Mtcb_AddBuildProgressEvent;
    }

    void EndRepair()
    {
        repairing = false;
        mtcb.EndRepair(this);
        mtcb.AddBuildProgressEvent -= Mtcb_AddBuildProgressEvent;
        nowHP = 0;
    }

    private void Mtcb_AddBuildProgressEvent(float buildProgress)
    {
        nowHP += buildProgress / 3;
        mtcb.SetProgressionDisplay(nowHP, maxHP);

        if (nowHP >= maxHP)
        {
            EndRepair();
        }
    }
}
