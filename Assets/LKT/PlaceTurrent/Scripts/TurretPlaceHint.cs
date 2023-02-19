using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPlaceHint : MonoBehaviour
{
    [SerializeField]
    private GameObject hintText;
    [SerializeField]
    private GameObject hintTextCanvas;
    [SerializeField]
    private Transform hintTextOrbit;

    public void SetDisplayHint(string hint)
    {
        if (hintText == null)
            return;
        if (hintText.GetComponent<Text>() == null)
            return;

        Text textCpn = hintText.GetComponent<Text>();
        if (textCpn.text != hint)
            textCpn.text = hint;
    }
    public void DisplayHint(bool condition,Vector3 lockAtPos)
    {
        if (hintTextCanvas == null)
            return;

        if (condition && hintTextCanvas.activeSelf == false)
            hintTextCanvas.SetActive(true);
        else if (!condition && hintTextCanvas.activeSelf == true)
            hintTextCanvas.SetActive(false);

        if (hintTextOrbit != null && condition)
            hintTextOrbit.LookAt(lockAtPos);
    }
}
