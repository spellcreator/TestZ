using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryB
{
    private Inventory inventory;
    private FactoryBView bView;


    public FactoryB(Inventory inventory, FactoryBView bView)
    {
        this.inventory = inventory;
        this.bView = bView;
        this.bView.ButtonClick += Work;
    }

    public void Work()
    {
        inventory.AddResource(ResourceType.ResourceB);
        Debug.Log("Add Resource B");
    }
}
