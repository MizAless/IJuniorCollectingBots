using UnityEngine;

public class CameraTracker
{
    private Transform _transform;
    private Camera _camera;

    public CameraTracker(Transform transform)
    {
        _transform = transform;
        _camera = ServiceLocator.GetInstance<Camera>();
    }
    
    public void Init()
    {
        _transform.LookAt(_camera.transform.position);
        _transform.Rotate(Vector3.up, 180f);
    }
}