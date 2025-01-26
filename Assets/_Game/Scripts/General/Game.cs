using UnityEngine;

public class Game : MonoBehaviour
{
    private PlayerInput _playerInput;
    private FlagSetter _flagSetter;
    private BaseSpawner _baseSpawner;
    private BuildValidater _buildValidater;
    private Base _mainBase;

    public void Init(Base mainBase)
    {
        _playerInput = ServiceLocator.GetInstance<PlayerInput>();
        _flagSetter = ServiceLocator.GetInstance<FlagSetter>();
        _baseSpawner = ServiceLocator.GetInstance<BaseSpawner>();

        _mainBase = mainBase;
        _buildValidater = new BuildValidater(_mainBase);
        
        AddListeners();
    }

    private void AddListeners()
    {
        _playerInput.Clicked += _buildValidater.Validate;

        _baseSpawner.ObjectSpawned += OnBaseSpawned;
    }

    private void RemoveListeners()
    {
        _playerInput.Clicked += _buildValidater.Validate;
        
        _baseSpawner.ObjectSpawned += OnBaseSpawned;
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnBaseSpawned(Base _)
    {
        _flagSetter.HideFlag();
    }
}