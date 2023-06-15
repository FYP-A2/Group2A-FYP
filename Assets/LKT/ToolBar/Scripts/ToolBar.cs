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

    float oldLeftLocPos = 0;
    float oldRightLocPos = 0;

    int turnDirection = 0;

    _1st_Director dir;

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

        dir = GameObject.FindObjectOfType<_1st_Director>();

        Deactivate();
    }

    private void Update()
    {
        if (transform.localScale == orignalScale)
        {
            Debug.Log("tool bar near: " + GetPlayerNearestSlot());
            if (GetPlayerNearestSlot() == 1 && dir.mode._TNT_State == Mode.TNT_State.Tree3)
                dir.TNTModeJumpState();

            if (GetPlayerNearestSlot() == 0 && dir.mode._TNT_State == Mode.TNT_State.Stone3)
                dir.TNTModeJumpState();

            if (GetPlayerNearestSlot() == 2 && dir.mode._TNT_State == Mode.TNT_State.Repair3)
                dir.TNTModeJumpState();
        }

    }

    private void OnEnable()
    {
        StartCoroutine(Swiveling());
    }

    void FixedUpdate()
    {
        pLeft.MovePosition(handLeft.position);
        pRight.MovePosition(handRight.position);

        float targetVelocity = 5f;

        float l = oldLeftLocPos - pLeft.transform.localPosition.x;
        float r = oldRightLocPos - pRight.transform.localPosition.x;

        if ( l / Time.fixedDeltaTime > targetVelocity 
            || r / Time.fixedDeltaTime > targetVelocity)
            SwivelLeft();
        if ( l / Time.fixedDeltaTime < -targetVelocity
            || r / Time.fixedDeltaTime < -targetVelocity)
            SwivelRight();

        oldLeftLocPos = pLeft.transform.localPosition.x;
        oldRightLocPos = pRight.transform.localPosition.x;
    }

    public void ActivateOrNot()
    {

        if (gameObject.activeSelf)
            Deactivate();
        else
            Activate();

        if (dir.mode._TNT_State == Mode.TNT_State.Tree2 || dir.mode._TNT_State == Mode.TNT_State.Stone2 || dir.mode._TNT_State == Mode.TNT_State.Repair2)
            dir.TNTModeJumpState();
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
        Debug.Log("SwivelLeft");
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

    float GetDistanceOfSlotNPlayer(ToolBarSlot s)
    {
        Transform p = GameObject.FindObjectOfType<Transform>();
        return (p.position - s.transform.position).magnitude;
    }

    int GetPlayerNearestSlot()
    {
        float[] sd = new float[3];
        sd[0] = GetDistanceOfSlotNPlayer(slots[0]);
        sd[1] = GetDistanceOfSlotNPlayer(slots[2]);
        sd[2] = GetDistanceOfSlotNPlayer(slots[4]);

        int result = 0;
        float min = 1024;
        for (int i = 0;i<3;i++)
        {
            if (sd[i] < min)
            {
                min = sd[i];
                result = i;
            }
        }

        return result;
    }
}
