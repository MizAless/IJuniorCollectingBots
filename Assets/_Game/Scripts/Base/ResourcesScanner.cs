using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScanner : MonoBehaviour
{
    [SerializeField] private float _scanDistance = 50f; 
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private float _cooldown = 20f;

    public event Action<List<Resources>> Scanned;

    public void StartScanning()
    {
        StartCoroutine(Scanning());
    }
    
    private IEnumerator Scanning()
    {
        var wait = new WaitForSeconds(_cooldown);
        
        while (enabled)
        {
            Scan();

            yield return wait;
        }
    }
    
    private void Scan()
    {
        if (_resourcesStorage.HasResources == false)
            return;

        var resourcesList = new List<Resources>();

        foreach (Resources resource in _resourcesStorage.ResourcesList)
            if ((resource.transform.position - transform.position).sqrMagnitude < _scanDistance * _scanDistance)
                resourcesList.Add(resource);

        Scanned?.Invoke(resourcesList);
    }
}
