using System.Collections.Generic;
using UnityEngine;

public class SNSurveyListSurveyDetailView : MonoBehaviour
{
    private GameObject m_SurveyQuestionRadioView;
    private GameObject m_SurveyQuestionMultipleView;
    private GameObject m_SurveyQuestionRatingView;
    private GameObject m_SurveyQuestionLikertView;
    private GameObject m_SurveyQuestionCustomView;

    private Transform m_QuestionContainer;

    public void Init(int id)
    {
        Debug.Log("ID " + id);
        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), RenderPage));

        m_SurveyQuestionRadioView = transform.Find("Viewport/Content/SurveyRecordRadio").gameObject;
        m_SurveyQuestionMultipleView = transform.Find("Viewport/Content/SurveyRecordMultiple").gameObject;
        m_SurveyQuestionRatingView = transform.Find("Viewport/Content/SurveyRecordRating").gameObject;
        m_SurveyQuestionLikertView = transform.Find("Viewport/Content/SurveyRecordLikert").gameObject;
        m_SurveyQuestionCustomView = transform.Find("Viewport/Content/SurveyRecordCustom").gameObject;

        m_QuestionContainer = m_SurveyQuestionRadioView.transform.parent;
    }

    private void RenderPage(SNSurveyQuestionDetailDTO data)
    {
        List<SNSectionQuestionRowOptionDTO> options = new List<SNSectionQuestionRowOptionDTO>();
        List<SNSectionQuestionDTO> questions = new List<SNSectionQuestionDTO>();

        data?.sections?.ForEach(section =>
            {
                section?.questions?.ForEach(
                    question =>
                    {
                        SpawnQuestion(question, options, questions);
                    });
            });
    }

    private void SpawnQuestion(SNSectionQuestionDTO question, List<SNSectionQuestionRowOptionDTO> options, List<SNSectionQuestionDTO> questions)
    {
        questions.Add(question);
        question?.rowOptions.ForEach(
            option =>
            {
                if (option.content != null) options.Add(option);
            });

        switch (question.type)
        {
            case "Text":
                GenerateMultipleQuestion(question, m_SurveyQuestionCustomView);
                break;
            case "Radio":
                GenerateMultipleQuestion(question, m_SurveyQuestionRadioView);
                break;
            case "CheckBox":
                GenerateMultipleQuestion(question, m_SurveyQuestionMultipleView);
                break;
            case "Selection":
                //GenerateMultipleQuestion(question, m_SurveyQuestionRadioView);
                break;
            case "Rating":
                GenerateMultipleQuestion(question, m_SurveyQuestionRatingView);
                break;
            case "Likert":
                GenerateMultipleQuestion(question, m_SurveyQuestionLikertView);
                break;
            default:
                break;
        }
    }

    private void GenerateMultipleQuestion(SNSectionQuestionDTO data, GameObject goPref)
    {
        GameObject go = Instantiate(goPref, m_QuestionContainer);
        SNInitView view = go.GetComponent<SNInitView>();
        go.SetActive(true);
        view.Init(data);
    }
}
