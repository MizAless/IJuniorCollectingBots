using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourcesSpawnLogic _resourcesSpawnLogic;
    
    private void Start()
    {
        _base.Init();
        _unitSpawner.Init();
        _resourcesSpawnLogic.Init();
    }
}
