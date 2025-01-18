public class MoveingToResourcesState : IState
{
    private Resources _resources;
    
    public MoveingToResourcesState(Resources resources)
    {
        _resources = resources;
    }
    
    public void Handle(Unit unit)
    {
        if (_resources == null)
        {
            unit.SetState(new IdleState());
            return;
        }

        _resources.Privatize();

        unit.Grabbed += OnGrabbed;
        unit.Grab(_resources);
    }

    private void OnGrabbed(Unit unit)
    {
        unit.Grabbed -= OnGrabbed;
        unit.SetState(new MoveingToBaseState());
    }
}