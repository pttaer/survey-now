using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyListView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnBack;
    //private Button m_BtnSearch;

    private SNSurveyListHistoryView m_PnlSurveyHistoryView;
    private SNSurveyListMySurveyView m_PnlMySurveyView;
    private SNSurveyListSurveyDetailView m_PnlSurveyDetailView;

    private Text m_TxtSceneTitle;
    private Text m_TxtSceneTitle2;
    private Text m_TxtSurveyTitle;
    private Text m_TxtSurveyStatus;

    private List<GameObject> m_ListPnl;
    private GameObject m_PreviousPnl;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
        //m_BtnSearch = transform.Find("TopBar/BtnSearch").GetComponent<Button>();

        m_TxtSceneTitle = transform.Find("TopBar/TxtTitleSurveyHistory").GetComponent<Text>();
        m_TxtSceneTitle2 = transform.Find("TopBar/TxtTitleMySurvey").GetComponent<Text>();
        m_TxtSurveyTitle = transform.Find("TopBar/TxtSurveyTitle").GetComponent<Text>();
        m_TxtSurveyStatus = transform.Find("TopBar/TxtSurveyStatus").GetComponent<Text>();

        m_PnlSurveyHistoryView = transform.Find("SurveyHistory").GetComponent<SNSurveyListHistoryView>();
        m_PnlMySurveyView = transform.Find("MySurvey").GetComponent<SNSurveyListMySurveyView>();
        m_PnlSurveyDetailView = transform.Find("SurveyDetail").GetComponent<SNSurveyListSurveyDetailView>();

        m_ListPnl = new()
        {
            m_PnlSurveyHistoryView.gameObject,
            m_PnlMySurveyView.gameObject,
            m_PnlSurveyDetailView.gameObject
        };

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnBack.onClick.AddListener(OnClickBack);
        //m_BtnSearch.onClick.AddListener(OnClickSearch);

        SNSurveyListControl.Api.OnOpenSurveyDetailEvent += OpenSurveyDetail;
        SNSurveyListControl.Api.OnOpenMySurveyEvent += OpenMySurvey;
        SNSurveyListControl.Api.OnOpenSurveyHistoryEvent += OpenSurveyHistory;
    }

    private void OnDestroy()
    {
        SNSurveyListControl.Api.OnOpenSurveyDetailEvent -= OpenSurveyDetail;
        SNSurveyListControl.Api.OnOpenMySurveyEvent -= OpenMySurvey;
        SNSurveyListControl.Api.OnOpenSurveyHistoryEvent -= OpenSurveyHistory;
    }

    private void OnClickSearch()
    {
        throw new NotImplementedException();
    }

    private void OnClickBack()
    {
        ShowPnl(m_PreviousPnl);
    }

    private void OpenSurveyHistory()
    {
        ShowPnl(m_PnlSurveyHistoryView.gameObject);
        m_PnlSurveyHistoryView.InitMySurveyHistory();
    }

    private void OpenMySurvey()
    {
        ShowPnl(m_PnlMySurveyView.gameObject);
        m_PnlMySurveyView.InitMySurvey();
    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl);

        ShowTitle(true);

        m_TxtSceneTitle.gameObject.SetActive(pnl == m_PnlSurveyHistoryView.gameObject);
        m_TxtSceneTitle2.gameObject.SetActive(pnl == m_PnlMySurveyView.gameObject);

        m_PreviousPnl = pnl;
    }

    private void OpenSurveyDetail(SNSurveyResponseDTO data)
    {
        // Show pnl detail
        m_PreviousPnl = m_PnlMySurveyView.gameObject.activeSelf ? m_PnlMySurveyView.gameObject : m_PnlSurveyHistoryView.gameObject;
        SNControl.Api.OpenPanel(m_PnlSurveyDetailView.gameObject, m_ListPnl);
        ShowTitle(false);
        m_PnlSurveyDetailView.Init(data.Id, data.Title, data.Status);
    }

    private void ShowTitle(bool isSceneTitleOn)
    {
        m_TxtSceneTitle.gameObject.SetActive(isSceneTitleOn);
        m_TxtSceneTitle2.gameObject.SetActive(isSceneTitleOn);
        m_TxtSurveyTitle.gameObject.SetActive(!isSceneTitleOn);
        m_TxtSurveyStatus.gameObject.SetActive(!isSceneTitleOn);

        // m_BtnSearch.gameObject.SetActive(isSceneTitleOn);
        m_BtnBack.gameObject.SetActive(!isSceneTitleOn);
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
