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
        InvokeRepeating("ClearList", 0f, 1f);
    }

    private void Update()
    {
        //ClearList();
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
                if (targets[i] == null)
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
