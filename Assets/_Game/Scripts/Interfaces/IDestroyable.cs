using System;

public interface IDestroyable
{
    public event Action<IDestroyable> Disabled;
    public event Action<IDestroyable> Destroyed;
}
