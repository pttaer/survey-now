using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNCreateQuestionTypePnlView : MonoBehaviour
{
    private Button m_Radio;
    private Button m_Multiple;
    private Button m_Rating;
    private Button m_Custom;
    private Button m_Likert;

    private const string RADIO_QUESTION_TYPE = "Radio";
    private const string MULTIPLE_QUESTION_TYPE = "Multiple";
    private const string RATING_QUESTION_TYPE = "Rating";
    private const string CUSTOM_QUESTION_TYPE = "Custom";
    private const string LIKERT_QUESTION_TYPE = "Likert";

    public void Init()
    {
        Transform content = transform.Find("SurveyAdd/Viewport/Content");

        m_Radio = content.Find(RADIO_QUESTION_TYPE).GetComponent<Button>();
        m_Multiple = content.Find(MULTIPLE_QUESTION_TYPE).GetComponent<Button>();
        m_Rating = content.Find(RATING_QUESTION_TYPE).GetComponent<Button>();
        m_Custom = content.Find(CUSTOM_QUESTION_TYPE).GetComponent<Button>();
        m_Likert = content.Find(LIKERT_QUESTION_TYPE).GetComponent<Button>();

        m_Radio.onClick.AddListener(() => ClickQuestion(RADIO_QUESTION_TYPE));
        m_Multiple.onClick.AddListener(() => ClickQuestion(MULTIPLE_QUESTION_TYPE));
        m_Rating.onClick.AddListener(() => ClickQuestion(RATING_QUESTION_TYPE));
        m_Custom.onClick.AddListener(() => ClickQuestion(CUSTOM_QUESTION_TYPE));
        m_Likert.onClick.AddListener(() => ClickQuestion(LIKERT_QUESTION_TYPE));
    }

    private static void ClickQuestion(string type)
    {
        SNCreateSurveyControl.Api.ClickChooseQuestionType(type);
    }
}
