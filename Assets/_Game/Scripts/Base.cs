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

    private int _resourcesValue = 0;

    //public event Action<IReadOnlyList<Resources>> ResourcesFinded;

    public event Action<int> ResourcesValueChanged;

    public IReadOnlyList<Resources> KnownResources => _knownResources;

    private void Start()
    {
        StartCoroutine(Scanning());
    }

    public void PutResources(Resources resources)
    {
        if (resources.Value < 0)
            throw new ArgumentOutOfRangeException();

        _resourcesValue += resources.Value;
        ResourcesValueChanged?.Invoke(_resourcesValue);
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
        if (_resourcesScaner.TryGetResources(out List<Resources> listResources) == false)
            return;

        _knownResources.AddRange(listResources
            .Where(resources => _knownResources.Contains(resources) == false)
            .ToList());

        //ResourcesFinded?.Invoke(_knownResources);
    }
}