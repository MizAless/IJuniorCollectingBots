using Unity.VisualScripting;

public interface IState
{
    public void Handle(Unit unit);
}
