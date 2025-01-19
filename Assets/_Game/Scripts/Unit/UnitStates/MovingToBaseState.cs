public class MovingToBaseState : IState
{
    private UnitStateMachine _unitStateMachine;

    public MovingToBaseState(UnitStateMachine unitStateMachine)
    {
        _unitStateMachine = unitStateMachine;
    }

    public void Enter()
    {
        _unitStateMachine.Unit.Gave += OnGave;
        _unitStateMachine.Unit.Give(_unitStateMachine.Unit.Base);
    }

    public void Exit()
    {
    }

    private void OnGave(Unit unit)
    {
        unit.Gave -= OnGave;
        _unitStateMachine.SetState(new IdleState(_unitStateMachine));
    }
}