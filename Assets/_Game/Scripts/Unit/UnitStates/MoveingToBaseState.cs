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