using System.Collections.Generic;

public class ResourcesStorage
{
    private List<Resources> _resourcesList = new List<Resources>();

    private ResourcesSpawner _resourcesSpawner;

    public ResourcesStorage(ResourcesSpawner resourcesSpawner)
    {
        _resourcesSpawner = resourcesSpawner;

        _resourcesSpawner.Spawned += Add;
    }

    public List<Resources> ResourcesList
    {
        get
        {
            List<Resources> resourcesList = new List<Resources>();

            foreach (var resources in _resourcesList)
            {
                resourcesList.Add(resources);
            }

            return resourcesList;
        }
    }

    private void Add(Resources resources)
    {
        _resourcesList.Add(resources);
        resources.Disabled += Remove;
    }

    private void Remove(IDestroyable resources)
    {
        if (resources is Resources)
            _resourcesList.Remove(resources as Resources);
    }
}
