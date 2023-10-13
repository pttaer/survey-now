using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNLoginView : MonoBehaviour
{
    private Button m_BtnSkip;
    private Button m_BtnNoAccount;
    private Button m_BtnHaveAccount;

    private Button m_BtnForgotPassword;
    private Button m_BtnSendOTP;
    private Button m_BtnLogin;
    private Button m_BtnGoToLogin;
    private Button m_BtnShowPass;

    private Image m_ImgIconShow;
    private Image m_ImgIconHide;

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

    private InputField m_IpfEmail_Login;
    private InputField m_IpfPassword_Login;

    private InputField m_IpfFirstname;
    private InputField m_IpfLastname;
    private InputField m_IpfEmail_Register;
    private InputField m_IpfEmailConfirm_Register;
    private InputField m_IpfPassword_Register;
    private InputField m_IpfPasswordConfirm_Register;

    private Text m_TxtFailLogin;

    private ToggleGroup m_TglGrGender;

    // no ref var
    private List<GameObject> m_PnlRegisterList;

    [SerializeField] string m_TxtWarning;
    [SerializeField] string m_TxtConfirmPasswordNotCorrect;
    [SerializeField] string m_TxtConfirm;
    [SerializeField] string m_TxtNotAllInfoFilledTryAgain;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform body = transform.Find("Body");

        m_BtnSkip = transform.parent.transform.Find("BtnSkip").GetComponent<Button>();
        m_BtnNoAccount = body.Find("BtnNoAccount").GetComponent<Button>();
        m_BtnHaveAccount = body.Find("BtnHaveAccount").GetComponent<Button>();

        m_BtnForgotPassword = body.Find("Content/PnlLogin/BtnLogin/BtnForgotPassword").GetComponent<Button>();
        m_BtnSendOTP = body.Find("Content/PnlForgotPassword/BtnSendOTP").GetComponent<Button>();
        m_BtnLogin = body.Find("Content/PnlLogin/BtnLogin").GetComponent<Button>();
        m_BtnGoToLogin = body.Find("Content/PnlForgotPassword/BtnSendOTP/BtnLogin").GetComponent<Button>();

        m_BtnShowPass = body.Find("Content/PnlLogin/IpfPassword/BtnHideShowPass").GetComponent<Button>();

        m_ImgIconShow = body.Find("Content/PnlLogin/IpfPassword/BtnHideShowPass/Show").GetComponent<Image>();
        m_ImgIconHide = body.Find("Content/PnlLogin/IpfPassword/BtnHideShowPass/Hide").GetComponent<Image>();

        m_IpfEmail_Login = body.Find("Content/PnlLogin/IpfEmail").GetComponent<InputField>();
        m_IpfPassword_Login = body.Find("Content/PnlLogin/IpfPassword").GetComponent<InputField>();

        m_PnlLogin = body.Find("Content/PnlLogin").gameObject;
        m_PnlForgotPassword = body.Find("Content/PnlForgotPassword").gameObject;

        m_PnlRegister1 = body.Find("Content/PnlRegister1").gameObject;
        m_PnlRegister2 = body.Find("Content/PnlRegister2").gameObject;
        m_PnlRegister3 = body.Find("Content/PnlRegister3").gameObject;
        m_PnlRegister4 = body.Find("Content/PnlRegister4").gameObject;
        m_PnlRegister5 = body.Find("Content/PnlRegister5").gameObject;

        m_IpfFirstname = m_PnlRegister1.transform.Find("IpfName").GetComponent<InputField>();
        m_IpfLastname = m_PnlRegister1.transform.Find("IpfFamilyName").GetComponent<InputField>();
        m_IpfEmail_Register = m_PnlRegister1.transform.Find("IpfEmail").GetComponent<InputField>();
        m_IpfEmailConfirm_Register = m_PnlRegister1.transform.Find("IpfEmailConfirm").GetComponent<InputField>();

        m_IpfPassword_Register = m_PnlRegister3.transform.Find("IpfPassword").GetComponent<InputField>();
        m_IpfPasswordConfirm_Register = m_PnlRegister3.transform.Find("IpfReEnterPassword").GetComponent<InputField>();

        m_BtnNextRegister1 = body.Find("Content/PnlRegister1/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister2 = body.Find("Content/PnlRegister2/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister3 = body.Find("Content/PnlRegister3/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister4 = body.Find("Content/PnlRegister4/BtnGroup/BtnNext").GetComponent<Button>();
        m_BtnNextRegister5 = body.Find("Content/PnlRegister5/BtnGroup/BtnFinish").GetComponent<Button>();

        m_BtnPreviousRegister2 = body.Find("Content/PnlRegister2/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister3 = body.Find("Content/PnlRegister3/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister4 = body.Find("Content/PnlRegister4/BtnGroup/BtnPrevious").GetComponent<Button>();
        m_BtnPreviousRegister5 = body.Find("Content/PnlRegister5/BtnGroup/BtnPrevious").GetComponent<Button>();

        m_TxtFailLogin = m_PnlLogin.transform.Find("TxtFailLogin").GetComponent<Text>();

        m_PnlRegisterList = new List<GameObject>
        {
            m_PnlRegister1,
            m_PnlRegister2,
            m_PnlRegister3,
            m_PnlRegister4,
            m_PnlRegister5,
        };

        m_BtnSkip.onClick.AddListener(() =>
        {
            LoadSceneMain();
        });

        // Register OnClick
        m_BtnNoAccount.onClick.AddListener(() =>
        {
            SetPnlOn(m_PnlRegister1);
            SetBtnAccountExist(false);
        });
        m_BtnHaveAccount.onClick.AddListener(() =>
        {
            Register();
            SetPnlOn(m_PnlLogin);
            SetBtnAccountExist(true);
        });

        m_BtnForgotPassword.onClick.AddListener(() => SetPnlOn(m_PnlForgotPassword));
        m_BtnSendOTP.onClick.AddListener(() => SetPnlOn(m_PnlForgotPassword));
        m_BtnLogin.onClick.AddListener(Login);
        m_BtnGoToLogin.onClick.AddListener(() => SetPnlOn(m_PnlLogin));

        m_BtnNextRegister1.onClick.AddListener(() => SetPnlOn(m_PnlRegister3));
        m_BtnNextRegister2.onClick.AddListener(() => SetPnlOn(m_PnlRegister3));
        m_BtnNextRegister3.onClick.AddListener(() =>
        {
            // Register
            ValidateRegister();
        });

        m_BtnNextRegister4.onClick.AddListener(() => SetPnlOn(m_PnlRegister5));
        m_BtnNextRegister5.onClick.AddListener(() =>
        {
            SetPnlOn(m_PnlLogin); // Back to login after finish
            SetBtnAccountExist(true);
        });

        m_BtnPreviousRegister2.onClick.AddListener(() => SetPnlOn(m_PnlRegister1));
        m_BtnPreviousRegister3.onClick.AddListener(() => SetPnlOn(m_PnlRegister1));
        m_BtnPreviousRegister4.onClick.AddListener(() => SetPnlOn(m_PnlRegister3));
        m_BtnPreviousRegister5.onClick.AddListener(() => SetPnlOn(m_PnlRegister4));

        m_BtnShowPass.onClick.AddListener(ShowPass);

        SNControl.Api.OnFailLogin += FailLogin;

        DefaultValue();
    }

    private void ShowPass()
    {
        bool hiding = m_ImgIconShow.gameObject.activeSelf;

        m_IpfPassword_Login.contentType = hiding ? InputField.ContentType.Standard : InputField.ContentType.Password;
        m_IpfPassword_Login.ForceLabelUpdate();

        m_ImgIconShow.gameObject.SetActive(!hiding);
        m_ImgIconHide.gameObject.SetActive(hiding);
    }

    private void ValidateRegister()
    {
        if (!string.IsNullOrEmpty(m_IpfFirstname.text)
            && !string.IsNullOrEmpty(m_IpfLastname.text)
            && !string.IsNullOrEmpty(m_IpfEmail_Register.text)
            && !string.IsNullOrEmpty(m_IpfPassword_Register.text)
            && !string.IsNullOrEmpty(m_IpfPasswordConfirm_Register.text))
        {
            if (m_IpfPassword_Register.text == m_IpfPasswordConfirm_Register.text)
            {
                Register();
            }
            else
            {
                SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtConfirmPasswordNotCorrect, m_TxtConfirm, "NotShow");
            }
        }
        else
        {
            SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtNotAllInfoFilledTryAgain, m_TxtConfirm, "NotShow", onConfirm: () =>
            {
                if (string.IsNullOrEmpty(m_IpfFirstname.text)
                    && string.IsNullOrEmpty(m_IpfLastname.text)
                    && string.IsNullOrEmpty(m_IpfEmail_Register.text))
                {
                    SetPnlOn(m_PnlRegister1);
                }
            });
        }
    }

    private void Register()
    {
        SNUserDTO newUser = new SNUserDTO()
        {
            FullName = m_IpfFirstname.text + " " + m_IpfLastname.text,
            Email = m_IpfEmail_Register.text,
            Password = m_IpfPassword_Register.text,
        };

        StartCoroutine(SNApiControl.Api.Register(newUser));
    }

    private void OnDestroy()
    {
        SNControl.Api.OnFailLogin -= FailLogin;
    }

    private void LoadSceneMain()
    {
        SNControl.Api.UnloadThenLoadScene(SNConstant.SCENE_HOME);
    }

    private void Login()
    {
        if (!string.IsNullOrEmpty(m_IpfEmail_Login.text) && !string.IsNullOrEmpty(m_IpfPassword_Login.text))
        {
            StartCoroutine(SNApiControl.Api.Login(m_IpfEmail_Login.text, m_IpfPassword_Login.text, () =>
            {
                SetGO(m_TxtFailLogin.gameObject, false);
                LoadSceneMain();
                SNControl.Api.HideLoading();
            }));
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(SNApiControl.Api.Login("user1@gmail.com", "12345678", () =>
            {
                LoadSceneMain();
            }));
        }
#endif
    }

    private void DefaultValue()
    {
        SetGO(m_PnlLogin, true);
        SetGO(m_PnlForgotPassword, false);
        SetBtnAccountExist(true);
        SetGO(m_TxtFailLogin.gameObject, false);

#if !UNITY_EDITOR
        SetGO(m_BtnSkip.gameObject, false);
#endif

        foreach (var pnl in m_PnlRegisterList)
        {
            SetGO(pnl, false);
        }
    }

    private void FailLogin()
    {
        SetGO(m_TxtFailLogin.gameObject, true);
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
