using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SNDoSurveyDTO;

public class SNQuestionCustomView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private InputField m_IpfAnswer;
    private int m_QuestionId;

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;
        m_QuestionId = data.id;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_IpfAnswer = transform.Find("ToggleGr/IpfAnswer").GetComponent<InputField>();

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);
    }

    public override AnswerDTO GetAnswer()
    {
        return new AnswerDTO()
        {
            QuestionId = m_QuestionId,
            Content = m_IpfAnswer.text,
            RateNumber = null,
            AnswerOptions = new List<AnswerOptionDTO>()
        };
    }

    public override bool Validate()
    {
        Debug.Log("GOOOO");
        string answer = m_IpfAnswer.text;
        return !string.IsNullOrEmpty(answer);
    }

    public override void SetAnswer(SNSurveyAnswerDTO.AnswerResponseDTO answer)
    {
        m_IpfAnswer.text = answer.content;
    }
}
