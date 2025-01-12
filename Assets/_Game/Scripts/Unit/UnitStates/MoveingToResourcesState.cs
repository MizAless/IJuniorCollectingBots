using System.Linq;

public class MoveingToResourcesState : IState
{
    public void Handle(Unit unit)
    {
        Resources grabbingResources = unit.Base.KnownResources
            .FirstOrDefault(resources => resources.IsAvalable);

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