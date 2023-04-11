using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP
{
    void GetHP(out float max, out float now);
    float GetHP();
    float GetMaxHP();

}
