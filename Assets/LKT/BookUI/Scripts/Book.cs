using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Book : MonoBehaviour
{
    public UIBook bookUI;
    public Animator animator;
    public UIBook flipUI;
    public Transform pageTabs;
    int pageNow;

    XRGrabInteractable xrgi;

    // Start is called before the first frame update
    void Start()
    {
        bookUI.gameObject.SetActive(false);
        xrgi = GetComponent<XRGrabInteractable>();
    }

    public void Open()
    {
        animator.SetTrigger("open");
        bookUI.FlipPage(pageNow);

        if (AreaTrigger.CheckPlayerInAreaByID("Buildable", bookUI.player))
            bookUI.player.GetComponent<FlyMode>().EnterFlyMode();

        bookUI.bookOpened = true;

        Invoke("BookUIOn", 0.8f);
    }

    public void Flip(int n)
    {
        pageNow = n;
        animator.SetTrigger("flip");
        if (bookUI.placeTurret.isPreviewing)
        {
            bookUI.placeTurret.DeletePreview();
            bookUI.SelectExitAllButton();
        }

        StartCoroutine(Flip(0.2f, n, 0.25f, 0.15f));
    }

    public void Close()
    {
        animator.SetTrigger("close");
        Invoke("BookUIOff", 0.8f);
        Invoke("GoBackToParentZero", 0.1f);

        bookUI.bookOpened = false;

        Debug.Log("exit fly: book close");
        if (!bookUI.placeTurret.isPreviewing)
            bookUI.player.GetComponent<FlyMode>().ExitFlyMode();
    }

    void BookUIOff()
    {
        bookUI.gameObject.SetActive(false);
        pageTabs.gameObject.SetActive(false);
    }

    void BookUIOn()
    {
        bookUI.gameObject.SetActive(true);
        Invoke("PageTabsOn", 0.5f);
    }

    void PageTabsOn()
    {
        pageTabs.gameObject.SetActive(true);
    }

    IEnumerator Flip(float delay1, int pageNumber, float delay2, float delay3)
    {
        flipUI.FlipPage(bookUI.pageNow);
        flipUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay1);
        BookUIOff();
        bookUI.FlipPage(pageNumber);

        yield return new WaitForSeconds(delay2);
        BookUIOn();

        yield return new WaitForSeconds(delay3);
        flipUI.gameObject.SetActive(false);
    }

    void GoBackToParentZero()
    {
        StartCoroutine(GoBackToParentZero(0.8f));
    }

    IEnumerator GoBackToParentZero(float duration)
    {
        float time = 0f;

        while (time < 1)
        {
            if (xrgi.isSelected)
                yield break;

            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, time);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, time);

            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}
