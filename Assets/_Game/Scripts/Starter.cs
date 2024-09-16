using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;

    public static Starter Instance;

    private ResourcesStorage _resourcesStorage;

    public ResourcesStorage ResourcesStorage  => _resourcesStorage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _resourcesStorage = new ResourcesStorage(_resourcesSpawner);
    }
}
