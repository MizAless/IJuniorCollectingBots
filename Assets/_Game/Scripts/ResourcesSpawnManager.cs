using System.Collections;
using UnityEngine;

public class ResourcesSpawnManager : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private int _maxResourcesCount = 3;
    [SerializeField] private float _spawnCooldown = 3f;

    private float _checkSpawnDelay = 0.2f;

    private int _currentResourcesCount = 0;

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            if (_currentResourcesCount < _maxResourcesCount)
            {
                Resources resources = _resourcesSpawner.Spawn();
                resources.Disabled += DecrementResourcesCount;
                _currentResourcesCount++;
                yield return new WaitForSeconds(_spawnCooldown);
            }
            else
            {
                yield return new WaitForSeconds(_checkSpawnDelay);
            }
        }
    }

    private void DecrementResourcesCount(IDestroyable resources)
    {
        _currentResourcesCount--;
    }

    //private IEnumerator Spawning()
    //{
    //    while (enabled)
    //    {
    //        if (_currentResourcesCount < _maxResourcesCount)
    //        {
    //            _resourcesSpawner.Spawn();
    //            _currentResourcesCount++;
    //        }

    //        yield return new WaitForSeconds(_spawnDelay);
    //    }
    //}
}
