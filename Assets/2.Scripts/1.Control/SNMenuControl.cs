using System;
using UnityEngine;

public class SNMenuControl : MonoBehaviour
{
    public static SNMenuControl Api;

    public Action onOpenPoints;

    public void OnOpenPoints()
    {
        onOpenPoints?.Invoke();
    }
}
