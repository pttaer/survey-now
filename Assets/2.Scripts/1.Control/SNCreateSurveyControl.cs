using System;
using UnityEngine;

public class SNCreateSurveyControl
{
    public static SNCreateSurveyControl Api;

    public Action<string> OnClickChooseQuestionTypeEvent;

    public void ClickChooseQuestionType(string questionType)
    {
        OnClickChooseQuestionTypeEvent?.Invoke(questionType);
    }
}
