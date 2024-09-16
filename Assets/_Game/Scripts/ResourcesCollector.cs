using System.Collections;
using UnityEngine;

public class ResourcesCollector : MonoBehaviour
{
    [SerializeField] private int _capacity = 1;
    [SerializeField] private int _collectValue = 1;
    [SerializeField] private float _cooldown = 1f;

    private int _currentCount = 0;
    public void Collect(Resources resources)
    {
        StartCoroutine(Collecting(resources));
    }

    private IEnumerator Collecting(Resources resources)
    {
        var wait = new WaitForSeconds(_cooldown);

        while (_capacity > _currentCount)
        {
            int remainingCapacity = _capacity - _currentCount;

            int collectValue = _collectValue >= remainingCapacity ? _collectValue : remainingCapacity;

            int resultCollectedValue = resources.Collect(collectValue);
            _currentCount += resultCollectedValue;

            yield return wait;
        }
    }
}
