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

        resources.Grab();
        resources.transform.position = transform.position;
        resources.transform.rotation = transform.rotation;
        resources.transform.SetParent(transform);
    }

    public Resources Give(Vector3 target)
    {
        _resources.transform.parent = null;
        StartCoroutine(Throw(target, _resources));

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
            resources.transform.position = newPosition;
            yield return null;
        }

        resources.SelfDestroy();
    }
}
