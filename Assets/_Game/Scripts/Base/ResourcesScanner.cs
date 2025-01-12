using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScanner : MonoBehaviour
{
    [SerializeField] private float _scanDistance = 50f; 

    [SerializeField] private ResourcesStorage _resourcesStorage;
    [field: SerializeField] public float Cooldown { get; private set; } = 20f;

    private bool _canScan = true;

    public bool TryScanResources(out List<Resources> resourcesList)
    {
        if (_canScan == false || _resourcesStorage.HasResources == false)
        {
            resourcesList = null;
            return false;
        }

        StartCoroutine(CooldownProcess());

        resourcesList = new List<Resources>();

        foreach (Resources resource in _resourcesStorage.ResourcesList)
            if ((resource.transform.position - transform.position).sqrMagnitude < _scanDistance * _scanDistance)
                resourcesList.Add(resource);

        return true;
    }

    private IEnumerator CooldownProcess()
    {
        _canScan = false;

        yield return new WaitForSeconds(Cooldown);

        _canScan = true;
    }
}
