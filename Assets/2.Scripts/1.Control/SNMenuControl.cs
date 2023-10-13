using System;
using UnityEngine;

public class SNMenuControl : MonoBehaviour
{
    public static SNMenuControl Api;

    public Action onOpenBuyPoints;

    public void OnOpenBuyPoints()
    {
        onOpenBuyPoints?.Invoke();
    }
}
