using FYP2A.VR.Melee;
using FYP2A.VR.Melee.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateRepair : MeleeTarget
{
    MeleeTargetCommonBuild mtcb;
    [Header("Gate Repair")]
    public Breakable gate;
    public bool canBeRepair = false;
    public bool repairing = false;

    public float maxHP = 30;
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

    private void Update()
    {
        if (!canBeRepair && gate.GetHP() <= 0)
            canBeRepair = true;

        if (repairing)
        {
            if (nowHP > 0)
                nowHP -= Time.deltaTime*5;

            mtcb.SetProgressionDisplay(nowHP, maxHP);
        }
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
        canBeRepair = false;
        mtcb.EndRepair(this);
        mtcb.AddBuildProgressEvent -= Mtcb_AddBuildProgressEvent;
        nowHP = 0;

        //do success event
        gate.GateReset();
    }

    private void Mtcb_AddBuildProgressEvent(float buildProgress)
    {
        nowHP += buildProgress / 2;

        Debug.Log("now: " + nowHP + "   maxHP: " + maxHP);

        if (nowHP >= maxHP)
        {
            EndRepair();
        }
    }
}
