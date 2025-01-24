using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance = 1f;

    public void Move(Transform target)
    {
        Move(target.position);
    }
    
    public void Move(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;
        transform.position += direction.normalized * _speed * Time.deltaTime;
    }
}
