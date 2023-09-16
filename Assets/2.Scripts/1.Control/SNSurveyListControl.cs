using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSurveyListControl
{
    public static SNSurveyListControl Api;

    public Action OnOpenSurveyHistoryEvent;
    public Action OnOpenMySurveylEvent;
    public Action OnOpenSurveyDetailEvent;

    public void OpenSurveyHistory()
    {
        OnOpenSurveyHistoryEvent?.Invoke();
    }

    public void OpenMySurvey()
    {
        OnOpenMySurveylEvent?.Invoke();
    }

    public void OpenSurveyDetail()
    {
        OnOpenSurveyDetailEvent?.Invoke();
    }
}
