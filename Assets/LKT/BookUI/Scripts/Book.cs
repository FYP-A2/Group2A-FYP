using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Book : MonoBehaviour
{

    public UIBook bookUI;
    public Animator animator;
    public UIBook flipUI;
    public Transform pageTabs;

    // Start is called before the first frame update
    void Start()
    {
        bookUI.gameObject.SetActive(false);
    }

    public void Open()
    {
        animator.SetTrigger("open");
        bookUI.FlipPage(0);

        Invoke("BookUIOn", 0.8f);
    }

    public void Flip(int n)
    {
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

}
