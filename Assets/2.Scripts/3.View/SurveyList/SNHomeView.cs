using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SNDoSurveyDTO;

public class SNHomeView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnBack;
    private Text m_Title;
    private Text m_TxtSurveyTitle;
    //private Button m_BtnSearch;

    private SNSurveyListMySurveyView m_PnlMySurveyView;
    private SNSurveyListSurveyDetailView m_PnlSurveyDetailView;

    private List<GameObject> m_ListPnl;
    private GameObject m_PreviousPnl;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        Transform topBar = transform.Find("TopBar");
        m_BtnMenu = topBar.Find("BtnMenu").GetComponent<Button>();
        m_BtnBack = topBar.Find("BtnBack").GetComponent<Button>();
        m_Title = topBar.Find("TxtTitleHome").GetComponent<Text>();
        m_TxtSurveyTitle = topBar.Find("TxtSurveyTitle").GetComponent<Text>();

        //m_BtnSearch = transform.Find("TopBar/BtnSearch").GetComponent<Button>();

        m_PnlMySurveyView = transform.Find("MySurvey").GetComponent<SNSurveyListMySurveyView>();
        m_PnlSurveyDetailView = transform.Find("SurveyDetail").GetComponent<SNSurveyListSurveyDetailView>();

        m_ListPnl = new()
        {
            m_PnlMySurveyView.gameObject,
            m_PnlSurveyDetailView.gameObject
        };

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnBack.onClick.AddListener(OnClickBack);
        //m_BtnSearch.onClick.AddListener(OnClickSearch);

        SNSurveyListControl.Api.OnOpenSurveyDetailEvent += OpenSurveyDetail;
        SNSurveyListControl.Api.OnOpenMySurveyEvent += OpenHome;
        SNSurveyListControl.Api.OnClickBackToHomeEvent += OnClickBack;

        OpenHome();
    }

    private void OnDestroy()
    {
        SNSurveyListControl.Api.OnOpenSurveyDetailEvent -= OpenSurveyDetail;
        SNSurveyListControl.Api.OnOpenMySurveyEvent -= OpenHome;
        SNSurveyListControl.Api.OnClickBackToHomeEvent -= OnClickBack;
    }

    private void OnClickSearch()
    {
        throw new NotImplementedException();
    }

    private void OnClickBack()
    {
        ShowPnl(m_PreviousPnl);
        m_TxtSurveyTitle.gameObject.SetActive(false);
    }

    private void OpenHome()
    {
        ShowPnl(m_PnlMySurveyView.gameObject);
        m_PnlMySurveyView.InitHome();
    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl);

        m_Title.gameObject.SetActive(true);

        ShowBackBtn(true);

        m_PreviousPnl = pnl;
    }

    private void OpenSurveyDetail(SNSurveyResponseDTO data)
    {
        // Show pnl detail
        m_Title.gameObject.SetActive(false);
        m_PreviousPnl = m_PnlMySurveyView.gameObject;
        SNControl.Api.OpenPanel(m_PnlSurveyDetailView.gameObject, m_ListPnl);
        ShowBackBtn(false);
        m_PnlSurveyDetailView.Init(data.Id, data.Title, data.Status, data.CreatedUserId);
    }

    private void ShowBackBtn(bool isSceneTitleOn)
    {
        //m_BtnSearch.gameObject.SetActive(isSceneTitleOn);
        m_BtnBack.gameObject.SetActive(!isSceneTitleOn);
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
