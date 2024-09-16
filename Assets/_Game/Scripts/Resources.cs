using System;
using UnityEngine;

public class Resources : MonoBehaviour, IDestroyable
{
    [SerializeField] private int _startValue = 5;

    private int _value;

    public event Action<IDestroyable> Disabled;
    public event Action<IDestroyable> Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }

    private void Start()
    {
        _value = _startValue;
    }

    public void Init(Vector3 position, Quaternion quaternion)
    {
        _value = _startValue;
        transform.position = position;
        transform.rotation = quaternion;
    }

    public int Collect(int collectedValue)
    {
        if (collectedValue < 0)
            throw new ArgumentOutOfRangeException();

        int resultCollectedValue = _value >= collectedValue ? collectedValue : _value;

        _value -= resultCollectedValue;

        if (_value == 0)
            SelfDestroy();

        return resultCollectedValue;
    }

    private void SelfDestroy()
    {
        Disabled?.Invoke(this);
    }
}
