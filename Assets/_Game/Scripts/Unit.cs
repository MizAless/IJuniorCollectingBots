using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour, IDestroyable<Unit>
{
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _giveDistance;

    private Interaction _grabPoint;

    private Mover _mover;
    private IState _state = new IdleState();

    public Base Base { get; private set; }

    public event Action<Unit> Grabbed;
    public event Action<Unit> Disabled;

    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _grabPoint = GetComponentInChildren<Interaction>();
    }

    public void Init(Base gameBase)
    {
        Base = gameBase;

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

    private IEnumerator GiveProcessing(Base gameBase)
    {
        while (CanGive(gameBase) == false)
        {
            _mover.Move(gameBase.transform);
            yield return null;
        }

        Resources givenResources = _grabPoint.Give(gameBase.transform.position);
        gameBase.PutResources(givenResources);
    }

    private IEnumerator GrabProcessing(Resources resources)
    {
        while (resources.IsActive)
        {
            if (CanGrab(resources))
            {
                _grabPoint.Grab(resources);
                Grabbed?.Invoke(this);
            }
            else
                yield return new WaitUntil(() => _mover.Move(resources.transform));
        }
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
    private float _delay = 0.5f;

    private Unit _unit;

    public void Handle(Unit unit)
    {
        _unit = unit;
    }

    private IEnumerator WaitForNewResources()
    {
        while()

        yield return new WaitForSeconds(_delay);

        _unit.Grab();
    }


}
public class MoveingToResourcesState : IState
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