using System;
using UnityEngine;

public class Resources : MonoBehaviour, IDestroyable<Resources>, IGrabable<Resources>
{
    [SerializeField] private int _startValue = 5;

    private int _value;

    private bool _isActive = true;

    public event Action<Resources> Grabbed;
    public event Action<Resources> Disabled;

    public bool IsActive => _isActive;
    public int Value => _value;

    private void Start()
    {
        _value = _startValue;
    }

    public void Init(Vector3 position, Quaternion quaternion)
    {
        _value = _startValue;
        transform.position = position;
        transform.rotation = quaternion;
        _isActive = true;
    }

    //public int Collect(int collectedValue)
    //{
    //    if (collectedValue < 0)
    //        throw new ArgumentOutOfRangeException();

    //    int resultCollectedValue = _value >= collectedValue ? collectedValue : _value;

    //    _value -= resultCollectedValue;

    //    if (_value == 0)
    //        SelfDestroy();

    //    return resultCollectedValue;
    //}

    public void Grab()
    {
        _isActive = false;
        Grabbed?.Invoke(this);
    }

    public void SelfDestroy()
    {
        Disabled?.Invoke(this);
    }
}
