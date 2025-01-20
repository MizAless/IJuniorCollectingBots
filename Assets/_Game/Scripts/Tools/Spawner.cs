using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour, IDestroyable<T>
{
    [SerializeField] private ObjectPool<T> _pool;

    public event Action<T> ObjectSpawned;
    public event Action<T> ObjectDisabled;

    public virtual T Spawn()
    {
        T newObject = _pool.Get();

        newObject.Disabled += OnDisabled;

        ObjectSpawned?.Invoke(newObject);

        return newObject;
    }

    private void OnDisabled(T obj)
    {
        obj.Disabled -= OnDisabled;
        ObjectDisabled?.Invoke(obj);
        _pool.Put(obj);
    }
}