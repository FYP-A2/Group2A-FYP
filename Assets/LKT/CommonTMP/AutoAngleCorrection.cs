using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAngleCorrection : MonoBehaviour
{

    public Transform centerTransform;
    public Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
        StartCoroutine(AutoAngleCorrection1());

    }

    IEnumerator AutoAngleCorrection1()
    {
        transform.SetParent(centerTransform);
        Vector3 locPos = transform.localPosition;
        Quaternion locRot = transform.localRotation;
        transform.SetParent(originalParent);

        while (true)
        {
            transform.SetParent(centerTransform);
            transform.localPosition = locPos;
            transform.localRotation = locRot;
            transform.SetParent(originalParent);

            yield return new WaitForSeconds(5);
        }
    }

}
