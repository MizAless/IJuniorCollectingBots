using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour, IDestroyable<Unit>
{
    [SerializeField] private float _grabDistance;
    [SerializeField] private float _giveDistance;
    [SerializeField] private float _buildDistance;

    private Interaction _grabPoint;
    private UnitStateMachine _unitStateMachine;

    private Mover _mover;
    private IState _state;

    public event Action<Unit> Released;

    public Base Base { get; private set; }

    public event Action<Unit> Grabbed;
    public event Action<Unit> Gave;
    public event Action<Unit> Builded;
    public event Action<Unit> Disabled;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _grabPoint = GetComponentInChildren<Interaction>();
    }

    public void Init(Base gameBase)
    {
        SetBase(gameBase);
        _unitStateMachine = new UnitStateMachine(this);
    }
    
    private void SetBase(Base gameBase)
    {
        Base = gameBase;
    }

    public void Collect(Resources resources)
    {
        _unitStateMachine.SetState(new MovingToResourcesState(_unitStateMachine, resources));
    }
    
    public void SendBuild(Vector3 position)
    {
        _unitStateMachine.SetState(new MovingToBuildBase(_unitStateMachine, position));
    }

    public void Grab(Resources resources)
    {
        StartCoroutine(GrabProcessing(resources));
    }

    public void Give(Base gameBase)
    {
        StartCoroutine(GiveProcessing(gameBase));
    }
    
    public void BuildBase(Vector3 position)
    {
        StartCoroutine(BuildProcessing(position));
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    private IEnumerator BuildProcessing(Vector3 position)
    {
        while (CanBuild(position) == false)
        {
            _mover.Move(position);
            
            yield return null;
        }

        Base.UnbindUnit(this);
        var newBase = Base.Build(position);
        SetBase(newBase);
        Base.BindUnit(this);
        
        Builded?.Invoke(this);
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

    private bool CanBuild(Vector3 position)
        => IsEnoughDistance(position, transform.position, _buildDistance);
    
    private bool IsEnoughDistance(Vector3 target, Vector3 current, float closeDistance)
        => (current - target).sqrMagnitude <= closeDistance * closeDistance;
}