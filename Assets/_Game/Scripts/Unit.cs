using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ResourcesCollector))]
[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour, IDestroyable
{
    private ResourcesCollector _resourcesCollector;
    private Mover _mover;

    private IState state = new IdleState();

    private Resources _targetResources;

    public event Action<IDestroyable> Disabled;
    public event Action<IDestroyable> Destroyed;

    private void OnEnable()
    {
        _mover.Reached += Collect;
    }

    private void OnDisable()
    {
        _mover.Reached -= Collect;
    }

    private void Awake()
    {
        _resourcesCollector = GetComponent<ResourcesCollector>();
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        Invoke(nameof(Move), 0.01f);

        state.Handle(this);
    }

    private void Move()
    {
        var res = FindAnyObjectByType(typeof(Resources));

        _targetResources = res as Resources;

        _mover.Move(_targetResources.transform);
    }
     
    private void Collect()
    {
        _resourcesCollector.Collect(_targetResources);
    }
}

public class IdleState : IState
{
    private float _delay = 0.5f;

    public void Handle(Unit unit)
    {
        
    }

    private IEnumerator WaitForNewResources()
    {
        yield return new WaitForSeconds(_delay);
    }
}
public class MoveingToResourcesState : IState
{
    public void Handle(Unit unit)
    {

    }
}

public class CollectResourcesState : IState
{
    public void Handle(Unit unit)
    {

    }
}

public class MoveingToBaseState : IState
{
    public void Handle(Unit unit)
    {
        
    }
}