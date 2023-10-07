using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyQuestionMultipleView : SNSurveyQuestionBaseView
{
    private InputField m_IpfQuestion;
    private Button m_BtnAddOption;
    private GameObject m_ItemOptionPref;

    private List<InputField> m_IpfQuestionsList;

    public override void Init()
    {
        m_IpfQuestion = transform.Find("IpfQuestion").GetComponent<InputField>();
        m_BtnAddOption = transform.Find("BtnAddOption").GetComponent<Button>();
        m_ItemOptionPref = transform.Find("Option").gameObject;

        m_IpfQuestionsList = new();

        m_BtnAddOption.onClick.AddListener(AddOption);
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

        foreach (var ipf in m_IpfQuestionsList)
        {
            var rowOption = new SNRowOptionRequestDTO()
            {
                Order = m_IpfQuestionsList.IndexOf(ipf) + 1,
                Content = ipf.text
            };
            rowOptions.Add(rowOption);
        }

        var dto = new SNQuestionRequestDTO()
        {
            Order = GetOrder(),
            Type = "CheckBox",
            Title = m_IpfQuestion.text,
            MultipleOptionType = "NoLimit",
            LimitNumber = null,
            RowOptions = rowOptions,
            ColumnOptions = new List<SNColumnOptionRequestDTO>()
        };
        return dto;
    }
}
