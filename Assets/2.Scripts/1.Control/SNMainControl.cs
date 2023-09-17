using System;
using UnityEngine;

public class SNMainControl : MonoBehaviour
{
    public static SNMainControl Api;

    public Action OnClickMenuEvent;
    public Action OnOpenProfileEvent;
    public Action OnOpenAccountlEvent;
    public Action OnOpenAccountPurchaseEvent;

    public void OpenMenuPnl()
    {
        OnClickMenuEvent?.Invoke();
    }

    public void OpenProfile()
    {
        OnOpenProfileEvent?.Invoke();
    }

    public void OpenAccount()
    {
        OnOpenAccountlEvent?.Invoke();
    }

    public void OpenAccountPurchase()
    {
        OnOpenAccountPurchaseEvent?.Invoke();
    }
}
