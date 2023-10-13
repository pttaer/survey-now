using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SNDoSurveyDTO;

public class SNQuestionLikertView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private GameObject m_QuestionItemPref;
    private Transform m_TQuestionGroup;

    private List<SNLikertQuestionItemView> m_ItemViewList;
    private SNSectionQuestionDTO m_Data;
    private int m_QuestionId;

    private void OnDestroy()
    {
        SNSurveyLikertQuestionControl.Api.OnOpenLikertOptionEvent -= TurnOptionsOff;
    }

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;
        m_Data = data;
        m_QuestionId = data.id;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_QuestionItemPref = transform.Find("QuestionGr/QuestionItem").gameObject;
        m_TQuestionGroup = transform.Find("QuestionGr");

        SNSurveyLikertQuestionControl.Api.OnOpenLikertOptionEvent += TurnOptionsOff;

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);
        m_ItemViewList = new();

        foreach (var option in data.rowOptions)
        {
            GenerateQuestionChoices(option.content, option.order);
        }
    }

    private void GenerateQuestionChoices(string title, int order)
    {
        GameObject go = Instantiate(m_QuestionItemPref, m_TQuestionGroup);
        SNLikertQuestionItemView view = go.GetComponent<SNLikertQuestionItemView>();
        go.SetActive(true);
        m_ItemViewList.Add(view);
        view.Init(title, order, m_Data.columnOptions);
    }

    private void TurnOptionsOff()
    {
        m_ItemViewList?.ForEach(item => item.TurnOptionOff());
    }

    public override AnswerDTO GetAnswer()
    {
        List<AnswerOptionDTO> answerOptions = new();

        foreach (SNLikertQuestionItemView view in m_ItemViewList)
        {
            answerOptions.Add(view.GetAnswer());
        }

        return new AnswerDTO()
        {
            QuestionId = m_QuestionId,
            Content = null,
            RateNumber = null,
            AnswerOptions = answerOptions
        };
    }

    public override bool Validate()
    {
        foreach (SNLikertQuestionItemView view in m_ItemViewList)
        {
            if (!view.Validate())
            {
                return false;
            }
        }
        return true;
    }

    public override void SetAnswer(SNSurveyAnswerDTO.AnswerResponseDTO answer)
    {
        for (int i = 0; i < m_ItemViewList.Count; i++)
        {
            Debug.Log("rowOrder: " + answer.answerOptions[i].rowOrder);
            Debug.Log("columnOrder: " + answer.answerOptions[i].columnOrder);

            m_ItemViewList[i].SetAnswer(answer.answerOptions[i]);
            m_TQuestionGroup.gameObject.SetActive(true);
        }
    }
}
