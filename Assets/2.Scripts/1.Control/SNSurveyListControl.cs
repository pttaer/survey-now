using System;

public class SNSurveyListControl
{
    public static SNSurveyListControl Api;

    public Action OnOpenSurveyHistoryEvent;
    public Action OnOpenMySurveyEvent;
    public Action<SNSurveyResponseDTO> OnOpenSurveyDetailEvent;

    public void OpenSurveyHistory()
    {
        OnOpenSurveyHistoryEvent?.Invoke();
    }

    public void OpenMySurvey()
    {
        OnOpenMySurveyEvent?.Invoke();
    }

    public void OpenSurveyDetail(SNSurveyResponseDTO data)
    {
        OnOpenSurveyDetailEvent?.Invoke(data);
    }
}
