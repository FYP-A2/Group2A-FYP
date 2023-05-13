using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapHpBar : MonoBehaviour
{
    public Transform hpDisplayObject;
    MonoBehaviour[] components;
    IHP hpInterface;

    [Header("UI")]
    public Slider sliderHPBar;
    public Image imageFill;
    public Image imageFillBackground;
    public Image imageIcon;
    public Text textAmount;

    [Header("Set")]
    public Color fillColor = Color.red;
    public Color backgroundColor = Color.white;
    [Range(0, 4)]
    public int baseScale = 0;
    public int groupChannel = 0;
    public int amount { get; private set; }

    [Header("private")]
    int scale = -1;
    int maxScaleLevel = 4;
    int[] sizeGraduation = new int[] { 45, 60, 75, 90, 105 }; //determine icon size by scale level 0 to 4
    int[] amountGraduation = new int[] { 1, 2, 6, 14, 30 }; //determine scale level 0 to 4 by amount

    bool tooCloseToOtherUI = false;
    bool display = true;

    bool groupMode = false;
    public bool groupModeIsLeader = false;
    UIMapHpBar groupLeader;
    public List<UIMapHpBar> groupMembers;

    private void Start()
    {
        FindAllComponent();
        FindHpInObject();
        SetAmount(1);
        StartCoroutine(UpdatePositionCoroutine());

        imageFill.color = fillColor;
        imageFillBackground.color = backgroundColor;

        transform.SetParent(null);
        transform.localEulerAngles = new Vector3(90, 0, 0);
        transform.position = new Vector3(hpDisplayObject.position.x, 512, hpDisplayObject.position.z);
    }

    private void Update()
    {
        UpdateHP();
        UpdateDisplayScale();
    }

    private void LateUpdate()
    {
        tooCloseToOtherUI = false;
    }

    void FindAllComponent()
    {
        components = hpDisplayObject.GetComponents<MonoBehaviour>();
    }

    void FindHpInObject()
    {
        foreach (var component in components)
        {
            if (component is IHP)
            {
                hpInterface = (IHP)component;
                break;
            }
        }
    }

    void UpdateHP()
    {
        float maxHP = 1;
        float nowHP = 0;

        if (!groupMode || groupModeIsLeader)
        {

            if (hpInterface != null)
            {
                hpInterface.GetHP(out maxHP, out nowHP);
            }
        }
        if (groupModeIsLeader)
        {
            foreach (UIMapHpBar a in groupMembers)
            {
                if (a.hpInterface != null)
                {
                    maxHP += a.hpInterface.GetMaxHP();
                    nowHP += a.hpInterface.GetHP();
                }
            }
        }

        sliderHPBar.maxValue = maxHP;
        sliderHPBar.value = nowHP;
    }

    void UpdateDisplayScale()
    {
        transform.localScale = new Vector3(UIMap.scale, UIMap.scale, UIMap.scale);
    }

    IEnumerator UpdatePositionCoroutine()
    {
        while (true)
        {
            while (tooCloseToOtherUI)
            {
                yield return new WaitForSeconds(0.2f);
                yield return new WaitForFixedUpdate();
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForFixedUpdate();

            if (hpDisplayObject == null)
            {
                Destroy(gameObject);
            }
            else

                //try
                //{
                UpdatePosition();
            //}
            //catch (MissingReferenceException e)
            //{
            //   Destroy(gameObject);
            //}
            //catch (NullReferenceException e)
            //{
            //   throw e;
            //}

            yield return new WaitForSeconds(0.03f);
        }
    }

    void UpdatePosition()
    {
        if (!groupMode)
        {
            Vector3 targetPos = new Vector3(hpDisplayObject.position.x, 512, hpDisplayObject.position.z);
            Vector3 pos = transform.position + (targetPos - transform.position) / 10;

            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else if (groupModeIsLeader)
        {
            Vector3 avgPos = new Vector3(hpDisplayObject.position.x, 512, hpDisplayObject.position.z);
            for (int i = 0; i < groupMembers.Count; i++)
                if (groupMembers[i] != null && groupMembers[i].hpDisplayObject != null)
                    avgPos += groupMembers[i].hpDisplayObject.position;
            avgPos /= groupMembers.Count + 1;
            avgPos.y = 512;

            Vector3 pos = transform.position + (avgPos - transform.position) / 10;
            GetComponent<Rigidbody>().MovePosition(pos);
        }

    }

    void UpdateIconAndTextSize()
    {
        int size = sizeGraduation[scale];

        imageIcon.rectTransform.sizeDelta = new Vector2(size, size);
        textAmount.rectTransform.sizeDelta = new Vector2(size, size);
        if (textAmount.gameObject.activeSelf && amount == 1)
            textAmount.gameObject.SetActive(false);
        else if (!textAmount.gameObject.activeSelf && amount != 1)
            textAmount.gameObject.SetActive(true);
    }

    void UpdateText()
    {
        textAmount.text = amount.ToString();
    }

    void UpdateScale()
    {
        int temp = GetScale(amount);
        if (temp != scale)
        {
            scale = temp;
            UpdateIconAndTextSize();
        }
    }

    int GetScale(int amount)
    {
        int result = baseScale;

        if (amount >= amountGraduation[amountGraduation.Length - 1])
            result += amountGraduation.Length - 1;
        else for (int i = 0; i < amountGraduation.Length - 1; i++)
            {
                if (amount >= amountGraduation[i] && amount < amountGraduation[i + 1])
                    result += i;
            }

        if (result > maxScaleLevel)
            result = maxScaleLevel;

        return result;
    }

    public void SetAmount(int n)
    {
        amount = n;
        UpdateScale();
        UpdateText();
    }

    public void AddAmount(int n)
    {
        amount += n;
        UpdateScale();
        UpdateText();
    }

    void Display(bool condition)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(condition);
        }
        display = condition;

    }

    void CreateGroup()
    {
        groupMode = true;
        groupModeIsLeader = true;
        groupLeader = this;
        groupMembers = new List<UIMapHpBar>();
        Display(true);
    }

    void JoinGroup(UIMapHpBar leader)
    {
        if (this != leader)
        {
            groupMode = true;
            groupModeIsLeader = false;
            groupLeader = leader;
            groupLeader.groupMembers.Add(this);
            groupLeader.AddAmount(1);
            Display(false);
        }
    }

    void LeaveGroup(bool turnOnDisplay = true)
    {
        groupMode = false;
        groupModeIsLeader = false;
        if (groupLeader != null)
        {
            groupLeader.groupMembers.Remove(this);
            groupLeader.AddAmount(-1);
            groupLeader = null;
        }
        if (turnOnDisplay)
            Display(true);
    }

    void FindNewLeader()
    {
        groupMembers[0].LeaveGroup();
        groupMembers[0].CreateGroup();
        NotifyNewGroup(groupMembers[0]);
    }

    void MergeGroup(UIMapHpBar other)
    {
        if (other != null)
        {
            other.NotifyNewGroup(this);
            other.JoinGroup(this);
        }
    }

    void NotifyNewGroup(UIMapHpBar newLeader)
    {
        UIMapHpBar[] hpBars = groupMembers.ToArray();
        for(int i = 0; i < hpBars.Length; i++ )
        {
            UIMapHpBar a = hpBars[i];
            if (a != newLeader)
            {
                a.LeaveGroup();
                a.JoinGroup(newLeader);
            }
        }
    }

    void NotifyDeleteGroup()
    {
        if (groupMembers == null) return;
        UIMapHpBar[] hpBars = groupMembers.ToArray();
        for (int i = 0; i < hpBars.Length; i++)
        {
            UIMapHpBar a = hpBars[i];
            if (a != null)
                a.LeaveGroup();
        }
    }

    private void OnDestroy()
    {
        if (groupMode)
        {
            if (groupModeIsLeader)
            {
                NotifyDeleteGroup();
            }
            else
            {
                LeaveGroup(false);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        tooCloseToOtherUI = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        UIMapHpBar a;
        if (!groupMode)
        {
            if (other.TryGetComponent<UIMapHpBar>(out a) && groupChannel == a.groupChannel && !a.groupMode)
            {
                CreateGroup();
            }
            else if (other.TryGetComponent<UIMapHpBar>(out a) && groupChannel == a.groupChannel && a.groupMode)
            {
                if (a.groupModeIsLeader)
                {
                    JoinGroup(a);
                }
                else
                {
                    JoinGroup(a.groupLeader);
                }
            }
        }
        else
        {
            if (other.TryGetComponent<UIMapHpBar>(out a) && groupChannel == a.groupChannel && a.groupMode && groupLeader != a.groupLeader)
            {
                if (groupMembers.Count >= a.groupLeader.groupMembers.Count)
                    MergeGroup(a);
                else
                    a.groupLeader.MergeGroup(this);
            }
        }
    }
}

// if leader include other
// do nothing

// if other is leader
// join group

// if 2 p not in group
// first p create group,be leader

// if 2 p are different leader
// merge group, notify member to change group



// if 2p same leader
// 
