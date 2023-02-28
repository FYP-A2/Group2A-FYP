using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeTarget
{
    public void EnableHitboxs();

    public void DisableHitboxs();

    //Make damage to this MeleeTarget
    public void TakeFightDamageAfterCheck(float damage);
    public void TakeHewDamageAfterCheck(float damage);
}
