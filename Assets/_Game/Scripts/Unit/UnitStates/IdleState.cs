using System.Collections;
using System.Linq;
using UnityEngine;

public class IdleState : IState
{
    private float _delay = 0.1f;

    public void Handle(Unit unit)
    {
        unit.StartCoroutine(WaitForNewResources(unit));
    }

    private IEnumerator WaitForNewResources(Unit unit)
    {
        while (unit.Base.KnownResources.Count(resources => resources.IsAvalable) == 0)
        {
            yield return new WaitForSeconds(_delay);
        }

        unit.SetState(new MoveingToResourcesState());
    }
}