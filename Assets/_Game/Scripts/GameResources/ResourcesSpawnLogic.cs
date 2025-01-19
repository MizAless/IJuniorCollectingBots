using System.Collections;
using UnityEngine;

public class ResourcesSpawnLogic : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private int _maxResourcesCount = 3;
    [SerializeField] private float _spawnCooldown = 3f;

    private int _resourcesCount = 0;

    private bool CanSpawn => _resourcesCount < _maxResourcesCount;

    public void Init()
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
            resources.Disabled += OnDisabled;
            _resourcesCount++;
            
            yield return wait;
        }
    }

    private void OnDisabled(Resources resources)
    {
        resources.Disabled -= OnDisabled;
        _resourcesCount--;
    }
}
