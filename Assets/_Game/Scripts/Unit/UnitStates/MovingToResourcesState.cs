public class MovingToResourcesState : IState
{
    private Resources _resources;
    private UnitStateMachine _unitStateMachine;

    public MovingToResourcesState(UnitStateMachine unitStateMachine, Resources resources)
    {
        _unitStateMachine = unitStateMachine;
        _resources = resources;
    }

    public void Enter()
    {
        if (_resources == null)
        {
            _unitStateMachine.SetState(new IdleState(_unitStateMachine));
            return;
        }

        _resources.Privatize();

        _unitStateMachine.Unit.Grabbed += OnGrabbed;
        _unitStateMachine.Unit.Grab(_resources);
    }

    private void OnGrabbed(Unit unit)
    {
        unit.Grabbed -= OnGrabbed;
        _unitStateMachine.SetState(new MovingToBaseState(_unitStateMachine));
    }

    public void Exit()
    {
    }
}