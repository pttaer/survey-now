using DG.Tweening;
using System;
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

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnHome = transform.Find("Body/Viewport/Content/BtnHome").GetComponent<Button>();
        m_BtnProfile = transform.Find("Body/Viewport/Content/BtnProfile").GetComponent<Button>();
        m_BtnCreateSurvey = transform.Find("Body/Viewport/Content/BtnCreateSurvey").GetComponent<Button>();
        m_BtnMySurvey = transform.Find("Body/Viewport/Content/BtnMySurvey").GetComponent<Button>();
        m_BtnHistory = transform.Find("Body/Viewport/Content/BtnHistory").GetComponent<Button>();
        m_BtnBilling = transform.Find("Body/Viewport/Content/BtnBilling").GetComponent<Button>();
        m_BtnPoints = transform.Find("Body/Viewport/Content/BtnPoints").GetComponent<Button>();
        m_BtnLogout = transform.Find("Body/Viewport/Content/BtnLogout").GetComponent<Button>();

        m_BtnMenu.onClick.AddListener(CloseMenu);
        m_BtnHome.onClick.AddListener(LoadHome);
        m_BtnProfile.onClick.AddListener(LoadProfile);
        m_BtnCreateSurvey.onClick.AddListener(LoadCreate);
        m_BtnMySurvey.onClick.AddListener(LoadMySurvey);
        m_BtnHistory.onClick.AddListener(LoadHistory);
        m_BtnBilling.onClick.AddListener(LoadBilling);
        m_BtnPoints.onClick.AddListener(LoadPoints);
        m_BtnLogout.onClick.AddListener(Logout);

        SNMainControl.Api.OnClickMenuEvent += OpenMenu;

        // Default value
        gameObject.SetActive(false);
    }

    private void LoadHome()
    {
        LoadScene(SNConstant.SCENE_BUNDLE);
    }

    private void OnDestroy()
    {
        SNMainControl.Api.OnClickMenuEvent -= OpenMenu;
    }

    private void LoadProfile()
    {
        LoadScene(SNConstant.SCENE_MAIN);
        DOVirtual.DelayedCall(0.2f, () => SNMainControl.Api.OpenProfile());
    }

    private void LoadCreate()
    {
        LoadScene(SNConstant.SCENE_CREATE_SURVEY);
    }

    private void LoadMySurvey()
    {
        LoadScene(SNConstant.SCENE_SURVEY_LIST);
        DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenMySurvey());
    }

    private void LoadHistory()
    {
        LoadScene(SNConstant.SCENE_SURVEY_LIST);
        DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenSurveyHistory());
    }

    private void LoadBilling()
    {
        LoadScene(SNConstant.SCENE_BUNDLE);
    }

    private void LoadPoints()
    {
        LoadScene(SNConstant.SCENE_MAIN);
        DOVirtual.DelayedCall(0.2f, () => SNMainControl.Api.OpenAccountPurchase());
    }

    private void Logout()
    {
        LoadScene(SNConstant.SCENE_LOGIN);
    }

    private void LoadScene(string sceneName, Action callback = null)
    {
        CloseMenu();
        SNControl.Api.UnloadThenLoadScene(sceneName);
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
