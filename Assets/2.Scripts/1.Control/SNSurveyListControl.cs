using System;

public class SNSurveyListControl
{
    public static SNSurveyListControl Api;

    public Action OnOpenSurveyHistoryEvent;
    public Action OnOpenMySurveyEvent;
    public Action OnClickBackToHomeEvent;
    public Action<int, bool> OnNotFinishSurveyPopupWarningEvent;
    public Action<SNSurveyResponseDTO> OnOpenSurveyDetailEvent;

    public void OpenSurveyHistory()
    {
        OnOpenSurveyHistoryEvent?.Invoke();
    }

    public void OpenMySurvey()
    {
        OnOpenMySurveyEvent?.Invoke();
    }

    public void ClickBackToHome()
    {
        OnClickBackToHomeEvent?.Invoke();
    }

    public void NotFinishSurveyPopupWarning(int questionId, bool isQuestionFinish)
    {
        OnNotFinishSurveyPopupWarningEvent?.Invoke(questionId, isQuestionFinish);
    }

    public void OpenSurveyDetail(SNSurveyResponseDTO data)
    {
        OnOpenSurveyDetailEvent?.Invoke(data);
    }
}
