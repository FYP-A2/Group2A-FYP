using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class RunTimeBake : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = transform.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
}
