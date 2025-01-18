public class IdleState : IState
{
    private float _delay = 0.1f;

    public void Handle(Unit unit)
    {
        unit.Release();
    }
}