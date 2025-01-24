using UnityEngine;
using UnityEngine.UI;

public class BaseResourcesValueView : MonoBehaviour
{
    private const string ResourcesValue = "resources value:";

    [SerializeField] private Text _text;
    
    private Base _gameBase;
    private string _baseName;
    
    public void Init(Base gameBase, string baseName)
    {
        _gameBase = gameBase;
        _baseName = baseName;
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
        _text.text = $"{_baseName} {ResourcesValue} {value}";
    }
}
