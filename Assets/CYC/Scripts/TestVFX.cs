using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteAlways]
public class TestVFX : MonoBehaviour
{
    public VisualEffect VFX;
    GameObject start,end;
    float i = 0;
    // Start is called before the first frame update
    //void Start()
    //{
    //    if (Application.IsPlaying(gameObject))
    //    {
    //        // Play logic
    //    }
    //    else
    //    {
    //        // Editor logic
    //        Destroy(gameObject);
    //        //DestroyImmediate(gameObject);
    //    }
    //    //VFX= GetComponent<VisualEffect>();
    //    //VFX.SetVector3("Pos1", Vector3.zero);
    //    //VFX.SetVector3("Pos2", new Vector3(10,0,10));
    //    //VFX.SetVector3("Pos3", new Vector3(10, 0, 20));
    //    //VFX.SetVector3("Pos4", new Vector3(10, 0, 30));
    //}

    private void Update()
    {
        i += Time.deltaTime;
        if(i > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetPos(GameObject startPos, GameObject endPos)
    {
       start = startPos;
       end = endPos;
       VFX.SetVector3("Pos1", start.transform.position);
       VFX.SetVector3("Pos4", end.transform.position);
    }
}