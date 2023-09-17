using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainAccountPurchaseView : MonoBehaviour
{
    private Button m_BtnPnlPointInfo;
    private Button m_BtnPnlPurchasePoint;
    private Button m_BtnPnlPointExchange;
    private Button m_BtnPnlPointHistory;

    private GameObject m_BodyPointProfile;
    private GameObject m_BodyPurchasePoint;
    private GameObject m_BodyPointExchange;

    private GameObject m_PnlPurchaseHistory;
    private GameObject m_PnlExchangeHistory;
    private GameObject m_PnlPurchaseDetail;
    private GameObject m_PnlExchangeDetail;

    private List<GameObject> m_ListPnl;

    public void Init()
    {
        m_BtnPnlPointInfo = transform.Find("Viewport/Content/PnlPointInfo/TopBar").GetComponent<Button>();
        m_BtnPnlPurchasePoint = transform.Find("Viewport/Content/PnlPurchasePoint/TopBar").GetComponent<Button>();
        m_BtnPnlPointExchange = transform.Find("Viewport/Content/PnlPointExchange/TopBar").GetComponent<Button>();
        m_BtnPnlPointHistory = transform.Find("Viewport/Content/PnlPointHistory/TopBar").GetComponent<Button>();

        m_BodyPointProfile = transform.Find("Viewport/Content/PnlPointInfo/Body").gameObject;
        m_BodyPurchasePoint = transform.Find("Viewport/Content/PnlPurchasePoint/Body").gameObject;
        m_BodyPointExchange = transform.Find("Viewport/Content/PnlPointExchange/Body").gameObject;

        m_PnlPurchaseHistory = transform.Find("Viewport/Content/PnlPurchaseHistory").gameObject;
        m_PnlExchangeHistory = transform.Find("Viewport/Content/PnlPurchaseHistory").gameObject;
        m_PnlPurchaseDetail = transform.Find("Viewport/Content/PnlPurchaseDetail").gameObject;
        m_PnlExchangeDetail = transform.Find("Viewport/Content/PnlExchangeDetail").gameObject;

        m_ListPnl = new()
        {
            m_BodyPointProfile,
            m_BodyPurchasePoint,
            m_BodyPointExchange,
            m_PnlPurchaseHistory,
            m_PnlExchangeHistory,
            m_PnlPurchaseDetail,
            m_PnlExchangeDetail,
        };

        m_BtnPnlPointInfo.onClick.AddListener(OpenPointInfoDetail);
        m_BtnPnlPurchasePoint.onClick.AddListener(OpenPurchasePointDetail);
        m_BtnPnlPointExchange.onClick.AddListener(OpenPointExchangeDetail);
        m_BtnPnlPointHistory.onClick.AddListener(OpenPointHistoryDetail);
    }

    private void OpenPointInfoDetail()
    {
        ShowPnl(m_BodyPointProfile);
    }

    private void OpenPurchasePointDetail()
    {
        ShowPnl(m_BodyPurchasePoint);
    }

    private void OpenPointExchangeDetail()
    {
        ShowPnl(m_BodyPointExchange);
    }

    private void OpenPointHistoryDetail()
    {

    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl, true);
    }
}
