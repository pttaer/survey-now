using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SNPointRedeem : MonoBehaviour
{
    private Text m_PointsBalance;
    private Text m_PointsToMoneyBalance;

    private Button m_BtnMenu;
    private Button m_BtnCancel;
    private Button m_BtnRedeem;

    private Text m_PointsToCurrency;
    private InputField m_PointsAmountToPurchase;

    [SerializeField] string m_TxtPopupTitle;
    [SerializeField] string m_TxtPopupContent;

    public void Init()
    {
        //m_PointsBalance = transform.Find("Body/Body_1/RightSide/TxtLabel").GetComponent<Text>();
        //m_PointsToMoneyBalance = transform.Find("Body/Body_1/RightSide/TxtLabel_2").GetComponent<Text>();

        m_BtnRedeem = transform.Find("Body/BtnGroup/BtnExchange").GetComponent<Button>();

        m_PointsToCurrency = transform.Find("Body/Body_1/RightSide/TxtLabel_2").GetComponent<Text>();
        m_PointsAmountToPurchase = transform.Find("Body/Body/RightSide/IpfFamilyName").GetComponent<InputField>();

        m_PointsAmountToPurchase.onValueChanged.AddListener(OnUpdatePointsDisplay);

        //m_BtnCancel.onClick.AddListener(BackToPoints);
        //m_BtnRedeem.onClick.AddListener(SendExchagnePointRequest);

        DefaultValue();
    }

    private void DefaultValue()
    {
        //m_PointsBalance.text = SNModel.Api.CurrentUser.Point.ToString() + "VND";
        //m_PointsToMoneyBalance.text = SNModel.Api.CurrentUser.Point.ToString() + "000 VND";
    }
    private void OnUpdatePointsDisplay(string points)
    {
        m_PointsToCurrency.text = ((int.Parse(points) * 1000)).ToString() + " VND";
    }

    
}
