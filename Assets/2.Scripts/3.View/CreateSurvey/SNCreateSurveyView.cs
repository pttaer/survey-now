using DG.Tweening;
using Newtonsoft.Json;
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
    private Button m_BtnBackToPnlName;

    private Transform m_Content;
    private SNCreateQuestionTypePnlView m_CreateQuestionTypePnlView;
    private Text m_TxtTitleSurveyName;
    private Text m_TxtTitleSurveyAdd;

    private Text m_TxtWarning;
    private Text m_TxtWarningDescription;
    private Text m_TxtConfirm;
    private Text m_TxtAreYouSure;
    private Text m_TxtNo;
    private GameObject m_Popup;
    private GameObject m_PnlSurveyName;
    private GameObject m_PopupSurveyVolunteer;
    private GameObject m_PnlQuestionType;

    private InputField m_IpfSurveyName;
    private InputField m_IpfSurveyDescription;

    private GameObject m_CustomQuestionSurveyItem;
    private GameObject m_MultipleQuestionSurveyItem;
    private GameObject m_RadioQuestionSurveyItem;
    private GameObject m_RatingQuestionSurveyItem;
    private GameObject m_LikertQuestionSurveyItem;

    private List<SNSurveyQuestionBaseView> m_ItemViewList;
    private List<SNSectionQuestionRequestDTO> m_SectionQuestionDTO;
    private List<SNSectionRequestDTO> m_SectionDTO;

    [SerializeField] string m_TxtSuccess;
    [SerializeField] string m_SuccessAddSurveyPleaseCheck;

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
        m_BtnBackToPnlName = transform.Find("TopBar/BtnBack").GetComponent<Button>();

        m_Content = transform.Find("SurveyAdd/Viewport/Content");
        m_CreateQuestionTypePnlView = transform.Find("Popup/QuestionType").GetComponent<SNCreateQuestionTypePnlView>();

        m_TxtTitleSurveyName = transform.Find("TopBar/TxtTitleSurveyName").GetComponent<Text>();
        m_TxtTitleSurveyAdd = transform.Find("TopBar/TxtTitleSurveyAdd").GetComponent<Text>();

        m_TxtWarning = transform.Find("TxtWarning").GetComponent<Text>();
        m_TxtWarningDescription = transform.Find("TxtWarningDescription").GetComponent<Text>();
        m_TxtConfirm = transform.Find("TxtConfirm").GetComponent<Text>();
        m_TxtAreYouSure = transform.Find("TxtAreYouSure").GetComponent<Text>();
        m_TxtNo = transform.Find("TxtNo").GetComponent<Text>();

        m_Popup = transform.Find("Popup").gameObject;
        m_PnlSurveyName = transform.Find("SurveyAdd/Viewport/Content/PnlSurveyName").gameObject;
        m_PopupSurveyVolunteer = transform.Find("Popup/SurveyVolunteer").gameObject;
        m_PnlQuestionType = transform.Find("Popup/QuestionType").gameObject;

        m_IpfSurveyName = transform.Find("SurveyAdd/Viewport/Content/PnlSurveyName/IpfSurveyName").GetComponent<InputField>();
        m_IpfSurveyDescription = transform.Find("SurveyAdd/Viewport/Content/PnlSurveyName/IpfSurveyDescription").GetComponent<InputField>();

        m_RatingQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionRating").gameObject;
        m_CustomQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionCustom").gameObject;
        m_RadioQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionRadio").gameObject;
        m_LikertQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionLikert").gameObject;
        m_MultipleQuestionSurveyItem = transform.Find("SpawnItems/SurveyQuestionMultiple").gameObject;

        m_ItemViewList = new();
        m_SectionQuestionDTO = new();
        m_SectionDTO = new();

        m_BtnMenu.onClick.AddListener(OnClickOpenMenu);
        m_BtnAdd.onClick.AddListener(OpenQuestionTypePanel);
        m_BtnExit.onClick.AddListener(ExitQuestionTypePanel);
        m_BtnNext.onClick.AddListener(NextPnl);
        m_BtnBackToPnlName.onClick.AddListener(() =>
        {
            SNControl.Api.ShowFAMPopup(m_TxtWarning.text, m_TxtAreYouSure.text, m_TxtConfirm.text, m_TxtNo.text, onConfirm: () =>
            {
                SetPnls(false);
            });
        });

        SNCreateSurveyControl.Api.OnClickChooseQuestionTypeEvent += SelectQuestionType;
        SNCreateSurveyControl.Api.OnDeleteItemReOrderQuestionListEvent += ReOrderItemList;

        m_BtnAdd.gameObject.SetActive(false);
        m_TxtTitleSurveyAdd.gameObject.SetActive(false);
        m_CreateQuestionTypePnlView.Init();
    }

    private void SetPnls(bool isNext)
    {
        m_TxtTitleSurveyName.gameObject.SetActive(!isNext);
        m_TxtTitleSurveyAdd.gameObject.SetActive(isNext);
        m_PnlSurveyName.gameObject.SetActive(!isNext);
        m_BtnAdd.gameObject.SetActive(isNext);
        //m_BtnBackToPnlName.gameObject.SetActive(isNext);
    }

    private void NextPnl()
    {
        if (m_TxtTitleSurveyAdd.gameObject.activeSelf)
        {
            // Popup confirm & post survey

            // Test
            foreach (var view in m_ItemViewList)
            {
                string json = JsonConvert.SerializeObject(view.GetQuestionData());
                m_SectionQuestionDTO.Add(view.GetQuestionData());
                Debug.Log("json " + view.name + " " + json);
                if (m_SectionQuestionDTO.Count > 9)
                {
                    var question = new List<SNSectionQuestionRequestDTO>(m_SectionQuestionDTO);

                    SNSectionRequestDTO dto = new()
                    {
                        Order = m_SectionDTO.Count + 1,
                        Questions = question
                    };
                    m_SectionDTO.Add(dto); // Add another section (page)
                    m_SectionQuestionDTO.Clear();
                }
            }

            Debug.Log("POST FORM count" + m_SectionDTO.Count);
            Debug.Log("POST FORM count" + m_SectionQuestionDTO.Count);

            if (m_SectionDTO.Count == 0)
            {
                var question = new List<SNSectionQuestionRequestDTO>(m_SectionQuestionDTO);

                SNSectionRequestDTO dto = new()
                {
                    Order = m_SectionDTO.Count + 1,
                    Questions = question
                };
                m_SectionDTO.Add(dto); // Add another section (page)
                m_SectionQuestionDTO.Clear();
            }

            Debug.Log("POST FORM count" + m_SectionDTO.Count);

            SNSurveyRequestDTO postData = new()
            {
                Title = m_IpfSurveyName.text,
                Description = m_IpfSurveyDescription.text,
                StartDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddDays(14),
                Sections = m_SectionDTO
            };

            StartCoroutine(SNApiControl.Api.PostData(SNConstant.SURVEY_CREATE, postData, callback: () => {
                SNControl.Api.UnloadThenLoadScene(SNConstant.SCENE_SURVEY_LIST);
                DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenMySurvey());
                DOVirtual.DelayedCall(0.4f, () => SNControl.Api.ShowFAMPopup(m_TxtSuccess, m_SuccessAddSurveyPleaseCheck, "Ok", "NotShow"));
            }));

            m_SectionDTO.Clear();
            m_SectionQuestionDTO.Clear();
        }

        if (m_PnlSurveyName.activeSelf)
        {
            if (string.IsNullOrEmpty(m_IpfSurveyName.text))
            {
                SNControl.Api.ShowFAMPopup(m_TxtWarning.text, m_TxtWarningDescription.text, m_TxtConfirm.text, "NotShow");
                return;
            }
            SetPnls(true);
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
