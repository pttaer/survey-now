using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNBundleNoticeView : MonoBehaviour
{
    private Text m_TxtCurrentBalance;
    private Text m_TxtRemaindingAmount;

    private Button m_BtnSubmit;

    public void Init(string remaindingAmount)
    {
        m_TxtCurrentBalance = transform.Find("GroupTitle/TxtAmount").GetComponent<Text>();
        m_TxtRemaindingAmount = transform.Find("GroupCalculate/TxtAmount").GetComponent<Text>();
        m_BtnSubmit = transform.Find("BtnSubmit").GetComponent<Button>();

        m_BtnSubmit.onClick.AddListener(OnSubmit);

        DefaultValue(remaindingAmount);
    }

    private void DefaultValue(string remaindingAmount)
    {
        m_TxtCurrentBalance.text = SNModel.Api.CurrentUser.Point.ToString();
        m_TxtRemaindingAmount.text = remaindingAmount;
    }

    private void OnSubmit()
    {
        SNMenuControl.Api.OnOpenPoints();
        DOVirtual.DelayedCall(0.2f, () => SNMainControl.Api.OpenAccountPurchase());
    }
}
