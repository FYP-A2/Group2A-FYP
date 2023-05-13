using UnityEngine;

public class Shop : MonoBehaviour
{
    ResourceGroupType resourceGroup;
    void Start()
    {
        resourceGroup = ResourceGroupType.Instance;
    }

    public void BuyWood(int x)
    {
        if(resourceGroup.GetAmount(2)>= x * 2)
        {
            resourceGroup.Reduce(2, x * 2);
            resourceGroup.Add(0, x);
        }
        else
        {
            return;
        }
    }

    public void BuyStone(int x)
    {
        if (resourceGroup.GetAmount(2) >= x * 4)
        {
            resourceGroup.Reduce(2, x * 4);
            resourceGroup.Add(1, x);
        }
        else
        {
            return;
        }
    }
}
