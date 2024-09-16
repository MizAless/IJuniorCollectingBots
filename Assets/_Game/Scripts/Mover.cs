using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _approachDistance = 1f;

    public event Action Reached;

    public void Move(Transform target)
    {
        StartCoroutine(Moveing(target));
    }

    private IEnumerator Moveing(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        while (direction.sqrMagnitude > _approachDistance * _approachDistance)
        {
            direction = target.position - transform.position;

            direction.y = 0f;

            transform.position += direction.normalized * _speed * Time.deltaTime;

            yield return null;
        }

        Reached?.Invoke();
    }
}
