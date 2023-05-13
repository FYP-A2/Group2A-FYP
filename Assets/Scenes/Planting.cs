using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planting : MonoBehaviour
{
    public List<GameObject> prefabs;
    public int plantAmount = 20;
    public int plantRangeRadius = 30;
    public float translateYBase = 0.5f;
    public float translateYMin = -0.1f;
    public float translateYMax = +0.1f;
    public float rotateMax = 1;
    public float scaleMin = 0.85f;
    public float scaleMax = 1.15f;

    public List<GameObject> planted = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < plantAmount; i++)
        {
            GameObject temp = Instantiate(prefabs[Random.Range(0,prefabs.Count)]);
            Vector3 pos;
            Vector3 rot;
            GetNextPt(out pos,out rot);
            temp.transform.position = pos;
            temp.transform.rotation = Quaternion.LookRotation(rot);
            temp.transform.LookAt(transform.up);
            temp.transform.localPosition += new Vector3(0, translateYBase + Random.Range(translateYMin,translateYMax), 0);
            temp.transform.localEulerAngles += new Vector3(Random.Range(-rotateMax, rotateMax), Random.Range(-180, 180), Random.Range(-rotateMax, rotateMax));
            float scale = Random.Range(scaleMin, scaleMax);
            temp.transform.localScale *= scale;

            Debug.Log(rot);

            planted.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetNextPt(out Vector3 pos,out Vector3 rot)
    {
        Vector3 finalPos;
        Vector3 finalRot;

        while (true) {

            Vector2 pt = GetRandomPt();
            Vector3 pt3 = new Vector3(pt.x, transform.position.y + 30, pt.y);
            Ray ray = new Ray(pt3, pt3 + Vector3.down * 50);
            RaycastHit hit;
            Physics.Raycast(ray,out hit, 50);
            if (hit.collider != null)
                if (planted.Count == 0 || (planted.Count > 0 &&!(hit.collider.gameObject.layer == 0)))
                {
                    finalPos = hit.point;
                    finalRot = hit.normal;
                    break;
                }
        }
        
        pos = finalPos;
        rot = finalRot;
    }

    Vector2 GetRandomPt()
    {
        float x = Random.Range(-plantRangeRadius, plantRangeRadius);
        float y = Random.Range(-plantRangeRadius, plantRangeRadius);

        return new Vector2(x, y);
    }
}
