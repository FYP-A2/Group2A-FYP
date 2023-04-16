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

    public void Display(string text, float duration = 0)
    {
        StopAllCoroutines();
        textMeshPro.text = text;
        StartCoroutine(StartAnimation(startDuration));
        if (duration > 0)
            StartCoroutine(EndAnimation(endDuration, duration));
    }

    IEnumerator StartAnimation(float animationDuration)
    {
        float time = 0;
        Vector3 startPos = animateInFrom;
        while (time < 1)
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

        float time = 0;
        Vector3 endPos = animateInFrom;
        while (time < 1)
        {
            float t = Mathf.Pow(animateLerpExponentiationBase, time - 1);
            canvas.anchoredPosition3D = Vector3.Lerp(Vector3.zero, endPos, t);
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);

            time += Time.deltaTime / animationDuration;
            yield return null;
        }
        //canvas.anchoredPosition3D = endPos;
        //canvasGroup.alpha = 0;
    }

    public void Debug1(int n)
    {
        if (n == 0)
            Display("Title Title", 3);
        if (n == 1)
            Display("Time: 28");
        if (n == 2)
            Display("Testing Text3:" +
                "\nHahahaha" +
                "\nJJJJJ" +
                "\nJJJJJ" +
                "\nJJJJJ");
    }
}
