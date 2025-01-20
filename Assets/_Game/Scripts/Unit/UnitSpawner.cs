using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitSpawner : Spawner<Unit>
{
    [field: SerializeField] public float Cooldown = 1f;
    // [SerializeField] private int _maxUnits = 1;
    // [SerializeField] private Base _base;
    
    private bool _canSpawn = true;

    private int _currentCount = 0;

    public void Init()
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