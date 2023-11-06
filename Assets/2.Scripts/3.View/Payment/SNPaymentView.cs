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
    private Button m_BtnSelectManual;

    private Text m_PointsToCurrency;
    private InputField m_PointsAmountToPurchase;

    [SerializeField] string m_TxtSuccess;
    [SerializeField] string m_TxtMomoSuccessRedirectToPoints;

    private bool m_IsPaymentMethodSelected;
    private bool m_IsMomoPayment;
    private bool m_IsPurchase;

    public void Start()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnCancel = transform.Find("Body/BtnGroup/BtnCancel").GetComponent<Button>();
        m_BtnPurchase = transform.Find("Body/BtnGroup/BtnPurchase").GetComponent<Button>();

        m_BtnSelectMomo = transform.Find("Body/BtnGroupPurchase/BtnMomo").GetComponent<Button>();
        m_BtnSelectManual = transform.Find("Body/BtnGroupPurchase/BtnManual").GetComponent<Button>();

        m_PointsToCurrency = transform.Find("Body/Body_1/RightSide/TxtLabel_2").GetComponent<Text>();
        m_PointsAmountToPurchase = transform.Find("Body/Body/RightSide/IpfPointAmount").GetComponent<InputField>();

        m_PointsAmountToPurchase.onValueChanged.AddListener(OnUpdatePointsDisplay);

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnCancel.onClick.AddListener(BackToPoints);
        m_BtnPurchase.onClick.AddListener(PurchasePoints);
        m_BtnSelectMomo.onClick.AddListener(delegate { OnSetPaymentMethod(m_BtnSelectMomo, m_BtnSelectManual); });
        m_BtnSelectManual.onClick.AddListener(delegate { OnSetPaymentMethod(m_BtnSelectManual, m_BtnSelectMomo); });

        SNDeeplinkControl.Api.onReturnFromMomo += ProcessMomoReturnData;
        SNMainControl.Api.OnSetPointAction += SetPointAction;

        DefaultValues();
    }

    private void SetPointAction(bool isPurchase)
    {
        m_IsPurchase = isPurchase;
    }

    private void DefaultValues()
    {
        m_BtnPurchase.gameObject.SetActive(m_IsPaymentMethodSelected);
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

        m_IsMomoPayment = btnSelect.name == "BtnMomo";

        m_IsPaymentMethodSelected = borderSelected.activeSelf;
        m_BtnPurchase.gameObject.SetActive(m_IsPaymentMethodSelected);
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
        if (m_IsPurchase)
        {
            StartCoroutine(SNApiControl.Api.PurchasePoints(m_IsMomoPayment, int.Parse(m_PointsAmountToPurchase.text), (momoUrl) =>
            {
                Debug.Log(momoUrl);
                Application.OpenURL(momoUrl);
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
        if (string.IsNullOrEmpty(SNModel.Api.CurrentUser?.PhoneNumber))
        {
            SNControl.Api.ShowFAMPopup(title: "No number.", content: "Please update phone number and try again.", btnConfirmText: "OK", btnElseText: "CANCEL", onConfirm: () =>
            {
                SNMainControl.Api.OpenProfile();
            });
        }
        else
        {
            SNRedeemRequestDTO data = new()
            {
                UserId = (int)SNModel.Api.CurrentUser.Id,
                PaymentMethod = "Momo",
                PointAmount = int.Parse(m_PointsAmountToPurchase.text),
                MomoAccount = SNModel.Api.CurrentUser?.PhoneNumber,
            };

            StartCoroutine(SNApiControl.Api.PostData(SNConstant.REDEEM_MONEY, data, () =>
            {

            }));
        }
    }
}
