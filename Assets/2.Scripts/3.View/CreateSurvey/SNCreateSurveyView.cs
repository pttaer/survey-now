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

    private List<GameObject> m_ItemList;

    void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        SNCreateSurveyControl.Api.OnClickChooseQuestionTypeEvent -= SelectQuestionType;
    }

    public void Init()
    {
        m_BtnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BtnAdd = transform.Find("SurveyAdd/BtnAdd").GetComponent<Button>();
        m_BtnExit = transform.Find("Popup/QuestionType/BtnExit").GetComponent<Button>();
        m_BtnNext = transform.Find("SurveyAdd/BtbNext").GetComponent<Button>();

        m_Content = transform.Find("SurveyAdd/Viewport/Content");
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

        m_ItemList = new();

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnAdd.onClick.AddListener(OpenQuestionTypePanel);
        m_BtnExit.onClick.AddListener(ExitQuestionTypePanel);
        m_BtnNext.onClick.AddListener(NextPnl);

        SNCreateSurveyControl.Api.OnClickChooseQuestionTypeEvent += SelectQuestionType;

        m_BtnAdd.gameObject.SetActive(false);
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
                AddQuestion(m_RadioQuestionSurveyItem);
                break;
            case "Multiple":
                AddQuestion(m_MultipleQuestionSurveyItem);
                break;
            case "Rating":
                AddQuestion(m_RatingQuestionSurveyItem);
                break;
            case "Likert":
                AddQuestion(m_LikertQuestionSurveyItem);
                break;
            case "Custom":
                AddQuestion(m_CustomQuestionSurveyItem);
                break;
        }
    }

    private void AddQuestion(GameObject spawnObject)
    {
        GameObject go = Instantiate(spawnObject, m_Content);
        m_ItemList.Add(go);
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
