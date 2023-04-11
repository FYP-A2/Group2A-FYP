using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceGroupType;

[CreateAssetMenu(fileName = "DefaultResources", menuName = "ResourceGroupTypeSO")]
public class ResourceGroupTypeSO : ScriptableObject
{
    public List<Resource> resources;
}