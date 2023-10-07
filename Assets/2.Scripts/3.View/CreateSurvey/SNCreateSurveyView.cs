using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNCreateSurveyView : MonoBehaviour
{
    private Button m_BtnMenu;
    private Button m_BtnAdd;
    private Button m_BtnExit;
    private Button m_BtnNext;

    private Transform m_Content;
    private SNCreateQuestionTypePnlView m_CreateQuestionTypePnlView;
    private Text m_TxtTitleSurveyName;
    private Text m_TxtTitleSurveyAdd;

    private GameObject m_Popup;
    private GameObject m_PnlSurveyName;
    private GameObject m_PopupSurveyVolunteer;
    private GameObject m_PnlQuestionType;

    private GameObject m_CustomQuestionSurveyItem;
    private GameObject m_MultipleQuestionSurveyItem;
    private GameObject m_RadioQuestionSurveyItem;
    private GameObject m_RatingQuestionSurveyItem;
    private GameObject m_LikertQuestionSurveyItem;

    private List<SNSurveyQuestionBaseView> m_ItemViewList;

    void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        SNCreateSurveyControl.Api.OnClickChooseQuestionTypeEvent -= SelectQuestionType;
        SNCreateSurveyControl.Api.OnDeleteItemReOrderQuestionListEvent -= ReOrderItemList;
    }

    public void Init()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnAdd = transform.Find("SurveyAdd/BtnAdd").GetComponent<Button>();
        m_BtnExit = transform.Find("Popup/QuestionType/BtnExit").GetComponent<Button>();
        m_BtnNext = transform.Find("SurveyAdd/BtnNext").GetComponent<Button>();

        m_Content = transform.Find("SurveyAdd/Viewport/Content");
        m_CreateQuestionTypePnlView = transform.Find("Popup/QuestionType").GetComponent<SNCreateQuestionTypePnlView>();

        m_TxtTitleSurveyName = transform.Find("TopBar/TxtTitleSurveyName").GetComponent<Text>();
        m_TxtTitleSurveyAdd = transform.Find("TopBar/TxtTitleSurveyAdd").GetComponent<Text>();

        m_Popup = transform.Find("Popup").gameObject;
        m_PnlSurveyName = transform.Find("SurveyAdd/Viewport/Content/PnlSurveyName").gameObject;
        m_PopupSurveyVolunteer = transform.Find("Popup/SurveyVolunteer").gameObject;
        m_PnlQuestionType = transform.Find("Popup/QuestionType").gameObject;

        m_RatingQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionRating").gameObject;
        m_CustomQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionCustom").gameObject;
        m_RadioQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionRadio").gameObject;
        m_LikertQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionLikert").gameObject;
        m_MultipleQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionMultiple").gameObject;

        m_ItemViewList = new();

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnAdd.onClick.AddListener(OpenQuestionTypePanel);
        m_BtnExit.onClick.AddListener(ExitQuestionTypePanel);
        m_BtnNext.onClick.AddListener(NextPnl);

        SNCreateSurveyControl.Api.OnClickChooseQuestionTypeEvent += SelectQuestionType;
        SNCreateSurveyControl.Api.OnDeleteItemReOrderQuestionListEvent += ReOrderItemList;

        m_BtnAdd.gameObject.SetActive(false);
        m_TxtTitleSurveyAdd.gameObject.SetActive(false);
        m_CreateQuestionTypePnlView.Init();
    }

    private void NextPnl()
    {
        if (m_PnlSurveyName.activeSelf)
        {
            m_TxtTitleSurveyName.gameObject.SetActive(false);
            m_TxtTitleSurveyAdd.gameObject.SetActive(true);
            m_PnlSurveyName.gameObject.SetActive(false);
            m_BtnAdd.gameObject.SetActive(true);
        }

        if (m_TxtTitleSurveyAdd.gameObject.activeSelf)
        {
            // Popup confirm & post survey
        }
    }

    private void SelectQuestionType(string type)
    {
        switch (type)
        {
            case "Radio":
                InitQuestion<SNSurveyQuestionRadioView>(AddQuestion(m_RadioQuestionSurveyItem));
                break;
            case "Multiple":
                InitQuestion<SNSurveyQuestionMultipleView>(AddQuestion(m_MultipleQuestionSurveyItem));
                break;
            case "Rating":
                InitQuestion<SNSurveyQuestionRatingView>(AddQuestion(m_RatingQuestionSurveyItem));
                break;
            case "Likert":
                InitQuestion<SNSurveyQuestionLikertView>(AddQuestion(m_LikertQuestionSurveyItem));
                break;
            case "Custom":
                InitQuestion<SNSurveyQuestionCustomView>(AddQuestion(m_CustomQuestionSurveyItem));
                break;
        }

        void InitQuestion<T>(SNSurveyQuestionBaseView view) where T : SNSurveyQuestionBaseView
        {
            var questionView = (T)view;
            questionView.Init();
        }
    }

    private SNSurveyQuestionBaseView AddQuestion(GameObject spawnObject)
    {
        GameObject go = Instantiate(spawnObject, m_Content);
        SNSurveyQuestionBaseView view = go.GetComponent<SNSurveyQuestionBaseView>();
        m_ItemViewList.Add(view);
        view.InitBase(m_ItemViewList.Count);

        ExitQuestionTypePanel();

        return view;
    }

    private void ReOrderItemList(GameObject deleteGameObject)
    {
        m_ItemViewList?.RemoveAll(view => view.gameObject == deleteGameObject);

        for (int i = 0; i < m_ItemViewList.Count; i++)
        {
            m_ItemViewList[i].SetOrder(i + 1);
        }
    }

    private void ExitQuestionTypePanel()
    {
        m_Popup.SetActive(false);
        m_PnlQuestionType.SetActive(false);
    }

    private void OpenQuestionTypePanel()
    {
        m_Popup.SetActive(true);
        m_PopupSurveyVolunteer.SetActive(false);
        m_PnlQuestionType.SetActive(true);
    }

    private void OnClickOpenMenu()
    {
        print("v");
        SNMainControl.Api.OpenMenuPnl();
    }
}
