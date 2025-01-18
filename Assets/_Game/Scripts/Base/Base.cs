using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ResourcesScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private ResourcesScanner _resourcesScaner;
    [SerializeField] private UnitSpawner _unitSpawner;

    private List<Unit> _units = new List<Unit>();

    private List<Resources> _knownResources = new List<Resources>();

    private int _resourcesValue = 0;

    public event Action<int> ResourcesValueChanged;

    public void Init()
    {
        AddListeners();
        _resourcesScaner.StartScanning();
    }

    public void PutResources(Resources resources)
    {
        if (resources.Value < 0)
            throw new ArgumentOutOfRangeException();

        _resourcesValue += resources.Value;
        ResourcesValueChanged?.Invoke(_resourcesValue);
    }

    private void AddListeners()
    {
        _resourcesScaner.Scanned += OnScanned;
        _unitSpawner.ObjectSpawned += OnUnitSpawned;
    }

    private void RemoveListeners()
    {
        _resourcesScaner.Scanned -= OnScanned;
        _unitSpawner.ObjectSpawned -= OnUnitSpawned;

        foreach (var unit in _units)
        {
            unit.Released -= OnUnitReleased;
        }
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnUnitSpawned(Unit unit)
    {
        _units.Add(unit);
        unit.Released += OnUnitReleased;
    }

    private void OnUnitReleased(Unit unit)
    {
        StartCoroutine(CollectAvailableResources(unit));
    }

    private IEnumerator CollectAvailableResources(Unit unit)
    {
        yield return new WaitUntil(() => _knownResources.Count(resources => resources.IsAvailable) > 0);
        
        Resources collectingResources = _knownResources.FirstOrDefault(resources => resources.IsAvailable);
        unit.Collect(collectingResources);
    }

    private void OnScanned(List<Resources> scannedResources)
    {
        if (scannedResources.Count == 0)
            return;

        _knownResources.AddRange(scannedResources
            .Where(resources => _knownResources.Contains(resources) == false)
            .ToList());
    }
}