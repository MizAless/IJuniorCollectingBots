using UnityEngine;
using UnityEngine.UI;

public class BaseValueView : MonoBehaviour
{
    private const string ResourcesValue = "Resources value: ";

    [SerializeField] private Base _gameBase;
    [SerializeField] private Text _text;

    private int _startValue = 0;

    private void Start()
    {
        UpdateTextView(_startValue);
    }

    private void OnEnable()
    {
        _gameBase.ResourcesValueChanged += UpdateTextView;
    }

    private void OnDisable()
    {
        _gameBase.ResourcesValueChanged -= UpdateTextView;
    }

    private void UpdateTextView(int value)
    {
        _text.text = ResourcesValue + value.ToString();
    }
}
