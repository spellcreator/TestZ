using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryA
{
    private Inventory inventory;
    private FactoryAView aView;


    public FactoryA(Inventory inventory, FactoryAView aView)
    {
        this.inventory = inventory;
        this.aView = aView;
        this.aView.ButtonClick += Work;
    }

    public void Work()
    {
        inventory.AddResource(ResourceType.ResourceA);
        Debug.Log("Add Resource A");
    }
}
