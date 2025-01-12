using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : Spawner<Resources>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector2 _spawnPositionPossibleValue;

    private List<Resources> _availableResources = new List<Resources>();

    public bool HasResources => _availableResources.Count > 0;

    public IReadOnlyList<Resources> AvailableResources => _availableResources;

    public override Resources Spawn()
    {
        Resources resources = base.Spawn();

        Vector3 spawnPositionOffset = new Vector3(Random.Range(-1f,1f) * _spawnPositionPossibleValue.x, 0, Random.Range(-1f, 1f) * _spawnPositionPossibleValue.y);
        Vector3 spawnPosition = _spawnPoint.position + spawnPositionOffset;

        resources.Init(spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));

        _availableResources.Add(resources);

        resources.Grabbed += OnGrabbed;

        return resources;
    }

    private void OnGrabbed(Resources resources)
    {
        resources.Grabbed -= OnGrabbed;
        _availableResources.Remove(resources);
    }
}
