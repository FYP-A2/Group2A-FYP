using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    public GameObject txt, enemy;
    public float DamageShow;

    public void ShowDamage() {
        GameObject x = Instantiate(txt, enemy.transform.position, enemy.transform.rotation);
        x.GetComponent<TextMove>().SetDamage(DamageShow);
        Destroy(x,1f);
    }

}
