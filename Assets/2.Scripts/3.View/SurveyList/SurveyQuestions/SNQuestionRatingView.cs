using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNQuestionRatingView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private GameObject m_ToggleItemPref;
    private ToggleGroup m_TglGroup;

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_ToggleItemPref = transform.Find("ToggleGr/ToggleItem").gameObject;
        m_TglGroup = transform.Find("ToggleGr").GetComponent<ToggleGroup>();

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);
    }
}
