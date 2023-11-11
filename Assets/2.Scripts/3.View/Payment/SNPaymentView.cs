using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNPaymentView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnCancel;
    private Button m_BtnPurchase;

    private Button m_BtnSelectMomo;
    private Button m_BtnSelectVnPay;

    private Text m_PointsToCurrency;
    private InputField m_PointsAmountToPurchase;
    private InputField m_PhoneNumber;

    [SerializeField] string m_TxtSuccess;
    [SerializeField] string m_TxtMomoSuccessRedirectToPoints;

    private bool m_IsPaymentMethodSelected;
    private bool m_IsPurchase;

    private string m_PaymentMethod;

    public void Start()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnCancel = transform.Find("Body/BtnGroup/BtnCancel").GetComponent<Button>();
        m_BtnPurchase = transform.Find("Body/BtnGroup/BtnPurchase").GetComponent<Button>();

        m_BtnSelectMomo = transform.Find("Body/BtnGroupPurchase/BtnMomo").GetComponent<Button>();
        m_BtnSelectVnPay = transform.Find("Body/BtnGroupPurchase/BtnVnPay").GetComponent<Button>();

        m_PointsToCurrency = transform.Find("Body/Body_1/RightSide/TxtLabel_2").GetComponent<Text>();
        m_PointsAmountToPurchase = transform.Find("Body/Body/RightSide/IpfPointAmount").GetComponent<InputField>();
        m_PhoneNumber = transform.Find("Body/Body/RightSide/IpfPointAmount_1").GetComponent<InputField>();

        m_PointsAmountToPurchase.onValueChanged.AddListener(OnUpdatePointsDisplay);

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnCancel.onClick.AddListener(BackToPoints);
        m_BtnPurchase.onClick.AddListener(PurchasePoints);
        m_BtnSelectMomo.onClick.AddListener(delegate { OnSetPaymentMethod(m_BtnSelectMomo, m_BtnSelectVnPay); });
        m_BtnSelectVnPay.onClick.AddListener(delegate { OnSetPaymentMethod(m_BtnSelectVnPay, m_BtnSelectMomo); });

        SNDeeplinkControl.Api.onReturnFromMomo += ProcessMomoReturnData;

        DefaultValues();
    }

    private void OnDestroy()
    {
        SNDeeplinkControl.Api.onReturnFromMomo -= ProcessMomoReturnData;
    }

    private void DefaultValues()
    {
        m_BtnPurchase.gameObject.SetActive(m_IsPaymentMethodSelected);

        m_IsPurchase = SNMainControl.Api.IsPurchase;
    }

    private void OnSetPaymentMethod(Button btnSelect, Button btnOther)
    {
        GameObject borderSelected = btnSelect.transform.Find("Border").gameObject;
        GameObject borderOther = btnOther.transform.Find("Border").gameObject;

        if (!borderSelected.activeSelf)
        {
            borderOther.SetActive(borderSelected.activeSelf);
            borderSelected.SetActive(!borderSelected.activeSelf);
        }
        else
        {
            borderSelected.SetActive(borderSelected.activeSelf);
        }

        m_PaymentMethod = btnSelect.name.Replace("Btn", "");

        m_IsPaymentMethodSelected = borderSelected.activeSelf;
        m_BtnPurchase.gameObject.SetActive(m_IsPaymentMethodSelected);
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
        if (m_IsPurchase)
        {
            StartCoroutine(SNApiControl.Api.PurchasePoints(int.Parse(m_PointsAmountToPurchase.text), m_PaymentMethod, (data) =>
            {
                SNControl.Api.ShowFAMPopup(title: data.message, content: "Vui lòng chuyển vào tài khoản: " + data.destinationAccount + "\n" + data.description, btnConfirmText: "OK", btnElseText: "CANCEL", onConfirm: () =>
                {
                    DOVirtual.DelayedCall(0.5f, () => SNMainControl.Api.OpenAccountPurchase());
                });
            }));
        }
        else
        {
            SendExchagnePointRequest();
        }
    }

    private void ProcessMomoReturnData(SNMomoRedirect momoData, string momoParam)
    {
        StartCoroutine(SNApiControl.Api.MomoReturn(momoData, momoParam, (data) =>
        {
            SNModel.Api.CurrentUser.Point += data.pointAmount;
            SNControl.Api.ShowFAMPopup(m_TxtSuccess, m_TxtMomoSuccessRedirectToPoints, "OK", "NotShow", () =>
            {
                SNMenuControl.Api.OnOpenPoints();
            }, onExit: () => SNMenuControl.Api.OnOpenPoints());
        }));
    }

    private void SendExchagnePointRequest()
    {
        SNRedeemRequestDTO data = new()
        {
            UserId = (int)SNModel.Api.CurrentUser.Id,
            PaymentMethod = m_PaymentMethod,
            PointAmount = int.Parse(m_PointsAmountToPurchase.text),
            MomoAccount = m_PhoneNumber.text,
        };

        StartCoroutine(SNApiControl.Api.PostData(SNConstant.REDEEM_MONEY, data, (response) =>
        {
            SNControl.Api.ShowFAMPopup(title: "Đã gửi yêu cầu thành công", content: (string)response["message"], btnConfirmText: "OK", btnElseText: "CANCEL", onConfirm: () =>
            {
                DOVirtual.DelayedCall(0.5f, () => SNMainControl.Api.OpenAccountPurchase());
            });
        }));
    }
}
