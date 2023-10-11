using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SNDoSurveyDTO;

public class SNQuestionRatingView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private GameObject m_ToggleItemPref;
    private ToggleGroup m_TglGroup;

    private List<Toggle> m_TglViewList;
    private int m_QuestionId;

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;
        m_QuestionId = data.id;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_ToggleItemPref = transform.Find("ToggleGr/ToggleItem").gameObject;
        m_TglGroup = transform.Find("ToggleGr").GetComponent<ToggleGroup>();

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);

        m_TglViewList = new();

        for (int i = 0; i < data.limitNumber; i++)
        {
            GenerateQuestionChoices();
        }

        m_TglGroup.allowSwitchOff = true;
        m_TglGroup.SetAllTogglesOff();
    }

    private void GenerateQuestionChoices()
    {
        GameObject go = Instantiate(m_ToggleItemPref, m_TglGroup.transform);
        Toggle tgl = go.GetComponent<Toggle>();
        m_TglViewList.Add(tgl);
        go.transform.Find("Background/TxtRate").GetComponent<Text>().text = m_TglViewList.Count.ToString();
        tgl.group = m_TglGroup;
        go.SetActive(true);
    }

    public override AnswerDTO GetAnswer()
    {
        string rate = m_TglGroup?.ActiveToggles()?.ToList()?.FirstOrDefault()?.transform.Find("Background/TxtRate").GetComponent<Text>().text ?? "";
        return new AnswerDTO()
        {
            QuestionId = m_QuestionId,
            Content = null,
            RateNumber = int.Parse(rate),
            AnswerOptions = new List<AnswerOptionDTO>()
        };
    }

    public override bool Validate()
    {
        Debug.Log("GOOOO");
        string rate = m_TglGroup?.ActiveToggles()?.ToList()?.FirstOrDefault()?.transform.Find("Background/TxtRate").GetComponent<Text>().text ?? "";
        return !string.IsNullOrEmpty(rate);
    }
}
