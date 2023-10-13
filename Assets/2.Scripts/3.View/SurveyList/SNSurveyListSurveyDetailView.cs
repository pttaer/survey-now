using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static SNDoSurveyDTO;
using Newtonsoft.Json;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;
using static SNSurveyAnswerDTO;
using Unity.VisualScripting;

public class SNSurveyListSurveyDetailView : MonoBehaviour
{
    private GameObject m_SurveyQuestionRadioView;
    private GameObject m_SurveyQuestionMultipleView;
    private GameObject m_SurveyQuestionRatingView;
    private GameObject m_SurveyQuestionLikertView;
    private GameObject m_SurveyQuestionCustomView;
    private GameObject m_PnlCompleteSurvey;
    private GameObject m_PnlPnlAlreadyCompleteSurvey;

    private Text m_TxtSurveyTitle;
    private Text m_TxtSurveyStatus;

    private Button m_BtnComplete;
    private Button m_BtnBackToHome;
    private Button m_BtnGoToHistory;
    private Button m_BtnDeleteSurvey;
    private Button m_BtnPostSurvey;
    private Button m_BtnActiveSurvey;

    private Text m_TxtOn;
    private Text m_TxtOff;

    private Transform m_QuestionContainer;
    private List<SNInitView> m_InitViewList;

    private bool m_IsInit = false;
    private int m_Id;
    private string m_TxtStat;

    [SerializeField] string m_TxtWarning;
    [SerializeField] string m_TxtDoYouSureDeleteSurvey;
    [SerializeField] string m_TxtConfirmDelete;
    [SerializeField] string m_TxtNo;

    [SerializeField] string m_TxtActive;
    [SerializeField] string m_TxtInActive;
    [SerializeField] string m_TxtDraft;
    [SerializeField] string m_TxtExpired;
    [SerializeField] string m_TxtPackPurchased;

    [SerializeField] string m_TxtYouWant;
    [SerializeField] string m_TxtPurchasePackBeforePost;
    [SerializeField] string m_TxtPostSurvey;
    [SerializeField] string m_TxtPostSurveyNow;
    [SerializeField] string m_TxtPurchasePack;
    [SerializeField] string m_TxtConfirmPost;

    [SerializeField] string m_TxtFail;
    [SerializeField] string m_TxtNotYetFinishSurvey;
    [SerializeField] string m_TxtYouCannotDoYourSurvey;
    [SerializeField] string m_TxtYouCannotDeleteAnySurveyThanDraft;
    [SerializeField] string m_TxtBack;
    [SerializeField] string m_TxtYes;

    private string m_Status;

