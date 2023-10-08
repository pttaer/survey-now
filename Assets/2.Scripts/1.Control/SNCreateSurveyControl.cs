using System;
using UnityEngine;

public class SNCreateSurveyControl
{
    public static SNCreateSurveyControl Api;

    public Action<string> OnClickChooseQuestionTypeEvent;
    public Action<GameObject> OnDeleteItemReOrderQuestionListEvent;

    public void ClickChooseQuestionType(string questionType)
    {
        OnClickChooseQuestionTypeEvent?.Invoke(questionType);
    }

    public void DeleteItemReOrderQuestionList(GameObject go)
    {
        OnDeleteItemReOrderQuestionListEvent?.Invoke(go);
    }
}
