using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourcesScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private ResourcesScanner _resourcesScaner;
    
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private int _unitCost = 3;

    private List<Resources> _knownResources = new List<Resources>();

    private int _resourcesValue = 9;

    public event Action<int> ResourcesValueChanged;

    public IReadOnlyList<Resources> KnownResources => _knownResources;

    private void Start()
    {
        StartCoroutine(Scanning());
        StartCoroutine(Spawning());
    }

    public void PutResources(Resources resources)
    {
        if (resources.Value < 0)
            throw new ArgumentOutOfRangeException();

        _resourcesValue += resources.Value;
        ResourcesValueChanged?.Invoke(_resourcesValue);
    }
    
    private IEnumerator Spawning()
    {
        while (enabled)
        {
            TrySpawn();

            yield return new WaitForSeconds(_unitSpawner.Cooldown);
        }
    }
    
    private void TrySpawn()
    {
        if (_resourcesValue < _unitCost) 
            return;
        
        _resourcesValue -= _unitCost;
        ResourcesValueChanged?.Invoke(_resourcesValue);
        _unitSpawner.Spawn().Init(this);
    }

    private IEnumerator Scanning()
    {
        while (enabled)
        {
            Scan();

            yield return new WaitForSeconds(_resourcesScaner.Cooldown);
        }
    }

    private void Scan()
    {
        if (_resourcesScaner.TryScanResources(out List<Resources> listResources) == false)
            return;
        
        _knownResources.AddRange(listResources
            .Where(resources => _knownResources.Contains(resources) == false)
            .ToList());
    }
}

public class BaseState : IState
{
    public void Handle(Unit unit) //nu pzdc
    {
        throw new NotImplementedException();
    }
}