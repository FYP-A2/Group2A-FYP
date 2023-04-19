using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public RectTransform canvas;
    public CanvasGroup canvasGroup;

    [Header("Attributes")]
    public Vector3 animateInFrom = new Vector3(0, -1, 0);
    public float startDuration = 0.4f;
    public float endDuration = 0.4f;
    public float animateLerpExponentiationBase = 8;

    bool displaying = false;

    public void Display(string text, float duration = 0, bool noStartAnimation = false) //duration <= 0: display forever
    {
        StopAllCoroutines();
        textMeshPro.text = text;
        if (!noStartAnimation)
            StartCoroutine(StartAnimation(startDuration));
        else
        {
            canvas.anchoredPosition3D = Vector3.zero;
            canvasGroup.alpha = 1;
        }

        if (duration > 0)
            StartCoroutine(EndAnimation(endDuration, duration));
    }

    public void DisplayOff(float delay = 0)
    {
        if (displaying)
        {
            StopAllCoroutines();
            StartCoroutine(EndAnimation(endDuration, delay));
        }
    }

    IEnumerator StartAnimation(float animationDuration)
    {
        displaying = true;

        float time = 0;
        Vector3 startPos = animateInFrom;
        while (time < 1 && displaying)
        {
            float t = 1 - Mathf.Pow(animateLerpExponentiationBase, (1 - time) - 1);
            canvas.anchoredPosition3D = Vector3.Lerp(startPos, Vector3.zero, t);
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);

            time += Time.deltaTime / animationDuration;
            yield return null;
        }
        //canvas.anchoredPosition3D = Vector3.zero;
        //canvasGroup.alpha = 1;
    }

    IEnumerator EndAnimation(float animationDuration, float delay)
    {
        yield return new WaitForSeconds(delay);

        displaying = false;

        float time = 0;
        Vector3 endPos = animateInFrom;
        while (time < 1 && !displaying)
        {
            float t = Mathf.Pow(animateLerpExponentiationBase, time - 1);
            canvas.anchoredPosition3D = Vector3.Lerp(Vector3.zero, endPos, t);
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);

            time += Time.deltaTime / animationDuration;
            yield return null;
        }
        canvas.anchoredPosition3D = endPos;
        canvasGroup.alpha = 0;
    }

    public void Debug1(int n)
    {
        if (n == 0)
            Display("Title Title", 6);
        if (n == 1)
            Display("update",0,true);
        if (n == 2)
            DisplayOff();
    }
}
