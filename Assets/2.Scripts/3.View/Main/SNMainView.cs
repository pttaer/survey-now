using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainView : MonoBehaviour
{
    private Button m_btnMenu;

    private GameObject m_PopupUpdateInfo;
    private GameObject m_PopupSuccessUpdatePassword;
    private GameObject m_PopupSuccessConnect;

    private SNMainProfileView m_MainProfileView;
    private SNMainAccountView m_MainAccountView;
    private SNMainAccountPurchaseView m_MainAccountPurchaseView;

    private List<GameObject> m_ListPnl;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_btnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();

        m_PopupUpdateInfo = transform.Find("Popups/PopupSuccessUpdateInfo").gameObject;
        m_PopupSuccessUpdatePassword = transform.Find("Popups/PopupSuccessUpdatePassword").gameObject;
        m_PopupSuccessConnect = transform.Find("Popups/PopupSuccessConnectThirdParties").gameObject;

        m_MainProfileView = transform.Find("Profile").GetComponent<SNMainProfileView>();
        m_MainAccountView = transform.Find("Account").GetComponent<SNMainAccountView>();
        m_MainAccountPurchaseView = transform.Find("AccountPurchase").GetComponent<SNMainAccountPurchaseView>();

        m_ListPnl = new()
        {
            m_MainProfileView.gameObject,
            m_MainAccountView.gameObject,
            m_MainAccountPurchaseView.gameObject
        };

        m_btnMenu.onClick.AddListener(OnClickOpenMenu);

        SNMainControl.Api.OnOpenProfileEvent += OpenProfile;
        SNMainControl.Api.OnOpenAccountlEvent += OpenAccount;
        SNMainControl.Api.OnOpenAccountPurchaseEvent += OpenAccountPurchase;

        m_MainProfileView.Init();
        m_MainAccountView.Init();
        m_MainAccountPurchaseView.Init();
    }

    private void OnDestroy()
    {
        SNMainControl.Api.OnOpenProfileEvent -= OpenProfile;
        SNMainControl.Api.OnOpenAccountlEvent -= OpenAccount;
        SNMainControl.Api.OnOpenAccountPurchaseEvent -= OpenAccountPurchase;
    }

    private void OpenProfile()
    {
        ShowPnl(m_MainProfileView.gameObject);
    }

    private void OpenAccount()
    {
        ShowPnl(m_MainAccountView.gameObject);
    }
    
    private void OpenAccountPurchase()
    {
        ShowPnl(m_MainAccountPurchaseView.gameObject);
    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl);
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
