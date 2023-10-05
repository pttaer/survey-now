using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSurveyLikertQuestionControl
{
    public static SNSurveyLikertQuestionControl Api;

    public Action OnOpenLikertOptionEvent;

    public void OpenLikertOption()
    {
        OnOpenLikertOptionEvent?.Invoke();
    }
}
