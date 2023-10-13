using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMenuView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnHome;
    private Button m_BtnProfile;
    private Button m_BtnCreateSurvey;
    private Button m_BtnMySurvey;
    private Button m_BtnHistory;
    private Button m_BtnBilling;
    private Button m_BtnPoints;
    private Button m_BtnLogout;

    private Text m_TxtProfile;

    private List<SNMenuIconView> m_MenuIconViewList;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform pnlBtn = transform.Find("Body/Viewport/Content/PnlBtn");

        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnHome = pnlBtn.Find("BtnHome").GetComponent<Button>();
        m_BtnProfile = pnlBtn.Find("BtnProfile").GetComponent<Button>();
        m_BtnCreateSurvey = pnlBtn.Find("BtnCreateSurvey").GetComponent<Button>();
        m_BtnMySurvey = pnlBtn.Find("BtnMySurvey").GetComponent<Button>();
        m_BtnHistory = pnlBtn.Find("BtnHistory").GetComponent<Button>();
        m_BtnBilling = pnlBtn.Find("BtnBilling").GetComponent<Button>();
        m_BtnPoints = pnlBtn.Find("BtnPoints").GetComponent<Button>();
        m_BtnLogout = pnlBtn.Find("BtnLogout").GetComponent<Button>();

        m_TxtProfile = transform.Find("Body/Viewport/Content/PnlProfile/TxtProfile").GetComponent<Text>();

        m_BtnMenu.onClick.AddListener(CloseMenu);
        m_BtnHome.onClick.AddListener(LoadHome);
        m_BtnProfile.onClick.AddListener(LoadProfile);
        m_BtnCreateSurvey.onClick.AddListener(LoadCreate);
        m_BtnMySurvey.onClick.AddListener(LoadMySurvey);
        m_BtnHistory.onClick.AddListener(LoadHistory);
        m_BtnBilling.onClick.AddListener(LoadBilling);
        m_BtnPoints.onClick.AddListener(LoadPoints);
        m_BtnLogout.onClick.AddListener(Logout);

        m_MenuIconViewList = new();

        foreach (Transform transform in pnlBtn)
        {
            m_MenuIconViewList.Add(transform.GetComponent<SNMenuIconView>());
        }

        SNMainControl.Api.OnClickMenuEvent += OpenMenu;
        SNMenuControl.Api.onOpenBuyPoints += LoadPoints;
        SNMainControl.Api.OnDeleteSurveyBackToMySurveyEvent += LoadMySurvey;
        SNMainControl.Api.OnOpenMySurveyEvent += LoadMySurvey;

        // Default value
        gameObject.SetActive(false);
        m_TxtProfile.text = PlayerPrefs.GetString(SNConstant.USER_FULLNAME_CACHE);
    }

    private void OnDestroy()
    {
        SNMainControl.Api.OnClickMenuEvent -= OpenMenu;
        SNMenuControl.Api.onOpenBuyPoints -= LoadPoints;
        SNMainControl.Api.OnDeleteSurveyBackToMySurveyEvent -= LoadMySurvey;
        SNMainControl.Api.OnOpenMySurveyEvent -= LoadMySurvey;
    }

    private void LoadHome()
    {
        LoadScene(SNConstant.SCENE_HOME, m_BtnHome);
    }

    private void LoadProfile()
    {
        LoadScene(SNConstant.SCENE_MAIN, m_BtnProfile);
        DOVirtual.DelayedCall(0.2f, () => SNMainControl.Api.OpenProfile());
    }

    private void LoadCreate()
    {
        LoadScene(SNConstant.SCENE_CREATE_SURVEY, m_BtnCreateSurvey);
    }

    private void LoadMySurvey()
    {
        LoadScene(SNConstant.SCENE_SURVEY_LIST, m_BtnMySurvey);
        DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenMySurvey());
    }

    private void LoadHistory()
    {
        LoadScene(SNConstant.SCENE_SURVEY_LIST, m_BtnHistory);
        DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenSurveyHistory());
    }

    private void LoadBilling()
    {
        LoadScene(SNConstant.SCENE_BUNDLE, m_BtnBilling);
        DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenScenePacks(-1));
    }

    private void LoadPoints()
    {
        LoadScene(SNConstant.SCENE_MAIN, m_BtnPoints);
        DOVirtual.DelayedCall(0.2f, () => SNMainControl.Api.OpenAccountPurchase());
    }

    private void Logout()
    {
        foreach (var view in m_MenuIconViewList)
        {
            view.SetButtonColor("BtnHome");
        }
        LoadScene(SNConstant.SCENE_LOGIN);
    }

    private void LoadScene(string sceneName, Button btn = null, Action callback = null)
    {
        CloseMenu();
        SNControl.Api.UnloadThenLoadScene(sceneName);

        foreach (var view in m_MenuIconViewList)
        {
            view.SetButtonColor(btn.name);
        }

        callback?.Invoke();
    }

    private void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
