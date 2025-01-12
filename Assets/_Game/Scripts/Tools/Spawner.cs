using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour, IDestroyable<T>
{
    [SerializeField] private ObjectPool<T> Pool;

    public event Action<T> ObjectSpawned;
    public event Action<T> ObjectDisabled;

    public virtual T Spawn()
    {
        T newObject = Pool.Get();

        newObject.Disabled += OnDisabled;

        ObjectSpawned?.Invoke(newObject);

        return newObject;
    }

    private void OnDisabled(T obj)
    {
        obj.Disabled -= OnDisabled;
        ObjectDisabled?.Invoke(obj);
        Pool.Put(obj);
    }
}