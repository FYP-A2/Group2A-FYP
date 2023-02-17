using Unity.AI.Navigation;
using UnityEngine;

public class RunTimeNavMesh : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface= GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
}
