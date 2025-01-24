using UnityEngine;

public class BuildValidater
{
    private readonly Base _mainBase;
    private readonly FlagSetter _flagSetter;
    private readonly UICreator _uiCreator;

    public BuildValidater(Base mainBase)
    {
        _mainBase = mainBase;
        _flagSetter = ServiceLocator.GetInstance<FlagSetter>();
        _uiCreator = ServiceLocator.GetInstance<UICreator>();
    }
    
    public void Validate(Vector2 clickPosition)
    {
        if (_mainBase.CanBuild)
        {
            _mainBase.StartBuilding(clickPosition);
            _flagSetter.SetFlag(clickPosition);
        }
        else
        {
            _uiCreator.Notify();
        }
    }
}