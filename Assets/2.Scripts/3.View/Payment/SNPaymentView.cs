using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNPaymentView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnCancel;
    private Button m_BtnPurchase;

    private Text m_PointsToCurrency;
    private InputField m_PointsAmountToPurchase;

    [SerializeField] string m_TxtSuccess;
    [SerializeField] string m_TxtMomoSuccessRedirectToPoints;

    public void Start()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnCancel = transform.Find("Body/BtnGroup/BtnCancel").GetComponent<Button>();
        m_BtnPurchase = transform.Find("Body/BtnGroup/BtnPurchase").GetComponent<Button>();

        m_PointsToCurrency = transform.Find("Body/Body_1/RightSide/TxtLabel_2").GetComponent<Text>();
        m_PointsAmountToPurchase = transform.Find("Body/Body/RightSide/IpfPointAmount").GetComponent<InputField>();

        m_PointsAmountToPurchase.onValueChanged.AddListener(OnUpdatePointsDisplay);

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnCancel.onClick.AddListener(BackToPoints);
        m_BtnPurchase.onClick.AddListener(PurchasePoints);

        SNDeeplinkControl.Api.onReturnFromMomo += ProcessMomoReturnData;
    }

    private void OnDestroy()
    {
        SNDeeplinkControl.Api.onReturnFromMomo -= ProcessMomoReturnData;
    }
    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
    private void BackToPoints()
    {
        SNMenuControl.Api.OnOpenPoints();
    }

    private void OnUpdatePointsDisplay(string points)
    {
        m_PointsToCurrency.text = ((int.Parse(points) * 1000)).ToString() + " VND";
    }

    private void PurchasePoints()
    {
        StartCoroutine(SNApiControl.Api.PurchasePoints(int.Parse(m_PointsAmountToPurchase.text), (momoUrl) =>
        {
            Debug.Log(momoUrl);
            Application.OpenURL(momoUrl);
        }));
    }

    private void ProcessMomoReturnData(SNMomoRedirect momoData, string momoParam)
    {
        StartCoroutine(SNApiControl.Api.MomoReturn(momoData, momoParam, (data) =>
        {
            SNModel.Api.CurrentUser.Point += data.pointAmount;
            SNControl.Api.ShowFAMPopup(m_TxtSuccess, m_TxtMomoSuccessRedirectToPoints, "Ok", "NotShow", () =>
            {
                SNMenuControl.Api.OnOpenPoints();
            }, onExit: () => SNMenuControl.Api.OnOpenPoints());
        }));
    }
}
