using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScaner : MonoBehaviour
{
    [SerializeField] private float _scanDistance = 50f; 
    [field: SerializeField] public float Cooldown { get; private set; } = 20f;

    private bool _canScan = true;

    private ResourcesStorage _resourcesStorage;

    private void Start()
    {
        _resourcesStorage = Starter.Instance.ResourcesStorage;
    }

    public List<Resources> Scan()
    {
        if (_canScan == false)
            return null;

        StartCoroutine(ColldownProcess());

        List<Resources> resourcesList = new List<Resources>();

        foreach (Resources resource in _resourcesStorage.ResourcesList)
            if ((resource.transform.position - transform.position).sqrMagnitude < _scanDistance * _scanDistance)
                resourcesList.Add(resource);

        return resourcesList;
    }

    private IEnumerator ColldownProcess()
    {
        _canScan = true;

        yield return new WaitForSeconds(Cooldown);

        _canScan = true;
    }
}
