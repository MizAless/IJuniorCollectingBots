using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitSpawner : Spawner<Unit>
{
    [field: SerializeField] public float Cooldown = 1f;
    // [SerializeField] private int _maxUnits = 1;
    // [SerializeField] private Base _base;
    
    private bool _canSpawn = true;

    // private int _currentCount = 0;
    //
    // private void Start()
    // {
    //     StartCoroutine(Spawning());
    // }
    //
    // private IEnumerator Spawning()
    // {
    //     var wait = new WaitForSeconds(_spawnCooldown);
    //
    //     while (enabled)
    //     {
    //         if (_currentCount < _maxUnits)
    //         {
    //             Unit spawnedUnit = base.Spawn();
    //             spawnedUnit.Init(_base);
    //             _currentCount++;
    //         }
    //
    //         yield return wait;
    //     }
    // }
    
    private IEnumerator CooldownProcess()
    {
        _canSpawn = false;

        yield return new WaitForSeconds(Cooldown);
        
        _canSpawn = true;
    }

    public override Unit Spawn()
    {
        if (_canSpawn == false)
            return null;

        StartCoroutine(CooldownProcess());
        
        return base.Spawn();
    }
}