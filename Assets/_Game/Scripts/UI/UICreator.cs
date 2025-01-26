using UnityEngine;

public class UICreator : MonoBehaviour
{
    private const string NotificationText =
        "You cannot build base: units count of main base is less than 2";
    
    [SerializeField] private Notification _notification;

    public void Notify()
    {
        _notification.Activate(NotificationText);
    }
}