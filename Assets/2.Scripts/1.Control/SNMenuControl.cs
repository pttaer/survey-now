using System;
using UnityEngine;

public class SNMenuControl
{
    public static SNMenuControl Api;

    public Action onOpenPoints;

    public void OnOpenPoints()
    {
        onOpenPoints?.Invoke();
    }
}
