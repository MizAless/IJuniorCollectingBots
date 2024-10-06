using System;
using UnityEngine;

public interface IDestroyable<T> 
    where T : MonoBehaviour
{
    public event Action<T> Disabled;
}
