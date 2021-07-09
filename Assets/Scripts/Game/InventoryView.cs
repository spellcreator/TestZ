using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryView : MonoBehaviour
{
    public TMPro.TextMeshProUGUI resourceA;
    public TMPro.TextMeshProUGUI resourceB;

    public FactoryAView factoryA;
    public FactoryBView factoryB;

    public Inventory inventory;


   private void Start()
    {
        SetTextAB();
    }

    public void SetTextA()
    {
        resourceA.DOFade(0, 0.5f).OnComplete(() => { resourceA.text = inventory.Resources[ResourceType.ResourceA].ToString(); resourceA.DOFade(1, 0.5f); });
    }
    public void SetTextB()
    {
        resourceB.DOFade(0, 0.5f).OnComplete(() => { resourceB.text = inventory.Resources[ResourceType.ResourceB].ToString(); resourceB.DOFade(1, 0.5f); });
    }

    public void SetTextAB()
    {
        resourceA.DOFade(0, 0.5f).OnComplete(() => { resourceA.text = inventory.Resources[ResourceType.ResourceA].ToString(); resourceA.DOFade(1, 0.5f); });
        resourceB.DOFade(0, 0.5f).OnComplete(() => { resourceB.text = inventory.Resources[ResourceType.ResourceB].ToString(); resourceB.DOFade(1, 0.5f); });
    }

}
