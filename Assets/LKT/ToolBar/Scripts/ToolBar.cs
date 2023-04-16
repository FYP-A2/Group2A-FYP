using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolBar : MonoBehaviour
{
    public List<ToolBarSlot> slots = new List<ToolBarSlot>();
    Vector3 orignalScale;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in slots)
        {
            slot.StoreTransform();
            slot.toolBar = this;
        }


        orignalScale = transform.localScale;

        gameObject.SetActive(false);
    }

    public void ActivateOrNot()
    {
        if (gameObject.activeSelf)
            Deactivate();
        else
            Activate();
    }



    public void Activate()
    {
        if (!gameObject.activeSelf)
        {
            foreach (var slot in slots)
                slot.RestoreTransform();
            gameObject.SetActive(true);
            StartCoroutine(ActivateAnimation());
        }
    }

    IEnumerator ActivateAnimation()
    {
        transform.localScale = Vector3.one * 0.000001f;
        transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y + 45, 0);
        float t = 0;
        while (t < 0.3f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, orignalScale, t);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, cam.transform.localEulerAngles, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = orignalScale;
    }

    public void Deactivate()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(DeactivateAnimation());
        }
    }

    IEnumerator DeactivateAnimation()
    {
        transform.localScale = orignalScale;
        Vector3 finalScale = Vector3.one * 0.000001f;
        Vector3 finalRotation = new Vector3(0, cam.transform.localEulerAngles.y + 45, 0);
        float t = 0;
        while (t < 0.3f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, t);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, finalRotation, t);
            t += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