    public void Init(int id, string title, string status, int userIdCreated = -1, bool isHistoryPnl = false)
    {
        bool isNotHome = !SceneManager.GetSceneByName(SNConstant.SCENE_HOME).isLoaded;

        Debug.Log("ID " + id);
        m_Id = id;

        if (!m_IsInit)
        {
            m_SurveyQuestionRadioView = transform.Find("Viewport/Content/SurveyRecordRadio").gameObject;
            m_SurveyQuestionMultipleView = transform.Find("Viewport/Content/SurveyRecordMultiple").gameObject;
            m_SurveyQuestionRatingView = transform.Find("Viewport/Content/SurveyRecordRating").gameObject;
            m_SurveyQuestionLikertView = transform.Find("Viewport/Content/SurveyRecordLikert").gameObject;
            m_SurveyQuestionCustomView = transform.Find("Viewport/Content/SurveyRecordCustom").gameObject;
            m_PnlCompleteSurvey = transform.Find("PnlCompleteSurvey").gameObject;
            m_PnlPnlAlreadyCompleteSurvey = transform.Find("PnlCompleteSurvey").gameObject;

            m_TxtSurveyTitle = transform.parent.transform.Find("TopBar/TxtSurveyTitle").GetComponent<Text>();
            if (isNotHome) m_TxtSurveyStatus = transform.parent.transform.Find("TopBar/TxtSurveyStatus").GetComponent<Text>();

            m_BtnComplete = transform.Find("BtnComplete").GetComponent<Button>();
            m_BtnBackToHome = transform.Find("PnlCompleteSurvey/BtnHome").GetComponent<Button>();
            m_BtnGoToHistory = transform.Find("PnlAlreadyCompleteSurvey/BtnHistory").GetComponent<Button>();
            if (isNotHome) m_BtnDeleteSurvey = transform.Find("BtnDeleteSurvey").GetComponent<Button>();
            if (isNotHome) m_BtnPostSurvey = transform.Find("BtnPostSurvey").GetComponent<Button>();
            if (isNotHome) m_BtnActiveSurvey = transform.Find("BtnActiveSurvey").GetComponent<Button>();
            if (isNotHome) m_TxtOn = transform.Find("BtnActiveSurvey/TxtOn").GetComponent<Text>();
            if (isNotHome) m_TxtOff = transform.Find("BtnActiveSurvey/TxtOff").GetComponent<Text>();

            m_BtnComplete.onClick.AddListener(() => OnClickCompleteSurvey(id, userIdCreated));
            m_BtnBackToHome.onClick.AddListener(() => SNSurveyListControl.Api.ClickBackToHome());
            if (isNotHome) m_BtnDeleteSurvey.onClick.AddListener(DeleteSurvey);
            if (isNotHome) m_BtnPostSurvey.onClick.AddListener(() => PostSurvey(status));
            if (isNotHome) m_BtnActiveSurvey.onClick.AddListener(() => ActiveSurvey());

            m_InitViewList = new();
            m_QuestionContainer = m_SurveyQuestionRadioView.transform.parent;

            m_IsInit = true;
            if (isNotHome) m_TxtStat = m_TxtSurveyStatus.text;
        }

        foreach (var item in from Transform item in m_QuestionContainer
                             where item.gameObject.name.Contains("Clone")
                             select item)
        {
            Destroy(item.gameObject);
        }

        m_TxtSurveyTitle.text = title;

        Debug.Log("status " + status);
        if (isNotHome) GetStatus(status);
        m_TxtSurveyTitle.gameObject.SetActive(true);

        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), renderPage: (data) =>
        {
            RenderDetailPage(data, () =>
            {
                // Check if history pnl to show the answers
                if (isHistoryPnl)
                {
                    m_BtnActiveSurvey.gameObject.SetActive(false);
                    StartCoroutine(SNApiControl.Api.GetData<SurveyAnswerDTO>(string.Format(SNConstant.SURVEY_HISTORY_DETAIL, id), renderPage: (data) =>
                    {
                        string json = JsonConvert.SerializeObject(data);
                        Debug.Log(json);

                        List<AnswerResponseDTO> answerList = new List<AnswerResponseDTO>();

                        if (data != null && data.sections != null)
                        {
                            foreach (SectionDto section in data.sections)
                            {
                                if (section.questions != null)
                                {
                                    foreach (QuestionDto question in section.questions)
                                    {
                                        if (question.answers != null)
                                        {
                                            answerList.AddRange(question.answers);
                                        }
                                    }
                                }
                            }
                        }

                        SetAnswerQuestion(answerList);
                    }));
                }
            });
        }));
    }

    private void SetAnswerQuestion(List<AnswerResponseDTO> data)
    {
        if (data != null)
        {
            for (int i = 0; i < m_InitViewList.Count; i++)
            {
                Debug.Log("SET ANSWER");
                m_InitViewList[i].SetAnswer(data[i]);
            }
        }
    }

    private void ActiveSurvey()
    {
        // Handle call active survey
        SNControl.Api.ShowFAMPopup(m_TxtWarning, $"{m_TxtYouWant} {(m_Status != "Active" ? m_TxtOn.text : m_TxtOff.text)}", m_TxtYes, m_TxtNo, onConfirm: () =>
        {
            StartCoroutine(SNApiControl.Api.PatchData(string.Format(SNConstant.SURVEY_PATCH, m_Id), callback: () =>
            {
                if (m_Status == "Active")
                {
                    GetStatus("InActive");
                }
                else
                {
                    GetStatus("Active");
                }
            }));
        });
    }

    private void PostSurvey(string status)
    {
        switch (status)
        {
            case "Draft":
                SNControl.Api.ShowFAMPopup(m_TxtPostSurveyNow, $"{m_TxtYouWant} {m_TxtPurchasePackBeforePost}", m_TxtPurchasePack, m_TxtNo, onConfirm: () =>
                {
                    SNControl.Api.UnloadThenLoadScene(SNConstant.SCENE_BUNDLE);
                    DOVirtual.DelayedCall(0.2f, () => SNSurveyListControl.Api.OpenScenePacks(m_Id));
                });
                break;
            case "PackPurchased":
                SurveyPostDateDTO data = new()
                {
                    startDate = DateTime.Now,
                    expiredDate = DateTime.Now.AddDays(14)
                };

                Debug.Log("INNN " + status);

                SNControl.Api.ShowFAMPopup(m_TxtPostSurveyNow, $"{m_TxtYouWant} {m_TxtPostSurvey}", m_TxtConfirmPost, m_TxtNo, onConfirm: () =>
                {
                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.SURVEY_POST, m_Id), data, callback: () =>
                    {
                        GetStatus("Active");
                    }));
                });
                break;
            case "Active":
                GetStatus("Active");
                break;
            default:
                break;
        }
    }

    private void GetStatus(string status)
    {
        m_Status = status;

        string txtStatus = status switch
        {
            "Active" => m_TxtActive,
            "InActive" => m_TxtInActive,
            "Draft" => m_TxtDraft,
            "Expired" => m_TxtExpired,
            "PackPurchased" => m_TxtPackPurchased,
            _ => "N/A"
        };

        switch (status)
        {
            case "Active":
            case "InActive":
                m_BtnPostSurvey.gameObject.SetActive(false);
                m_BtnActiveSurvey.gameObject.SetActive(true);
                break;
            case "Draft":
            case "PackPurchased":
                m_BtnPostSurvey.gameObject.SetActive(true);
                m_BtnActiveSurvey.gameObject.SetActive(false);
                break;
            case "Expired":
                m_BtnPostSurvey.gameObject.SetActive(false);
                m_BtnActiveSurvey.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        m_BtnDeleteSurvey.gameObject.SetActive(status == "Draft");
        m_TxtOn.gameObject.SetActive(status == "InActive");
        m_TxtOff.gameObject.SetActive(status == "Active");

        m_TxtSurveyStatus.text = m_TxtStat + $"\n<b>{txtStatus}</b>";
    }

    private void DeleteSurvey()
    {
        if (m_Status == "Active")
        {
            SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtYouCannotDeleteAnySurveyThanDraft, m_TxtBack, "NotShow");
            return;
        }

        SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtDoYouSureDeleteSurvey, m_TxtConfirmDelete, m_TxtNo, onConfirm: () =>
        {
            StartCoroutine(SNApiControl.Api.DelItem(string.Format(SNConstant.SURVEY_DELETE, m_Id), callback: () =>
            {
                SNMainControl.Api.DeleteSurveyBackToMySurvey();
            }));
        });
    }

    private bool ValidateAnswers()
    {
        foreach (var view in m_InitViewList)
        {
            Debug.Log("GOO " + view.Validate());
            if (!view.Validate())
            {
                return false;
            }
        }
        return true;
    }

    private List<SNDoSurveyDTO.AnswerDTO> GetAllAnswers()
    {
        List<SNDoSurveyDTO.AnswerDTO> answers = new();
        foreach (var view in m_InitViewList)
        {
            answers.Add(view.GetAnswer());
        }
        return answers;
    }

    private void OnClickCompleteSurvey(int id, int userIdCreated)
    {
        if (!ValidateAnswers())
        {
            // POPUP WARNING SOMETHING WRONG
            Debug.Log("SURVEY NOT FINISH");
            SNControl.Api.ShowFAMPopup(m_TxtFail, m_TxtNotYetFinishSurvey, "Ok", "NotShow");
            return;
        }

        if (userIdCreated == PlayerPrefs.GetInt(SNConstant.USER_ID))
        {
            SNControl.Api.ShowFAMPopup(m_TxtFail, m_TxtYouCannotDoYourSurvey, "Ok", "NotShow");
            return;
        }

        SurveyDTO data = new SurveyDTO()
        {
            SurveyId = id,
            Answers = GetAllAnswers()
        };

        string postData = JsonConvert.SerializeObject(data);
        print("Data post: " + postData);

        // Show success finish survey after done
        StartCoroutine(SNApiControl.Api.PostData<SurveyDTO>(SNConstant.SURVEY_DO, data, callback: () =>
        {
            m_QuestionContainer.gameObject.SetActive(false);
            m_PnlCompleteSurvey.SetActive(true);
        }));
    }

    private void RenderDetailPage(SNSurveyQuestionDetailDTO data, Action callbackHistory = null)
    {
        List<SNSectionQuestionRowOptionDTO> options = new List<SNSectionQuestionRowOptionDTO>();
        List<SNSectionQuestionDTO> questions = new List<SNSectionQuestionDTO>();
        m_InitViewList.Clear();

        data?.sections?.ForEach(section =>
            {
                section?.questions?.ForEach(
                    question =>
                    {
                        SpawnQuestion(question, options, questions);
                    });
            });

        callbackHistory?.Invoke();
    }

    private void SpawnQuestion(SNSectionQuestionDTO question, List<SNSectionQuestionRowOptionDTO> options, List<SNSectionQuestionDTO> questions)
    {
        questions.Add(question);
        question?.rowOptions.ForEach(
            option =>
            {
                if (option.content != null) options.Add(option);
            });

        switch (question.type)
        {
            case "Text":
                GenerateMultipleQuestion(question, m_SurveyQuestionCustomView);
                break;
            case "Radio":
                GenerateMultipleQuestion(question, m_SurveyQuestionRadioView);
                break;
            case "CheckBox":
                GenerateMultipleQuestion(question, m_SurveyQuestionMultipleView);
                break;
            case "Selection":
                //GenerateMultipleQuestion(question, m_SurveyQuestionRadioView);
                break;
            case "Rating":
                GenerateMultipleQuestion(question, m_SurveyQuestionRatingView);
                break;
            case "Likert":
                GenerateMultipleQuestion(question, m_SurveyQuestionLikertView);
                break;
            default:
                break;
        }
    }

    private void GenerateMultipleQuestion(SNSectionQuestionDTO data, GameObject goPref)
    {
        GameObject go = Instantiate(goPref, m_QuestionContainer);
        SNInitView view = go.GetComponent<SNInitView>();
        m_InitViewList.Add(view);
        go.SetActive(true);
        view.Init(data);
    }
}
