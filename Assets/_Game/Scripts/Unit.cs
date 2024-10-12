using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour, IDestroyable<Unit>
{
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _giveDistance;

    private Interaction _grabPoint;

    private Mover _mover;
    private IState _state;

    public Base Base { get; private set; }

    public event Action<Unit> Grabbed;
    public event Action<Unit> Gave;
    public event Action<Unit> Disabled;

    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _grabPoint = GetComponentInChildren<Interaction>();
    }

    public void Init(Base gameBase)
    {
        Base = gameBase;

        SetState(new IdleState());
    }    

    public void SetState(IState state)
    {
        _state = state;
        _state.Handle(this);
    }

    public void Grab(Resources resources)
    {
        StartCoroutine(GrabProcessing(resources));
    }

    public void Give(Base gameBase)
    {
        StartCoroutine(GiveProcessing(gameBase));
    }

    private IEnumerator GrabProcessing(Resources resources)
    {
        while (CanGrab(resources) == false)
        {
            _mover.Move(resources.transform);
            yield return null;
        }

        _grabPoint.Grab(resources);
        Grabbed?.Invoke(this);
    }

    private IEnumerator GiveProcessing(Base gameBase)
    {
        while (CanGive(gameBase) == false)
        {
            _mover.Move(gameBase.transform);
            yield return null;
        }

        Resources givenResources = _grabPoint.Give(gameBase.transform.position);
        gameBase.PutResources(givenResources);
        Gave?.Invoke(this);
    }

    private bool CanGrab(Resources resources)
        => isEnoughDistance(resources.transform.position, transform.position, _grabDistance);
    private bool CanGive(Base gameBase)
        => isEnoughDistance(gameBase.transform.position, transform.position, _giveDistance);

    private bool isEnoughDistance(Vector3 target, Vector3 current, float closeDistance)
        => (current - target).sqrMagnitude <= closeDistance * closeDistance;
}

public class IdleState : IState
{
    private float _delay = 0.1f;

    public void Handle(Unit unit)
    {
        unit.StartCoroutine(WaitForNewResources(unit));
    }

    private IEnumerator WaitForNewResources(Unit unit)
    {
        while (unit.Base.KnownResources.Count(resources => resources.IsBusy == false) == 0)
        {
            yield return new WaitForSeconds(_delay);
        }

        unit.SetState(new MoveingToResourcesState());
    }
}

public class MoveingToResourcesState : IState
{
    public void Handle(Unit unit)
    {
        Resources grabbingResources = unit.Base.KnownResources
            .FirstOrDefault(resources => resources.IsBusy == false);

        if (grabbingResources == null)
        {
            unit.SetState(new IdleState());
            return;
        }

        grabbingResources.Privatize();

        unit.Grabbed += OnGrabbed;
        unit.Grab(grabbingResources);
    }

    private void OnGrabbed(Unit unit)
    {
        unit.Grabbed -= OnGrabbed;
        unit.SetState(new MoveingToBaseState());
    }
}

public class MoveingToBaseState : IState
{
    public void Handle(Unit unit)
    {
        unit.Gave += OnGave;

        unit.Give(unit.Base);
    }

    private void OnGave(Unit unit)
    {
        unit.Gave -= OnGave;

        unit.SetState(new IdleState());
    }
}