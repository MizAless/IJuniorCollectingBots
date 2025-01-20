using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private float _spawnCooldown = 1f;
    [SerializeField] private int _maxUnits = 1;
    [SerializeField] private Base _base;

    private int _currentCount = 0;

    public void Init()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSeconds(_spawnCooldown);

        while (enabled)
        {
            if (_currentCount < _maxUnits)
            {
                Unit spawnedUnit = base.Spawn();
                spawnedUnit.Init(_base);
                _currentCount++;
            }

            yield return wait;
        }
    }
}