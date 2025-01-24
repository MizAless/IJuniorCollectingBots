using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Vector3 _mainBasePosition;
    [SerializeField] private int _mainBaseStartResourcesValue;
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private ResourcesSpawnLogic _resourcesSpawnLogic;
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private UICreator _uiCreator;

    private Base _base;
    
    private void Start()
    {
        InstallBindings();
        
        _flagSetter.Init();
        _uiCreator.Init();
        _base = _baseSpawner.Spawn();
        _base.Init(_mainBaseStartResourcesValue, _mainBasePosition);
        _game.Init(_base);
        _resourcesSpawnLogic.Init();
    }
    
    private void InstallBindings()
    {
        ServiceLocator.Init();
        ServiceLocator.Register<BaseSpawner>(_baseSpawner);
        ServiceLocator.Register<ResourcesStorage>(_resourcesStorage);
        ServiceLocator.Register<PlayerInput>(_playerInput);
        ServiceLocator.Register<FlagSetter>(_flagSetter);
        ServiceLocator.Register<UICreator>(_uiCreator);
    }
}
