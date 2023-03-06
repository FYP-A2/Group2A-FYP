using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceDisplay : MonoBehaviour
{
    public ResourceGroupType playerResource;
    public enum GetByWhat { NAME, INDEX}
    public GetByWhat getByWhat = GetByWhat.NAME;

    public Text textAmount;
    public Image imageIcon;

    public string resourceName;
    public int resourceIndex = 0;

    private void Start()
    {
        if (playerResource== null)
        {
            playerResource = GameObject.FindObjectOfType<ResourceGroupType>();
        }
    }
    private void Update()
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
}
