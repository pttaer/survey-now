using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNLikertQuestionItemView : MonoBehaviour
{
    private Button m_BtnQuestion;
    private GameObject m_Options;
    private ToggleGroup m_TglGroup;
    private GameObject m_ToggleItemPref;
    private Text m_TxtTitle;

    private List<SNQuestionToggleItemView> m_ItemViewList;

    public void Init(string title, List<SNSectionQuestionColumnOptionDTO> columnOptions)
    {
        m_BtnQuestion = transform.Find("BtnQuestion").GetComponent<Button>();
        m_Options = transform.Find("Options").gameObject;
        m_TglGroup = transform.Find("Options").GetComponent<ToggleGroup>();
        m_ToggleItemPref = transform.Find("Options/ToggleItem").gameObject;
        m_TxtTitle = transform.Find("BtnQuestion/TxtTitle").GetComponent<Text>();

        m_BtnQuestion.onClick.AddListener(OnClickQuestion);

        m_TxtTitle.text = title;
        m_ItemViewList = new List<SNQuestionToggleItemView>();

        foreach (var option in columnOptions)
        {
            if (option.content != null)
            {
                GenerateQuestionChoices(option);
            }
        }
        m_TglGroup.SetAllTogglesOff();
    }

    private void OnClickQuestion()
    {
        // Turn the others off
        if (!m_Options.activeSelf) SNSurveyLikertQuestionControl.Api.OpenLikertOption();
        m_Options.SetActive(!m_Options.activeSelf);
    }

    private void GenerateQuestionChoices(SNSectionQuestionColumnOptionDTO data)
    {
        GameObject go = Instantiate(m_ToggleItemPref, m_Options.transform);
        SNQuestionToggleItemView view = go.GetComponent<SNQuestionToggleItemView>();
        Toggle tgl = go.GetComponent<Toggle>();
        tgl.group = m_TglGroup;
        m_ItemViewList.Add(view);
        view.Init(data);
    }

    public void TurnOptionOff()
    {
        m_Options.SetActive(false);
    }
}
