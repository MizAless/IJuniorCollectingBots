using UnityEngine;

public class MovingToBuildBase : IState
{
    private UnitStateMachine _unitStateMachine;
    private Vector3 _targetPosition;

    public MovingToBuildBase(UnitStateMachine unitStateMachine, Vector3 position)
    {
        _unitStateMachine = unitStateMachine; 
        _targetPosition = position;
    }
    
    public void Enter()
    {
        _unitStateMachine.Unit.Builded += OnBuilded;
        _unitStateMachine.Unit.BuildBase(_targetPosition);
    }

    private void OnBuilded(Unit unit)
    {
        unit.Builded -= OnBuilded;
        Debug.Log("Builded");
        _unitStateMachine.SetState(new IdleState(_unitStateMachine));
    }

    public void Exit()
    {
    }
}