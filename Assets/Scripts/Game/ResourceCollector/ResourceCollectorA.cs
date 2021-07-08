using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectorA : IResourceCollector
{
    private Dictionary<ResourceType, int> resources;
    public ResourceCollectorA(Dictionary<ResourceType, int> resources)
    {
        this.resources = resources;
    }

    public void Collect(int count = 1)
    {
        resources[ResourceType.ResourceA] += count;
    }
}
