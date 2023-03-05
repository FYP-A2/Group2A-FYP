using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : MonoBehaviour
{
    List<UIResourceDisplay> uiResourceDisplays = new List<UIResourceDisplay>();

    public Player player;
    public ResourceGroupType PlayerResource { get => player.resourceGroup; }

    // Start is called before the first frame update
    void Start()
    {
        if (uiResourceDisplays != null)
            foreach (var display in uiResourceDisplays)
                display.playerResource = PlayerResource;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
