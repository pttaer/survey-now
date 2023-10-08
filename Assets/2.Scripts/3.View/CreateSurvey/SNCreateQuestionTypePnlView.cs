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

    public void Init()
    {
        m_Radio = transform.Find("SurveyAdd/Viewport/Content/Radio").GetComponent<Button>();
        m_Multiple = transform.Find("SurveyAdd/Viewport/Content/Multiple").GetComponent<Button>();
        m_Rating = transform.Find("SurveyAdd/Viewport/Content/Rating").GetComponent<Button>();
        m_Custom = transform.Find("SurveyAdd/Viewport/Content/Custom").GetComponent<Button>();
        m_Likert = transform.Find("SurveyAdd/Viewport/Content/Likert").GetComponent<Button>();

        m_Radio.onClick.AddListener(() => ClickQuestion("Radio"));
        m_Multiple.onClick.AddListener(() => ClickQuestion("Multiple"));
        m_Rating.onClick.AddListener(() => ClickQuestion("Rating"));
        m_Custom.onClick.AddListener(() => ClickQuestion("Custom"));
        m_Likert.onClick.AddListener(() => ClickQuestion("Likert"));
    }

    private static void ClickQuestion(string type)
    {
        SNCreateSurveyControl.Api.ClickChooseQuestionType(type);
    }
}
