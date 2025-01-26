using UnityEngine;
using UnityEngine.UI;

public class BaseResourcesValueView : MonoBehaviour
{
    private const string ResourcesValue = "resources value:";

    [SerializeField] private Text _text;
    
    private Base _gameBase;
    private CameraTracker _cameraTracker;
    
    public void Init(Base gameBase)
    {
        _gameBase = gameBase;
        _cameraTracker = new CameraTracker(transform);
        _cameraTracker.Init();
        UpdateTextView(_gameBase.ResourcesValue);
        AddListeners();
    }

    private void AddListeners()
    {
        _gameBase.ResourcesValueChanged += UpdateTextView;
    }

    private void RemoveListeners()
    {
        _gameBase.ResourcesValueChanged -= UpdateTextView;
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void UpdateTextView(int value)
    {
        _text.text = $"{ResourcesValue} {value}";
    }
}
