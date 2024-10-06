using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] private ResourcesSpawner _resourcesSpawner;

    private ResourcesStorage _resourcesStorage;

    public ResourcesStorage ResourcesStorage  => _resourcesStorage;

    private void Awake()
    {
        Instance = this;

        _resourcesStorage = new ResourcesStorage(_resourcesSpawner);
    }
}
