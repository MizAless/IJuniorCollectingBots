using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour, IDestroyable
{
    [SerializeField] private ObjectPool<T> Pool;

    public event Action<T> Instatiated;
    public event Action<T> Spawned;

    private void OnEnable()
    {
        Instatiated += AddListeners;
    }

    private void OnDisable()
    {
        Instatiated -= AddListeners;
    }

    public virtual T Spawn()
    {
        T newObject = Pool.Get(out bool isInstantiated);

        if (isInstantiated)
        {
            Instatiated?.Invoke(newObject);
        }

        Spawned?.Invoke(newObject);
        return newObject;
    }

    public virtual void AddListeners(T instantiatedObject)
    {
        instantiatedObject.Disabled += PutInPull;
        instantiatedObject.Destroyed += RemoveListeners;
    }

    public virtual void RemoveListeners(IDestroyable destroyableObject)
    {
        T poolObject = Convert(destroyableObject);

        if (poolObject != null)
        {
            poolObject.Disabled += PutInPull;
            poolObject.Destroyed += RemoveListeners;
        }
    }

    private T Convert(IDestroyable destroyableObject)
    {
        if (destroyableObject is T poolObject)
            return poolObject;

        return null;
    }

    private void PutInPull(IDestroyable destroyableObject)
    {
        T putedObject = Convert(destroyableObject);

        if (putedObject != null)
            Pool.Put(putedObject);
    }
}