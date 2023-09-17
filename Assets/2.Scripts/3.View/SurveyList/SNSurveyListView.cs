using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyListView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnBack;
    private Button m_BtnSearch;

    private GameObject m_PnlSurveyHistory;
    private GameObject m_PnlMySurvey;
    private GameObject m_PnlSurveyDetail;

    private Text m_TxtSceneTitle;
    private Text m_TxtSceneTitle2;
    private Text m_TxtSurveyTitle;

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
        m_BtnSearch = transform.Find("TopBar/BtnSearch").GetComponent<Button>();

        m_TxtSceneTitle = transform.Find("TopBar/TxtTitleSurveyHistory").GetComponent<Text>();
        m_TxtSceneTitle2 = transform.Find("TopBar/TxtTitleMySurvey").GetComponent<Text>();
        m_TxtSurveyTitle = transform.Find("TopBar/TxtSurveyTitle").GetComponent<Text>();

        m_PnlSurveyHistory = transform.Find("SurveyHistory").gameObject;
        m_PnlMySurvey = transform.Find("MySurvey").gameObject;
        m_PnlSurveyDetail = transform.Find("SurveyDetail").gameObject;

        m_ListPnl = new()
        {
            m_PnlSurveyHistory,
            m_PnlMySurvey,
            m_PnlSurveyDetail
        };

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnBack.onClick.AddListener(OnClickBack);
        m_BtnSearch.onClick.AddListener(OnClickSearch);

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
        ShowPnl(m_PnlSurveyHistory);
    }

    private void OpenMySurvey()
    {
        ShowPnl(m_PnlMySurvey);
    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl);
        ShowTitle(true);

        m_TxtSceneTitle.gameObject.SetActive(pnl == m_PnlSurveyHistory);
        m_TxtSceneTitle2.gameObject.SetActive(pnl == m_PnlMySurvey);

        m_PreviousPnl = pnl;
    }

    private void OpenSurveyDetail()
    {
        m_PreviousPnl = m_PnlMySurvey.activeSelf ? m_PnlMySurvey : m_PnlSurveyHistory;
        SNControl.Api.OpenPanel(m_PnlSurveyDetail, m_ListPnl);
        ShowTitle(false);
    }

    private void ShowTitle(bool isSceneTitleOn)
    {
        m_TxtSurveyTitle.gameObject.SetActive(!isSceneTitleOn);
        m_TxtSceneTitle.gameObject.SetActive(isSceneTitleOn);
        m_TxtSceneTitle2.gameObject.SetActive(isSceneTitleOn);

        m_BtnSearch.gameObject.SetActive(isSceneTitleOn);
        m_BtnBack.gameObject.SetActive(!isSceneTitleOn);
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
