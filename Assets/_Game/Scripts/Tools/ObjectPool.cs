using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private Queue<T> _pool;

    private void Awake()
    {
        _pool = new();
    }

    public T Get()
    {
        T newObject;

        if (_pool.Count == 0)
        {
            newObject = Instantiate(_prefab, _container);
            return newObject;
        }

        newObject = _pool.Dequeue();
        newObject.gameObject.SetActive(true);

        return newObject;
    }

    public void Put(T putedObject)
    {
        _pool.Enqueue(putedObject);
        putedObject.transform.parent = _container;
        putedObject.gameObject.SetActive(false);
    }
}