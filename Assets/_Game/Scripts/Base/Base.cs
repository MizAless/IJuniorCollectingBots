using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourcesScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private ResourcesScanner _resourcesScaner;

    private List<Resources> _knownResources = new List<Resources>();

    private List<Unit> _units = new List<Unit>();

    private int _resourcesValue = 0;

    public event Action<int> ResourcesValueChanged;

    public IReadOnlyList<Resources> KnownResources => _knownResources;

    private void Start()
    {
        _resourcesScaner.StartScanning();
    }

    private void OnEnable()
    {
        _resourcesScaner.Scanned += OnScanned;
    }

    private void OnDisable()
    {
        _resourcesScaner.Scanned -= OnScanned;
    }

    public void PutResources(Resources resources)
    {
        if (resources.Value < 0)
            throw new ArgumentOutOfRangeException();

        _resourcesValue += resources.Value;
        ResourcesValueChanged?.Invoke(_resourcesValue);
    }
    
    private IEnumerator ResourcesCollecting()
    {
        while (enabled)
        {
            
            
            yield return null;
        }
    }
    
    private void Collect()
    {
        //продолжить
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