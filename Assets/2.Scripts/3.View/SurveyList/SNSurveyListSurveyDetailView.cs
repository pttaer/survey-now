using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SNSurveyListSurveyDetailView : MonoBehaviour
{
    private GameObject m_SurveyQuestionRadioView;

    public void Init(int id)
    {
        Debug.Log("ID " + id);
        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), RenderPage));

        m_SurveyQuestionRadioView = transform.Find("Viewport/Content/SurveyRecordRadio").gameObject;
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
                        if (question.type == "Radio")
                        {
                            questions.Add(question);
                            question?.rowOptions.ForEach(
                                option =>
                                {
                                    if (option.content != null) options.Add(option);
                                });
                            GenerateRadioQuestion(question);
                        }
                    });
            });

        foreach (var item in questions)
        {
            Debug.Log("Question " + item.title);
        }

        foreach (var item in options)
        {
            Debug.Log("Content " + item.content);
        }
    }

    private void GenerateRadioQuestion(SNSectionQuestionDTO data)
    {
        GameObject go = Instantiate(m_SurveyQuestionRadioView, m_SurveyQuestionRadioView.transform.parent);
        SNQuestionRadioView view = go.GetComponent<SNQuestionRadioView>();
        go.SetActive(true);
        view.Init(data);
    }
}
