using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNBundleItemView : MonoBehaviour
{
    private Text m_TxtTitle;
    private Text m_TxtPackType;
    private Text m_TxtBenefits;

    private Button m_BtnBuy;

    public void Init(string title, string packType, string[] benefits)
    {
        m_BtnBuy = transform.Find("BtnBuy").GetComponent<Button>();
        m_TxtTitle = transform.Find("TxtTitle").GetComponent<Text>();
        m_TxtPackType = transform.Find("GroupDescription/TxtDescription1").GetComponent<Text>();
        m_TxtBenefits = transform.Find("GroupDescription/TxtDescription1 (1)").GetComponent<Text>();

        DefaultValue(title, packType, benefits);

        m_BtnBuy.onClick.AddListener(OpenPopupForm);
    }

    private void DefaultValue(string title, string packType, string[] benefits)
    {
        m_TxtTitle.text = title;
        m_TxtPackType.text = packType;

        foreach (string item in benefits)
        {
            Instantiate(m_TxtBenefits, m_TxtBenefits.transform.parent);
            m_TxtBenefits.text = item;
            m_TxtBenefits.gameObject.SetActive(true);
        }
    }

    private void OpenPopupForm()
    {
        SNBundleControl.Api.OnOpenPopupForm(m_TxtPackType.text);
    }
}
