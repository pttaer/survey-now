using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SNConstant;

public class SNSurveyListSurveyDetailView : MonoBehaviour
{
    private GameObject m_SurveyQuestionRadioView;
    private Transform m_QuestionContainer;

    public void Init(int id)
    {
        Debug.Log("ID " + id);
        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), RenderPage));

        m_SurveyQuestionRadioView = transform.Find("Viewport/Content/SurveyRecordRadio").gameObject;
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
                break;
            case "Radio":
                GenerateMultipleQuestion(question, m_SurveyQuestionRadioView);
                break;
            case "CheckBox":
                break;
            case "Selection":
                break;
            case "Rating":
                break;
            case "Likert":
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
