using System.Collections.Generic;
using UnityEngine;

public class ResourcesStorage : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;

    private List<Resources> _resourcesList = new List<Resources>();

    public IReadOnlyList<Resources> ResourcesList => _resourcesList;
    
    public bool HasResources => _resourcesList.Count > 0;
    
    private void OnEnable()
    {
        _resourcesSpawner.ObjectSpawned += Add;
        _resourcesSpawner.ObjectDisabled += Remove;
    }

    private void OnDisable()
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
