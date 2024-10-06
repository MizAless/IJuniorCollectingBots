using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance = 1f;

    public bool Move(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        direction.y = 0f;

        transform.position += direction.normalized * _speed * Time.deltaTime;

        if (direction.sqrMagnitude > _stoppingDistance * _stoppingDistance)
            return false;

        return true;
    }
}
