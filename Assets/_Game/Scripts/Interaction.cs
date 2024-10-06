using System.Collections;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float _throwDuration = 1f;
    [SerializeField] private float _throwHight = 1f;
    [SerializeField] private AnimationCurve _throwXCurve;
    [SerializeField] private AnimationCurve _throwYCurve;

    private Resources _resources;

    private Coroutine _throwCoroutine;

    public bool isEmpty => _resources != null;

    public void Grab(Resources resources)
    {
        _resources = resources;

        _resources.Grab();
        _resources.transform.position = transform.position;
        _resources.transform.rotation = transform.rotation;
        _resources.transform.SetParent(transform);

        _resources.Disabled += OnResourcesDisabled;
    }

    public Resources Give(Vector3 target)
    {
        _resources.transform.parent = null;

        _throwCoroutine = StartCoroutine(Throw(target, _resources));

        return _resources;
    }

    private IEnumerator Throw(Vector3 target, Resources resources)
    {
        float elapsedTime = 0;

        Vector3 startPosition = resources.transform.position;

        while (elapsedTime < _throwDuration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(startPosition, target, _throwXCurve.Evaluate(Mathf.Clamp01(elapsedTime / _throwDuration)));
            newPosition.y = Mathf.Lerp(0, 1, _throwYCurve.Evaluate(Mathf.Clamp01(elapsedTime / _throwDuration))) * _throwHight;
            _resources.transform.position = newPosition;
            yield return null;
        }

        resources.SelfDestroy();
    }

    private void OnResourcesDisabled(Resources resources)
    {
        _resources.Disabled -= OnResourcesDisabled;

        if (_throwCoroutine != null)
            StopCoroutine(_throwCoroutine);

        _resources = null;
    }
}
