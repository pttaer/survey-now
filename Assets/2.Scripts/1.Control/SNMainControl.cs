using System;
using UnityEngine;

public class SNMainControl : MonoBehaviour
{
    public static SNMainControl Api;

    public Action OnClickMenuEvent;
    public Action OnOpenProfileEvent;
    public Action OnOpenAccountlEvent;
    public Action OnOpenAccountPurchaseEvent;
    public Action<SNHistoryRecordType, string, string> OnCallHistoryRecorDetailEvent;

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

    public void CallHistoryRecordDetail(SNHistoryRecordType type, string date, string points)
    {
        OnCallHistoryRecorDetailEvent?.Invoke(type, date, points);
    }
}
