using System;
using UnityEngine;

public class SNMainControl : MonoBehaviour
{
    public static SNMainControl Api;

    public Action onClickMenu;

    public void OpenMenuPnl()
    {
        onClickMenu?.Invoke();
    }
}
