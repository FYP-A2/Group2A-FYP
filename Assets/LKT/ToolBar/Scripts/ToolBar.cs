using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolBar : MonoBehaviour
{
    public List<ToolBarSlot> slots = new List<ToolBarSlot>();
    Vector3 orignalScale;
    public Camera cam;
    public GameObject camHandPrefab;
    public Transform swivel;
    public Transform handLeft;
    public Transform handRight;

    Rigidbody pLeft;
    Rigidbody pRight;

    int turnDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in slots)
        {
            slot.StoreTransform();
            slot.toolBar = this;
        }


        orignalScale = transform.localScale;

        pLeft = Instantiate(camHandPrefab).GetComponent<Rigidbody>();
        pRight = Instantiate(camHandPrefab).GetComponent<Rigidbody>();

        pLeft.transform.SetParent(cam.transform);
        pRight.transform.SetParent(cam.transform);
    }

    private void OnEnable()
    {
        StartCoroutine(Swiveling());
    }

    void FixedUpdate()
    {
        pLeft.MovePosition(handLeft.position);
        pRight.MovePosition(handRight.position);

        float targetVelocity = 10f;

        if (pLeft.velocity.z > targetVelocity || pRight.velocity.z > targetVelocity)
            SwivelRight();
        if (pLeft.velocity.z < -targetVelocity || pRight.velocity.z < -targetVelocity)
            SwivelLeft();
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
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0,cam.transform.localEulerAngles.y,0) , t);
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

    public void SwivelLeft()
    {
        if (sTime >= 1)
        {
            targetRotation = originalRotation + new Vector3(0, 120, 0);
            sTime = 0;
        }
    }

    public void SwivelRight()
    {
        if (sTime >= 1)
        {
            targetRotation = originalRotation + new Vector3(0, -120, 0);
            sTime = 0;
        }
    }

    Vector3 originalRotation = Vector3.zero;
    Vector3 targetRotation = Vector3.zero;
    float sTime = 1;

    IEnumerator Swiveling()
    {
        float duration = 0.25f;

        while (true)
        {
            if (sTime < 1)
            {
                swivel.localEulerAngles = Vector3.Lerp(originalRotation, targetRotation, sTime);

                sTime += Time.deltaTime / duration;
            }
            else
                originalRotation = targetRotation;


            yield return null;
        }
    }
}
