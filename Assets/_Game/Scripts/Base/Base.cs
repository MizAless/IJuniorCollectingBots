using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourcesScanner))]
public class Base : MonoBehaviour, IDestroyable<Base>
{
    private const int MinUnitsForBuilding = 2;

    [SerializeField] private ResourcesScanner _resourcesScaner;
    [SerializeField] private BaseResourcesValueView _resourcesValueView;

    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private int _unitCost = 3;
    [SerializeField] private int _baseCost = 5;

    private List<Unit> _units = new List<Unit>();

    private List<Resources> _knownResources = new List<Resources>();

    private int _resourcesValue;

    private bool _isReadyForBuild = false;

    private Coroutine _currentCoroutine;

    private Vector3 _buildPosition;

    public int ResourcesValue
    {
        get => _resourcesValue;
        private set
        {
            _resourcesValue = value;
            ResourcesValueChanged?.Invoke(_resourcesValue);
        }
    }

    private bool IsReadyForBuild
    {
        get
        {
            var result = _isReadyForBuild;
            _isReadyForBuild = false;
            return result;
        }
    }

    public bool CanBuild => _units.Count >= MinUnitsForBuilding;

    public event Action<Base> Disabled;
    public event Action<int> ResourcesValueChanged;

    public void Init(int resourcesValue, Vector3 position)
    {
        _resourcesValue = resourcesValue;
        transform.position = position;
        _resourcesValueView.Init(this);

        AddListeners();

        _currentCoroutine = StartCoroutine(SpawnProcessing());
        _resourcesScaner.Init();
    }

    public void PutResources(Resources resources)
    {
        if (resources.Value < 0)
            throw new ArgumentOutOfRangeException();

        ResourcesValue += resources.Value;
    }

    public void StartBuilding(Vector2 position)
    {
        if (CanBuild == false)
            return;

        _buildPosition = new Vector3(position.x, 0, position.y);

        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(BuildProcessing());
    }

    public Base Build(Vector3 position)
    {
        var baseSpawner = ServiceLocator.GetInstance<BaseSpawner>();
        var newBase = baseSpawner.Spawn();
        newBase.Init(0, position);

        ResourcesValue -= _baseCost;

        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(SpawnProcessing());

        return newBase;
    }

    public void BindUnit(Unit unit)
    {
        _units.Add(unit);
        unit.Released += OnUnitReleased;
    }

    public void UnbindUnit(Unit unit)
    {
        _units.Remove(unit);
        unit.Released -= OnUnitReleased;
    }

    private void TrySpawn()
    {
        if (_resourcesValue < _unitCost)
            return;

        ResourcesValue -= _unitCost;

        _unitSpawner
            .Spawn()
            .Init(this);
    }

    private void AddListeners()
    {
        _resourcesScaner.Scanned += OnScanned;
        _unitSpawner.ObjectSpawned += BindUnit;
    }

    private void RemoveListeners()
    {
        _resourcesScaner.Scanned -= OnScanned;
        _unitSpawner.ObjectSpawned -= BindUnit;

        foreach (var unit in _units)
        {
            unit.Released -= OnUnitReleased;
        }
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
    
    private IEnumerator BuildProcessing()
    {
        yield return new WaitUntil(() => _resourcesValue >= _baseCost);

        _isReadyForBuild = true;
    }

    private IEnumerator SpawnProcessing()
    {
        while (enabled)
        {
            TrySpawn();

            yield return new WaitForSeconds(_unitSpawner.Cooldown);
        }
    }

    private void SendBuild(Unit unit)
    {
        unit.SendBuild(_buildPosition);
    }

    private IEnumerator CollectAvailableResources(Unit unit)
    {
        yield return new WaitUntil(() => _knownResources.Count(resources => resources.IsAvailable) > 0);

        Resources collectingResources = _knownResources.FirstOrDefault(resources => resources.IsAvailable);
        unit.Collect(collectingResources);
    }

    private void OnUnitReleased(Unit unit)
    {
        if (IsReadyForBuild)
            SendBuild(unit);
        else
            StartCoroutine(CollectAvailableResources(unit));
    }

    private void OnScanned(List<Resources> scannedResources)
    {
        if (scannedResources.Count == 0)
            return;

        _knownResources.AddRange(scannedResources
            .Where(resources => _knownResources.Contains(resources) == false)
            .ToList());
    }
}