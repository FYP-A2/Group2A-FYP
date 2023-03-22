using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceDisplay : MonoBehaviour
{
    public ResourceGroupType playerResource;
    public ResourceGroupTypeSO compareResource;

    public enum GetByWhat { NAME, INDEX}
    public GetByWhat getByWhat = GetByWhat.NAME;

    public string resourceName;
    public int resourceIndex = 0;

    [Header("display")]
    public Text textAmount;
    public Image imageIcon;
    public Color colorNormal;
    public Color colorAbnormal;


    private void Start()
    {
        if (playerResource== null)
        {
            playerResource = GameObject.FindObjectOfType<ResourceGroupType>();
        }

        textAmount.color = colorNormal;
    }
    private void Update()
    {
        if (compareResource == null)
        {
            if (getByWhat == GetByWhat.NAME)
            {
                textAmount.text = playerResource.GetAmount(resourceName).ToString();
                imageIcon.sprite = playerResource.FindResourceByName(resourceName).icon;
            }
            else
            {
                textAmount.text = playerResource.GetAmount(resourceIndex).ToString();
                imageIcon.sprite = playerResource.resources[resourceIndex].icon;
            }
        }
        else
        {
            textAmount.text = compareResource.resources[resourceIndex].amount.ToString();
            imageIcon.sprite = compareResource.resources[resourceIndex].icon;

            if (compareResource.resources[resourceIndex].amount > playerResource.GetAmount(resourceIndex))
                textAmount.color = colorAbnormal;
            else
                textAmount.color = colorNormal;
        }

    }
}
