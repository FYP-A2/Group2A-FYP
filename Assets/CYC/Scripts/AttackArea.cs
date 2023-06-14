using System.Collections.Generic;
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
                if (targets[i] != null)
                {
                    targets[i].TryGetComponent<Collider>(out col);
                    if (col != null && !col.enabled)
                    {
                        targets.Remove(targets[i]);
                        break;
                    }
                }
                
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
