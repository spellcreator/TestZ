using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectorB : IResourceCollector
{
    private Dictionary<ResourceType, int> resources;
    public ResourceCollectorB(Dictionary<ResourceType, int> resources)
    {
        this.resources = resources;
    }

    public void Collect(int count = 1)
    {
        resources[ResourceType.ResourceB] += count * 3;
    }
}