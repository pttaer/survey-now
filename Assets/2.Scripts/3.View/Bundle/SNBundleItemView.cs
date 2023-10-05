using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNBundleItemView : MonoBehaviour
{
    private Button m_BtnBuy;

    public void Init()
    {
        m_BtnBuy = transform.Find("BtnBuy").GetComponent<Button>();

        m_BtnBuy.onClick.AddListener(OpenPayment);
    }

    private void OpenPayment()
    {
        SNBundleControl.Api.OpenPayment();
    }
}
