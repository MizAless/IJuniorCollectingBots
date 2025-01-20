using System;
using System.Collections.Generic;

public class StateMachine
{
    private IState _currentState;

    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

    public void AddState(IState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : IState
    {
        var type = typeof(T);

        if (_currentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState) == false) 
            return;
        
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}