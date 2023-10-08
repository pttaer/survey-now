using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNQuestionLikertView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private GameObject m_QuestionItemPref;
    private Transform m_TQuestionGroup;

    private List<SNLikertQuestionItemView> m_ItemViewList;
    private SNSectionQuestionDTO m_Data;

    private void OnDestroy()
    {
        SNSurveyLikertQuestionControl.Api.OnOpenLikertOptionEvent -= TurnOptionsOff;
    }

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;
        m_Data = data;

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
            GenerateQuestionChoices(option.content);
        }
    }

    private void GenerateQuestionChoices(string title)
    {
        GameObject go = Instantiate(m_QuestionItemPref, m_TQuestionGroup);
        SNLikertQuestionItemView view = go.GetComponent<SNLikertQuestionItemView>();
        go.SetActive(true);
        m_ItemViewList.Add(view);
        view.Init(title, m_Data.columnOptions);
    }

    private void TurnOptionsOff()
    {
        m_ItemViewList?.ForEach(item => item.TurnOptionOff());
    }
}
