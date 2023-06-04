using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Material m;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
       m = GetComponent<LineRenderer>().material;
        offset = Vector2.zero;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        m.mainTextureOffset = offset;
        offset += new Vector2(0.0005f, 0f);
    }

    IEnumerator Move()
    {
        yield return null;
    }
}
