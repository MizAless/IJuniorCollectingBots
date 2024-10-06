using System;
using System.Collections.Generic;

public class ResourcesStorage : IDisposable
{
    private List<Resources> _resourcesList = new List<Resources>();

    private ResourcesSpawner _resourcesSpawner;

    public ResourcesStorage(ResourcesSpawner resourcesSpawner)
    {
        _resourcesSpawner = resourcesSpawner;

        _resourcesSpawner.ObjectSpawned += Add;
        _resourcesSpawner.ObjectDisabled += Remove;
    }

    public IReadOnlyList<Resources> ResourcesList => _resourcesList;

    public void Dispose()
    {
        _resourcesSpawner.ObjectSpawned -= Add;
        _resourcesSpawner.ObjectDisabled -= Remove;
    }

    private void Add(Resources resources)
    {
        _resourcesList.Add(resources);
    }

    private void Remove(Resources resources)
    {
        _resourcesList.Remove(resources);
    }
}
