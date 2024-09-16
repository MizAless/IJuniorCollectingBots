using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private float _spawnCooldown = 1f;
    [SerializeField] private int _maxUnits = 1;

    private int _currentCount = 0;

    private void Start()
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
                base.Spawn();
                _currentCount++;
            }

            yield return wait;
        }
    }
}