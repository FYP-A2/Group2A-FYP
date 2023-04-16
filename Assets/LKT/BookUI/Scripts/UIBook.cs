using FYP2A.VR.PlaceTurret;
using MiscUtil.Xml.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UICommonButton;

public class UIBook : MonoBehaviour
{
    [Header("Player Info")]
    public Player player;
    public Camera playerCam;
    public ResourceGroupType PlayerResource { get => player.resourceGroup; }
    public PlaceTurret placeTurret;

    [Header("Book")]
    public List<GameObject> pages = new List<GameObject>();
    public int pageNow = 0;
    public List<UIResourceDisplay> uiResourceDisplays = new List<UIResourceDisplay>();
    public List<UICommonButton> uIButtons = new List<UICommonButton>();
    public UIResourceDisplayHover uiHover;
    public UIMap uiMap;
    public GameObject uiMapCamPrefab;
    public Camera uiMapCam;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (playerCam == null)
            playerCam = Camera.main;

        if (placeTurret == null)
            placeTurret = GameObject.FindGameObjectWithTag("Player").GetComponent<PlaceTurret>();




        if (uiResourceDisplays != null)
            foreach (var display in uiResourceDisplays)
                display.playerResource = PlayerResource;

        if (uIButtons != null)
            foreach (var button in uIButtons)
            {
                button.EventSelect += Select;
                button.book = this;
            }


        uiHover.book = this;
        uiHover.ownedResource = PlayerResource;


        if (uiMapCam == null)
        {
            uiMapCam = Instantiate(uiMapCamPrefab).GetComponent<Camera>();
        }

        uiMap.book = this;
        uiMap.cam = uiMapCam;
    }

    // Update is called once per frame
    void Update()
    {
        uiMapCam.transform.position = new Vector3(transform.position.x, 512, transform.position.z);
    }

    public void SelectExitAllButton()
    {
        if (uIButtons != null)
            foreach (var button in uIButtons)
                button.OnSelectExit();
    }

    public void Select(int n, SelectType t)
    {
        if (t == SelectType.tower)
            SelectTurret(n);
        else if (t == SelectType.pearl)
            SelectPearl(n);
    }

    public void SelectTurret(int n)
    {
        placeTurret.SetPreviewTurret(n);
    }

    public void SelectPearl(int n)
    {

    }

    public void ClearPage()
    {
        foreach (GameObject page in pages)
            page.SetActive(false);
    }

    public void FlipPage(int n)
    {
        ClearPage();
        pages[n].gameObject.SetActive(true);
        pageNow = n;
    }
}
