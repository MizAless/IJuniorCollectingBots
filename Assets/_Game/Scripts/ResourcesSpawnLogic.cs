using System.Collections;
using UnityEngine;

public class ResourcesSpawnLogic : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private int _maxResourcesCount = 3;
    [SerializeField] private float _spawnCooldown = 3f;

    private int _resourcesCount = 0;

    private bool CanSpawn => _resourcesCount < _maxResourcesCount;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSeconds(_spawnCooldown);

        while (enabled)
        {
            if (_resourcesCount >= _maxResourcesCount)
                yield return new WaitUntil(() => CanSpawn);

            Resources resources = _resourcesSpawner.Spawn();
            resources.Grabbed += OnGrabbed;
            _resourcesCount++;
            yield return wait;
        }
    }

    private void OnGrabbed(Resources resources)
    {
        resources.Grabbed -= OnGrabbed;
        _resourcesCount--;
    }
}
