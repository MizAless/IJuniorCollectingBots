using UnityEngine;

public class ResourcesSpawner : Spawner<Resources>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector2 _spawnPositionPossibleValue;

    public override Resources Spawn()
    {
        Resources resources = base.Spawn();

        Vector3 spawnPositionOffset = new Vector3(Random.Range(-1f,1f) * _spawnPositionPossibleValue.x, 0, Random.Range(-1f, 1f) * _spawnPositionPossibleValue.y);
        Vector3 spawnPosition = _spawnPoint.position + spawnPositionOffset;

        resources.Init(spawnPosition, Quaternion.identity);
        return resources;
    }
}
