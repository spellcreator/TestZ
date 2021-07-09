using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryView : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI resourceA;
    [SerializeField]
    private TMPro.TextMeshProUGUI resourceB;
    [SerializeField]
    private Button reset;
    [SerializeField]
    private FactoryAView factoryA;
    [SerializeField]
    private FactoryBView factoryB;
   
    public Inventory inventory;


   private void Start()
    {
        SetTextAB();
        reset.onClick.AddListener(ResetCounter);
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
        SetTextA();
        SetTextB();
        
    }

    private void ResetCounter()
    {
        inventory.ResetCounter();
        SetTextAB();
    }

}
