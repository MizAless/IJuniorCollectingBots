using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour, IDestroyable<Unit>
{
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _giveDistance;

    private Interaction _grabPoint;
    private UnitStateMachine _unitStateMachine;

    private Mover _mover;
    private IState _state;

    public event Action<Unit> Released;

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
        _unitStateMachine = new UnitStateMachine(this);
    }

    public void Collect(Resources resources)
    {
        _unitStateMachine.SetState(new MovingToResourcesState(_unitStateMachine, resources));
    }

    public void Grab(Resources resources)
    {
        StartCoroutine(GrabProcessing(resources));
    }

    public void Give(Base gameBase)
    {
        StartCoroutine(GiveProcessing(gameBase));
    }

    public void Release()
    {
        Released?.Invoke(this);
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
        => IsEnoughDistance(resources.transform.position, transform.position, _grabDistance);

    private bool CanGive(Base gameBase)
        => IsEnoughDistance(gameBase.transform.position, transform.position, _giveDistance);

    private bool IsEnoughDistance(Vector3 target, Vector3 current, float closeDistance)
        => (current - target).sqrMagnitude <= closeDistance * closeDistance;
}