using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyQuestionLikertView : SNSurveyQuestionBaseView
{
    private InputField m_IpfQuestion;
    private Button m_BtnAddQuestion;
    private Button m_BtnAddOption;
    private GameObject m_ItemQuestionOptionPref;
    private GameObject m_ItemOptionPref;

    private List<InputField> m_IpfRowQuestionsList;
    private List<InputField> m_IpfColumnQuestionsList;

    public override void Init()
    {
        m_IpfQuestion = transform.Find("Option/IpfQuestion").GetComponent<InputField>();
        m_BtnAddQuestion = transform.Find("BtnAddQuestion").GetComponent<Button>();
        m_BtnAddOption = transform.Find("BtnAddOption").GetComponent<Button>();

        m_ItemQuestionOptionPref = transform.Find("IpfQuestion").gameObject;
        m_ItemOptionPref = transform.Find("Option").gameObject;

        m_IpfRowQuestionsList = new();
        m_IpfColumnQuestionsList = new();

        m_BtnAddQuestion.onClick.AddListener(AddQuestion);
        m_BtnAddOption.onClick.AddListener(AddOption);
    }

    private void AddQuestion()
    {
        GameObject go = Instantiate(m_ItemQuestionOptionPref, transform);
        go.transform.SetSiblingIndex(m_ItemQuestionOptionPref.transform.GetSiblingIndex() + 1);
    }

    private void AddOption()
    {
        GameObject go = Instantiate(m_ItemOptionPref, transform);
        go.transform.SetSiblingIndex(m_ItemOptionPref.transform.GetSiblingIndex() + 1);
    }

    private void Validate()
    {
        if (m_IpfQuestion.text == string.Empty)
        {
            // Error showing
        }
    }

    public SNQuestionRequestDTO GetQuestionData()
    {
        var rowOptions = new List<SNRowOptionRequestDTO>();

        foreach (var ipf in m_IpfRowQuestionsList)
        {
            var rowOption = new SNRowOptionRequestDTO()
            {
                Order = m_IpfRowQuestionsList.IndexOf(ipf) + 1,
                Content = ipf.text
            };
            rowOptions.Add(rowOption);
        }
        
        var columnOptions = new List<SNColumnOptionRequestDTO>();

        foreach (var ipf in m_IpfColumnQuestionsList)
        {
            var columnOption = new SNColumnOptionRequestDTO()
            {
                Order = m_IpfColumnQuestionsList.IndexOf(ipf) + 1,
                Content = ipf.text
            };
            columnOptions.Add(columnOption);
        }

        var dto = new SNQuestionRequestDTO()
        {
            Order = GetOrder(),
            Type = "Likert",
            Title = m_IpfQuestion.text,
            LimitNumber = null,
            RowOptions = rowOptions,
            ColumnOptions = columnOptions
        };
        return dto;
    }
}
