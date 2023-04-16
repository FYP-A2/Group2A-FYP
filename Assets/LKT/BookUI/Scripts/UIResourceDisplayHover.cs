using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static ResourceGroupType;

public class UIResourceDisplayHover : MonoBehaviour
{
    [HideInInspector]
    public UIBook book;
    [HideInInspector]
    public ResourceGroupType ownedResource;

    public List<UIResourceDisplay> UIRDSingles;
    bool activeState = false, move = false;
    float moveTime = 0, moveSpeed = 1.5f;
    float showSpeed = 3f;
    public RectTransform moveEndPos;
    public GameObject emptyPrefab;

    CanvasGroup canvasGroup;

    IEnumerator coroutine1;
    IEnumerator coroutine2;

    RectTransform DefaultParent { get => book.GetComponent<RectTransform>(); }


    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            coroutine1 = MoveAnimation();
            StartCoroutine(coroutine1);
            coroutine2 = ActiveAnimation();
            StartCoroutine(coroutine2);
        }
    }

    //on 
    //off
    public void DisplayOn(RectTransform rt, ResourceGroupTypeSO neededResource)
    {
        SetPosition(rt);
        SetResource(neededResource, ownedResource);
        UIActive(true);
    }
    public void DisplayOff()
    {
        UIActive(false);
    }



    void UIActive(bool condition)
    {
        activeState = condition;
        if (condition)
        {
            gameObject.SetActive(true);
        }
    }

    IEnumerator ActiveAnimation()
    {
        float activeAnimationProcess = 0;
        while (true)
        {
            if (activeState && activeAnimationProcess < 1)
            {
                if (activeAnimationProcess < 0)
                    activeAnimationProcess = 0;
                canvasGroup.alpha = activeAnimationProcess;
                activeAnimationProcess += Time.deltaTime * showSpeed;
            }
            else if (!activeState && activeAnimationProcess > 0)
            {
                if (activeAnimationProcess > 1)
                    activeAnimationProcess = 1;
                canvasGroup.alpha = activeAnimationProcess;
                activeAnimationProcess -= Time.deltaTime * showSpeed;
            }
            if (!activeState && gameObject.activeSelf && activeAnimationProcess <= 0)
            {
                gameObject.SetActive(false);
            }

            yield return null;

        }
    }

    //setpos
    void SetPosition(RectTransform rt)
    {
        moveEndPos = rt;
        move = true;
        moveTime = 0;
    }

    IEnumerator MoveAnimation()
    {
        while (true)
        {
            if (move && moveTime < 1)
            {
                moveTime += Time.deltaTime * moveSpeed;
                RectTransform rt = gameObject.GetComponent<RectTransform>();

                RectTransform rtTemp = Instantiate(emptyPrefab).GetComponent<RectTransform>();
                rtTemp.SetParent(rt);
                rtTemp.anchoredPosition3D = Vector3.zero;

                rtTemp.SetParent(moveEndPos);
                Vector3 temp = Vector3.Lerp(rtTemp.anchoredPosition, Vector3.zero, moveTime);
                rtTemp.anchoredPosition3D = new Vector3(temp.x, temp.y, -72);

                rtTemp.SetParent(DefaultParent);
                rt.anchoredPosition3D = rtTemp.anchoredPosition3D;
                rtTemp.SetParent(null);

                Destroy(rtTemp.gameObject);
            }
            if (moveTime >= 1)
            {
                move = false;
            }

            yield return null;
        }
    }

    //setRes
    void SetResource(ResourceGroupTypeSO neededResource, ResourceGroupType ownedResource)
    {
        DisableAllSingle();
        int n = 0;
        for (int i = 0; i < neededResource.resources.Count; i++)
        {
            if (neededResource.resources[i].amount != 0)
            {
                UIRDSingles[n].playerResource = ownedResource;
                UIRDSingles[n].compareResource = neededResource;
                UIRDSingles[n].resourceIndex = i;
                UIRDSingles[n].gameObject.SetActive(true);
                n++;
            }
        }
    }

    //disableAllSingle
    void DisableAllSingle()
    {
        foreach (UIResourceDisplay single in UIRDSingles)
        {
            single.gameObject.SetActive(false);
        }
    }

}
