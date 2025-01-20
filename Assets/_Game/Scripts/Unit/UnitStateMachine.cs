public class UnitStateMachine : StateMachine
{
    public Unit Unit { get; private set; }

    public UnitStateMachine(Unit unit)
    {
        Unit = unit;
        SetState(new IdleState(this));
    }
}