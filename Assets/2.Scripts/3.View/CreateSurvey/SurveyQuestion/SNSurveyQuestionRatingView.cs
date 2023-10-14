using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyQuestionRatingView : SNSurveyQuestionBaseView
{
    private InputField m_IpfQuestion;
    private InputField m_IpfLimitNumber; // Will always be integer

    public override void Init()
    {
        m_IpfQuestion = transform.Find("IpfQuestion").GetComponent<InputField>();
        m_IpfLimitNumber = transform.Find("IpfLimitNumber").GetComponent<InputField>();
    }

    private void Validate()
    {
        if (m_IpfQuestion.text == string.Empty || m_IpfLimitNumber.text == string.Empty)
        {
            // Error showing
        }
    }

    public override SNSectionQuestionRequestDTO GetQuestionData()
    {
        var dto = new SNSectionQuestionRequestDTO()
        {
            Order = GetOrder(),
            Type = "Rating",
            IsRequired = GetRequire(),
            Title = m_IpfQuestion.text,
            LimitNumber = string.IsNullOrEmpty(m_IpfLimitNumber.text) ? 5 : int.Parse(m_IpfLimitNumber.text),
            RowOptions = new List<SNRowOptionRequestDTO>(),
            ColumnOptions = new List<SNColumnOptionRequestDTO>()
        };
        return dto;
    }
}
