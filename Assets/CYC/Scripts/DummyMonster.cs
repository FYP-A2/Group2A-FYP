using System.Collections;
using UnityEngine;

public class DummyMonster : Monster
{
    protected override void Idle()
    {
        
    }

    public override void TakeDamage(int phyDamage, int magicDamage)
    {
        int finalDamage = (int)(phyDamage * (1 - defense) + magicDamage * (1 - resistance));
        hp -= finalDamage;
        if (animator != null)
            animator.SetTrigger(ani_GetHit);
        ShowDamage(finalDamage, Color.black);
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(ResetHP());
    }

    IEnumerator ResetHP()
    {
        yield return new WaitForSeconds(0.5f);
        if (hp<GetMaxHP())
        {
            hp = (int)GetMaxHP();
        }
    }
}
