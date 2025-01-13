using System;
using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private Flag _flagPrefab;
    
    private Flag _flag;
    
    private bool _isActive = false;

    private void Awake()
    {
        _flag = Instantiate(_flagPrefab, Vector3.zero, Quaternion.identity);
        
        _flag.gameObject.SetActive(_isActive);
    }

    private void OnEnable()
    {
        _playerInput.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _playerInput.Clicked -= OnClicked;
    }

    private void OnClicked(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, 0, position.y);

        _flag.transform.position = worldPosition;
        _isActive = true;
        _flag.gameObject.SetActive(_isActive);
    }
}