using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List <Transform> targets = new List <Transform> ();
    public string targetTag;

    private void Start()
    {
        //InvokeRepeating("ClearList", 0f, 1f);
    }

    private void Update()
    {
        ClearList();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == targetTag && !targets.Contains(other.transform))
        {
            targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag && targets.Contains(other.transform))
        {
            targets.Remove(other.transform);
        }
    }

    void ClearList()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //Debug.Log(targets[0].ToString());
                Collider col;
                targets[i].TryGetComponent<Collider>(out col);
                if (targets[i] == null || col != null && !col.enabled)
                {
                    //Debug.Log("Remove");
                    targets.Remove(targets[i]);
                }
            }
        }
        if (targets.Count == 0)
        {
            //Debug.Log("Clear");
            targets.Clear();
        }
    }
}
