using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();

    public static void Init()
    {
        Instances.Clear();
    }
    
    public static void Register<TContract>(TContract instance)
    {
        if (!Instances.TryAdd(typeof(TContract), instance))
            throw new ArgumentException($"An instance of this type: {typeof(TContract)} is already registered.");
    }

    public static TInstance GetInstance<TInstance>()
        => (TInstance)Instances[typeof(TInstance)];
}