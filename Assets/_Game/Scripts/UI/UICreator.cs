using UnityEngine;

public class UICreator : MonoBehaviour
{
    private const string NotificationText =
        "You cannot build base: units count of main base is less than 2";

    [SerializeField] private BaseResourcesValueView _baseResourcesValueView;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Notification _notification;

    private static int _baseCount;

    public void Init()
    {
        _baseCount = 1;
    }

    public void CreateResourcesView(Base gameBase)
    {
        var resourcesView = Instantiate(_baseResourcesValueView, _container);
        resourcesView.Init(gameBase, $"Base №{_baseCount++}");
    }

    public void Notify()
    {
        _notification.Activate(NotificationText);
    }
}