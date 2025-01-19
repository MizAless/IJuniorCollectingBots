public class IdleState : IState
{
    private UnitStateMachine _unitStateMachine;
    
    public IdleState(UnitStateMachine unitStateMachine)
    {
        _unitStateMachine = unitStateMachine;
    }
    
    public void Enter()
    {
        _unitStateMachine.Unit.Release();
    }

    public void Exit()
    {
    }
}