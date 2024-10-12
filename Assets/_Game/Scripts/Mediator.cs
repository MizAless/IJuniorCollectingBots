//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class Mediator : MonoBehaviour
//{
//    [SerializeField] private Base _base;
//    [SerializeField] private UnitSpawner _unitSpawner;

//    private List<Unit> _busyUnits = new List<Unit>();
//    private List<Unit> _freeUnits = new List<Unit>();

//    private void OnEnable()
//    {
//        _base.ResourcesFinded += NotifyCollect;
//        _unitSpawner.ObjectSpawned += AddUnit;
//    }

//    private void OnDisable()
//    {
//        _base.ResourcesFinded -= NotifyCollect;
//        _unitSpawner.ObjectSpawned -= AddUnit;
//    }

//    private void NotifyCollect(IReadOnlyList<Resources> resourcesList)
//    {
//        var freeUnits = _freeUnits.ToArray();

//        int iterationsCount = _freeUnits.Count < resourcesList.Count ? _freeUnits.Count : resourcesList.Count;

//        for (int i = 0; i < iterationsCount; i++) 
//        {
//            Unit unit = freeUnits[i];
//            Resources grabbingResources = resourcesList[i];

//            unit.Grab(grabbingResources);

//            unit.Grabbed += OnUnitResourcesGrabbed;
//            _freeUnits.Remove(unit);
//            _busyUnits.Add(unit);
//        }
//    }

//    private void OnUnitResourcesGrabbed(Unit unit)
//    {
//        unit.Give(_base);
//    }

//    private void AddUnit(Unit unit)
//    {
//        _freeUnits.Add(unit);
//    }
//}
