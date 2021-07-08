using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorFactory
{
    private Dictionary<ResourceType, int> resources;
    public CollectorFactory(Dictionary<ResourceType, int> resources)
    {
        this.resources = resources;
    }
    public IResourceCollector GetCollector(ResourceType type)
    {
        switch(type)
        {
            case ResourceType.ResourceA:
                return new ResourceCollectorA(resources);
            case ResourceType.ResourceB:
                return new ResourceCollectorB(resources);

            default: Debug.LogWarning("Null GetCollector Returned"); return null;
        }
    }
}
