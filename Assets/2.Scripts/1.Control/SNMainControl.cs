using System;
using UnityEngine;

public class SNMainControl
{
    public static SNMainControl Api;

    public Action OnClickMenuEvent;
    public Action OnDeleteSurveyBackToMySurveyEvent;
    public Action OnOpenProfileEvent;
    public Action OnOpenAccountlEvent;
    public Action OnOpenAccountPurchaseEvent;
    public Action OnOpenMySurveyEvent;
    public Action OnOpenPointsEvent;
    public Action OnOpenPayment;
    public Action<SNHistoryRecordType, string, string> OnCallHistoryRecorDetailEvent;

    public void OpenMenuPnl()
    {
        OnClickMenuEvent?.Invoke();
    }

    public void DeleteSurveyBackToMySurvey()
    {
        OnDeleteSurveyBackToMySurveyEvent?.Invoke();
    }

    public void OpenMySurvey()
    {
        OnOpenMySurveyEvent?.Invoke();
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

    public void OpenPoints()
    {
        OnOpenPointsEvent?.Invoke();
    }

    public void OpenPayment()
    {
        OnOpenPayment?.Invoke();
    }

    public void CallHistoryRecordDetail(SNHistoryRecordType type, string date, string points)
    {
        OnCallHistoryRecorDetailEvent?.Invoke(type, date, points);
    }
}
