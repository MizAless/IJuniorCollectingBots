using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ResourcesScaner _resourcesScaner;

    public List<Resources> KnownResources { get; private set; } = new List<Resources>();

    public event Action<List<Resources>> Scanned;

    private int resourcesValue = 0;

    private void Start()
    {
        StartCoroutine(Scaning());
    }

    public void PutResources(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();

        resourcesValue += value;
    }

    private IEnumerator Scaning()
    {
        while (enabled)
        {
            Scan();

            yield return new WaitForSeconds(_resourcesScaner.Cooldown);
        }
    }

    private void Scan()
    {
        KnownResources.AddRange
        (
            _resourcesScaner.
                Scan().
                Where(resources => KnownResources.Contains(resources) == false).
                ToList()
        );

        Scanned?.Invoke(KnownResources);
        print("Scanned");
    }
}