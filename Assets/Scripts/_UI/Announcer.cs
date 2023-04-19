using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Announcer : MonoBehaviour
{
    public TMP_Text TMP_Text;
    public CommonTMP VR_TMP;


    public void Announce(string message, float displayDuration = 2f)
    {
        TMP_Text.SetText(message); StartCoroutine(FadeIn_n_Out(displayDuration));
        VR_TMP.Display(message, displayDuration);
    }

    public IEnumerator FadeIn_n_Out(float displayDuration)
    {
        TMP_Text.color = new Color(TMP_Text.color.r, TMP_Text.color.g, TMP_Text.color.b, 0);
        while (TMP_Text.color.a < 1.0f) { TMP_Text.color = new Color(TMP_Text.color.r, TMP_Text.color.g, TMP_Text.color.b, TMP_Text.color.a + Time.deltaTime * .55f); yield return null; }
        yield return new WaitForSeconds(displayDuration);
        while (TMP_Text.color.a > 0.0f) { TMP_Text.color = new Color(TMP_Text.color.r, TMP_Text.color.g, TMP_Text.color.b, TMP_Text.color.a - Time.deltaTime * .65f); yield return null; }
    }
}
