using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    
    private Flag _flag;
    
    private bool _isActive = false;

    public void Init()
    {
        _flag = Instantiate(_flagPrefab, Vector3.zero, Quaternion.identity);
        
        _flag.gameObject.SetActive(_isActive);
    }
    
    public void SetFlag(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, 0, position.y);

        _flag.transform.position = worldPosition;
        _isActive = true;
        _flag.gameObject.SetActive(_isActive);
    }

    public void HideFlag()
    {
        _isActive = false;
        _flag.gameObject.SetActive(_isActive);
    }
}