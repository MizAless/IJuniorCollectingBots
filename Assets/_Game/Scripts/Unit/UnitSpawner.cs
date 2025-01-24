using System.Collections;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [field: SerializeField] public float Cooldown = 1f;
    
    private bool _canSpawn = true;

    private int _currentCount = 0;

    public IEnumerator CooldownProcess()
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