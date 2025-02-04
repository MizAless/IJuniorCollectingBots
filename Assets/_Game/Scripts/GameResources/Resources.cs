using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Resources : MonoBehaviour, IDestroyable<Resources>, IGrabable<Resources>
{
    [SerializeField] private int _startValue = 5;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private int _value;

    private bool _isAvailable = true;

    public event Action<Resources> Grabbed;
    public event Action<Resources> Disabled;

    public bool IsAvailable => _isAvailable;
    public int Value => _value;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
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
        _isAvailable = true;
        _rigidbody.velocity = Vector3.zero;
        _collider.isTrigger = false;
        _rigidbody.useGravity = true;
    }

    public void Privatize()
    {
        _isAvailable = false;
    }

    public void Grab()
    {
        _rigidbody.velocity = Vector3.zero;
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        Grabbed?.Invoke(this);
    }

    public void SelfDestroy()
    {
        Disabled?.Invoke(this);
    }
}
