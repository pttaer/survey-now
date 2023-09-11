using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNLoginView : MonoBehaviour
{
    private Button m_BtnNoAccount;
    private Button m_BtnHaveAccount;

    private Button m_BtnForgotPassword;
    private Button m_BtnSendOTP;
    private Button m_BtnLogin;
    private Button m_BtnGoToLogin;

    private GameObject m_PnlLogin;
    private GameObject m_PnlForgotPassword;

    private GameObject m_PnlRegister1;
    private GameObject m_PnlRegister2;
    private GameObject m_PnlRegister3;
    private GameObject m_PnlRegister4;
    private GameObject m_PnlRegister5;

    private Button m_BtnNextRegister1;
    private Button m_BtnNextRegister2;
    private Button m_BtnNextRegister3;
    private Button m_BtnNextRegister4;
    private Button m_BtnNextRegister5;

    private Button m_BtnPreviousRegister2;
    private Button m_BtnPreviousRegister3;
    private Button m_BtnPreviousRegister4;
    private Button m_BtnPreviousRegister5;

    // no ref var
    private List<GameObject> m_PnlRegisterList;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform body = transform.Find("Body");

        m_BtnNoAccount = body.Find("BtnNoAccount").GetComponent<Button>();
        m_BtnHaveAccount = body.Find("BtnHaveAccount").GetComponent<Button>();

        m_BtnForgotPassword = body.Find("Content/PnlLogin/BtnLogin/BtnForgotPassword").GetComponent<Button>();
        m_BtnSendOTP = body.Find("Content/PnlForgotPassword/BtnSendOTP").GetComponent<Button>();
        m_BtnLogin = body.Find("Content/PnlLogin/BtnLogin").GetComponent<Button>();
        m_BtnGoToLogin = body.Find("Content/PnlForgotPassword/BtnSendOTP/BtnLogin").GetComponent<Button>();

        m_PnlLogin = body.Find("Content/PnlLogin").gameObject;
        m_PnlForgotPassword = body.Find("Content/PnlForgotPassword").gameObject;

        m_PnlRegister1 = body.Find("Content/PnlRegister1").gameObject;
        m_PnlRegister2 = body.Find("Content/PnlRegister2").gameObject;
        m_PnlRegister3 = body.Find("Content/PnlRegister3").gameObject;
        m_PnlRegister4 = body.Find("Content/PnlRegister4").gameObject;
        m_PnlRegister5 = body.Find("Content/PnlRegister5").gameObject;

        m_BtnNextRegister1 = body.Find("Content/PnlRegister1/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister2 = body.Find("Content/PnlRegister2/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister3 = body.Find("Content/PnlRegister3/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister4 = body.Find("Content/PnlRegister4/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister5 = body.Find("Content/PnlRegister5/BtnGroup/BtnFinish").GetComponent<Button>();

        m_BtnPreviousRegister2 = body.Find("Content/PnlRegister2/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister3 = body.Find("Content/PnlRegister3/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister4 = body.Find("Content/PnlRegister4/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister5 = body.Find("Content/PnlRegister5/BtnGroup/BtnPrevious").GetComponent<Button>();

        m_PnlRegisterList = new List<GameObject>
        {
            m_PnlRegister1,
            m_PnlRegister2,
            m_PnlRegister3,
            m_PnlRegister4,
            m_PnlRegister5,
        };

        // Register OnClick
        m_BtnNoAccount.onClick.AddListener(() =>
        {
            SetPnlOn(m_PnlRegister1);
            SetBtnAccountExist(false);
        });
        m_BtnHaveAccount.onClick.AddListener(() =>
        {
            SetPnlOn(m_PnlLogin);
            SetBtnAccountExist(true);
        });

        m_BtnForgotPassword.onClick.AddListener(() => SetPnlOn(m_PnlForgotPassword));
        m_BtnSendOTP.onClick.AddListener(() => SetPnlOn(m_PnlForgotPassword));
        m_BtnLogin.onClick.AddListener(() => SetPnlOn(m_PnlForgotPassword));
        m_BtnGoToLogin.onClick.AddListener(() => SetPnlOn(m_PnlLogin));

        m_BtnNextRegister1.onClick.AddListener(() => SetPnlOn(m_PnlRegister2));
        m_BtnNextRegister2.onClick.AddListener(() => SetPnlOn(m_PnlRegister3));
        m_BtnNextRegister3.onClick.AddListener(() => SetPnlOn(m_PnlRegister4));
        m_BtnNextRegister4.onClick.AddListener(() => SetPnlOn(m_PnlRegister5));
        m_BtnNextRegister5.onClick.AddListener(() =>
        {
            SetPnlOn(m_PnlLogin); // Back to login after finish
            SetBtnAccountExist(true);
        });

        m_BtnPreviousRegister2.onClick.AddListener(() => SetPnlOn(m_PnlRegister1));
        m_BtnPreviousRegister3.onClick.AddListener(() => SetPnlOn(m_PnlRegister2));
        m_BtnPreviousRegister4.onClick.AddListener(() => SetPnlOn(m_PnlRegister3));
        m_BtnPreviousRegister5.onClick.AddListener(() => SetPnlOn(m_PnlRegister4));

        DefaultValue();
    }

    private void DefaultValue()
    {
        SetGO(m_PnlLogin, true);
        SetGO(m_PnlForgotPassword, false);
        SetBtnAccountExist(true);

        foreach (var pnl in m_PnlRegisterList)
        {
            SetGO(pnl, false);
        }
    }

    #region UTILS

    private void SetBtnAccountExist(bool isShowBtnNoAccount)
    {
        m_BtnNoAccount.gameObject.SetActive(isShowBtnNoAccount);
        m_BtnHaveAccount.gameObject.SetActive(!isShowBtnNoAccount);
    }

    private void SetPnlOn(GameObject pnl)
    {
        SetAllPnlOff();
        SetGO(pnl, true);
    }

    private void SetGO(GameObject go, bool isOn)
    {
        go.SetActive(isOn);
    }

    private void SetAllPnlOff()
    {
        SetGO(m_PnlLogin, false);
        SetGO(m_PnlForgotPassword, false);

        foreach (var pnl in m_PnlRegisterList)
        {
            SetGO(pnl, false);
        }
    }

    #endregion
}
