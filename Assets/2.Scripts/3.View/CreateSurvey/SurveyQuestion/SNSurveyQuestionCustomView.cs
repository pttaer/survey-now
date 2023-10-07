using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyQuestionCustomView : SNSurveyQuestionBaseView
{
    private InputField m_IpfQuestion;

    public override void Init()
    {
        m_IpfQuestion = transform.Find("IpfQuestion").GetComponent<InputField>();
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
        var dto = new SNQuestionRequestDTO()
        {
            Order = GetOrder(),
            Type = "Text",
            LimitNumber = null,
            Title = m_IpfQuestion.text,
            RowOptions = new List<SNRowOptionRequestDTO>(),
            ColumnOptions = new List<SNColumnOptionRequestDTO>()
        };
        return dto;
    }
}
