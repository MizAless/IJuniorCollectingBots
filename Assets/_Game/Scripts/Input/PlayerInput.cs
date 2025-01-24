using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const int CastDistance = 100;
    
    [SerializeField] private KeyCode _click = KeyCode.Mouse0;
    [SerializeField] private LayerMask _layerMask;
    
    public event Action<Vector2> Clicked;
    
    private void Update()
    {
        if (Input.GetKeyDown(_click))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, CastDistance,_layerMask))
            {
                print(hit.collider.name);

                Vector2 position = new Vector2(hit.point.x, hit.point.z);

                Clicked?.Invoke(position);
            }
        }
    }
}