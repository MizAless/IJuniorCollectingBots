using System;
using UnityEngine;

public interface IGrabable<T>
    where T : MonoBehaviour
{
    public event Action<T> Grabbed;
}
