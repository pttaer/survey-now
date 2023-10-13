using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainAccountPurchaseView : MonoBehaviour
{
    private Button m_BtnPnlPointInfo;
    private Button m_BtnPnlPurchasePoint;
    private Button m_BtnPnlPointExchange;
    private Button m_BtnPnlPointHistory;

    private Button m_BtnPnlPurchaseHistory;
    private Button m_BtnPnlExchangeHistory;

    private Button m_BtnPurchase;

    private GameObject m_BodyPointProfile;
    private GameObject m_BodyPurchasePoint;
    private GameObject m_BodyPointExchange;
    private GameObject m_BodyPurchaseHistory;
    private GameObject m_BodyExchangeHistory;
    private GameObject m_BodyPointHistory;

    private GameObject m_PnlPointInfo;
    private GameObject m_PnlPurchasePoint;
    private GameObject m_PnlPointExchange;
    private GameObject m_PnlPointHistory;

    private GameObject m_PnlPurchaseHistory;
    private GameObject m_PnlExchangeHistory;
    private GameObject m_PnlPurchaseDetail;
    private GameObject m_PnlExchangeDetail;

    private GameObject m_PrefRecord;

    private List<GameObject> m_ListPnl;
    private List<GameObject> m_ListNotPnlHistory;
    private List<GameObject> m_ListPnlHistory;

    private Text m_PointsBalance;
    private Text m_PointsConfirm;
    private Text m_PointsToCurrency;
    private InputField m_PointsAmountToPurchase;

    public void Init()
    {
        m_BtnPnlPointInfo = transform.Find("Viewport/Content/PnlPointInfo/TopBar").GetComponent<Button>();
        m_BtnPnlPurchasePoint = transform.Find("Viewport/Content/PnlPurchasePoint/TopBar").GetComponent<Button>();
        m_BtnPnlPointExchange = transform.Find("Viewport/Content/PnlPointExchange/TopBar").GetComponent<Button>();
        m_BtnPnlPointHistory = transform.Find("Viewport/Content/PnlPointHistory/TopBar").GetComponent<Button>();

        m_BtnPnlPurchaseHistory = transform.Find("Viewport/Content/PnlPurchaseHistory/TopBar").GetComponent<Button>();
        m_BtnPnlExchangeHistory = transform.Find("Viewport/Content/PnlExchangeHistory/TopBar").GetComponent<Button>();

        m_BodyPointProfile = transform.Find("Viewport/Content/PnlPointInfo/Body").gameObject;
        m_BodyPurchasePoint = transform.Find("Viewport/Content/PnlPurchasePoint/Body").gameObject;
        m_BodyPointExchange = transform.Find("Viewport/Content/PnlPointExchange/Body").gameObject;
        m_BodyPointHistory = transform.Find("Viewport/Content/PnlPointHistory/Body").gameObject;

        m_BtnPurchase = m_BodyPurchasePoint.transform.Find("BtnGroup/BtnPurchase").GetComponent<Button>();

        m_BodyPurchaseHistory = transform.Find("Viewport/Content/PnlPurchaseHistory/Body").gameObject;
        m_BodyExchangeHistory = transform.Find("Viewport/Content/PnlExchangeHistory/Body").gameObject;

        m_PnlPointInfo = transform.Find("Viewport/Content/PnlPointInfo").gameObject;
        m_PnlPurchasePoint = transform.Find("Viewport/Content/PnlPurchasePoint").gameObject;
        m_PnlPointExchange = transform.Find("Viewport/Content/PnlPointExchange").gameObject;
        m_PnlPointHistory = transform.Find("Viewport/Content/PnlPointHistory").gameObject;

        m_PnlPurchaseHistory = transform.Find("Viewport/Content/PnlPurchaseHistory").gameObject;
        m_PnlExchangeHistory = transform.Find("Viewport/Content/PnlExchangeHistory").gameObject;
        m_PnlPurchaseDetail = transform.Find("Viewport/Content/PnlPurchaseDetail").gameObject;
        m_PnlExchangeDetail = transform.Find("Viewport/Content/PnlExchangeDetail").gameObject;

        m_PrefRecord = transform.Find("Viewport/SpawnItems/Record").gameObject;

        m_PointsBalance = m_BodyPointProfile.transform.Find("RightSide/TxtLabel_1").GetComponent<Text>();
        m_PointsConfirm = m_BodyPurchasePoint.transform.Find("Body_1/RightSide/TxtLabel").GetComponent<Text>();
        m_PointsToCurrency = m_BodyPurchasePoint.transform.Find("Body_1/RightSide/TxtLabel_2").GetComponent<Text>();
        m_PointsAmountToPurchase = m_BodyPurchasePoint.transform.Find("Body/RightSide/IpfPointAmount").GetComponent<InputField>();

        // 3 bodies of main panels and 4 sub panels
        m_ListPnl = new()
        {
            m_BodyPointProfile,
            m_BodyPurchasePoint,
            m_BodyPointExchange,
            m_PnlPurchaseHistory,
            m_PnlExchangeHistory,
            m_PnlPurchaseDetail,
            m_PnlExchangeDetail,
            m_BodyPointHistory
        };

        // The 4 main panels
        m_ListNotPnlHistory = new()
        {
            m_PnlPointInfo,
            m_PnlPurchasePoint,
            m_PnlPointExchange,
            m_PnlPointHistory
        };

        // 2 History panel
        m_ListPnlHistory = new()
        {
            m_BodyPurchaseHistory,
            m_BodyExchangeHistory
        };

        m_BtnPnlPointInfo.onClick.AddListener(OpenPointInfoDetail);
        m_BtnPnlPurchasePoint.onClick.AddListener(OpenPurchasePointDetail);
        m_BtnPnlPointExchange.onClick.AddListener(OpenPointExchangeDetail);
        m_BtnPnlPointHistory.onClick.AddListener(OpenPointHistoryDetail);
        m_BtnPnlPurchaseHistory.onClick.AddListener(OpenPnlPurchaseHistory);
        m_BtnPnlExchangeHistory.onClick.AddListener(OpenPnlExchangeHistory);

        m_PointsAmountToPurchase.onValueChanged.AddListener(OnUpdatePointsDisplay);

        m_BtnPurchase.onClick.AddListener(PurchasePoints);

        SNMainControl.Api.OnCallHistoryRecorDetailEvent += OpenHistoryRecord;
        SNDeeplinkControl.Api.onReturnFromMomo += ProcessMomoReturnData;

        m_PnlPointHistory.GetComponent<SNPointHistory>().Init();

        DefaultValue();
    }

    private void DefaultValue()
    {
        m_PointsBalance.text = SNModel.Api.CurrentUser.Point.ToString();
    }

    private void OnDestroy()
    {
        SNMainControl.Api.OnCallHistoryRecorDetailEvent -= OpenHistoryRecord;
        SNDeeplinkControl.Api.onReturnFromMomo -= ProcessMomoReturnData;
    }

    private void OpenHistoryRecord(SNHistoryRecordType type, string date, string points)
    {
        if (type == SNHistoryRecordType.Purchase)
        {
            // Handle null
            if (date != null && points != null)
            {
                SNControl.Api.OpenPanel(m_PnlPurchaseDetail, m_ListNotPnlHistory, true);
                SNControl.Api.OpenPanel(m_PnlPurchaseDetail, m_ListPnlHistory, true);
            }
        }
        else
        {
            // Handle null
            if (date != null && points != null)
            {
                SNControl.Api.OpenPanel(m_PnlExchangeDetail, m_ListNotPnlHistory, true);
                SNControl.Api.OpenPanel(m_PnlExchangeDetail, m_ListPnlHistory, true);
            }
        }
    }

    private void OpenPointInfoDetail()
    {
        ShowPnlNotHistory(m_BodyPointProfile);
    }

    private void OpenPurchasePointDetail()
    {
        ShowPnlNotHistory(m_BodyPurchasePoint);
    }

    private void OpenPointExchangeDetail()
    {
        ShowPnlNotHistory(m_BodyPointExchange);
    }

    private void OpenPnlPurchaseHistory()
    {
        SNControl.Api.OpenPanel(m_BodyPurchaseHistory, m_ListPnlHistory, true);
    }

    private void OpenPnlExchangeHistory()
    {
        SNControl.Api.OpenPanel(m_BodyExchangeHistory, m_ListPnlHistory, true);
    }

    private void OpenPointHistoryDetail()
    {
        //SNControl.Api.OpenManyPanel(m_ListNotPnlHistory, new List<GameObject> { m_PnlPurchaseHistory, m_PnlExchangeHistory });
        ShowPnlNotHistory(m_BodyPointHistory);
    }

    private void ShowPnlNotHistory(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl, true);
    }

    private void OnUpdatePointsDisplay(string points)
    {
        m_PointsConfirm.text = points;
        m_PointsToCurrency.text = ((int.Parse(points) * 10)).ToString() + " VND";
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
            UpdateCurrentPoints(data.pointAmount);
        }));
    }

    private void UpdateCurrentPoints(int pointAmount)
    {
        int.TryParse(m_PointsBalance.text, out int result);

        m_PointsBalance.text = (result + pointAmount).ToString();
    }


}
