using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Text _notificationText;
    [SerializeField] private float _activateCooldown = 1f;
    
    private bool _isActive = false;
    
    public void Activate(string message)
    {
        if (_isActive)
            return;
        
        SetActive(true);
        _notificationText.text = message;
        StartCoroutine(StartHiding());
    }
    
    private void SetActive(bool isActive)
    {
        _isActive = isActive;
        gameObject.SetActive(_isActive);
    }
    
    private IEnumerator StartHiding()
    {
        yield return new WaitForSeconds(_activateCooldown);
        
        Hide();
    }
    
    private void Hide()
    {
        SetActive(false);
    }
}