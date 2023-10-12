using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static SNDoSurveyDTO;
using Newtonsoft.Json;
using System;

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

    private Transform m_QuestionContainer;
    private List<SNInitView> m_InitViewList;

    private bool m_IsInit = false;
    private int m_Id;
    private string m_TxtStat;

    [SerializeField] string m_TxtWarning;
    [SerializeField] string m_TxtDoYouSureDeleteSurvey;
    [SerializeField] string m_TxtConfirm;
    [SerializeField] string m_TxtNo;

    [SerializeField] string m_TxtActive;
    [SerializeField] string m_TxtInActive;
    [SerializeField] string m_TxtDraft;
    [SerializeField] string m_TxtExpired;
    [SerializeField] string m_TxtPackPurchased;

    public void Init(int id, string title, string status)
    {
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
            m_TxtSurveyStatus = transform.parent.transform.Find("TopBar/TxtSurveyStatus").GetComponent<Text>();

            m_BtnComplete = transform.Find("BtnComplete").GetComponent<Button>();
            m_BtnBackToHome = transform.Find("PnlCompleteSurvey/BtnHome").GetComponent<Button>();
            m_BtnGoToHistory = transform.Find("PnlAlreadyCompleteSurvey/BtnHistory").GetComponent<Button>();
            m_BtnDeleteSurvey = transform.Find("BtnDeleteSurvey").GetComponent<Button>();

            m_BtnComplete.onClick.AddListener(() => OnClickCompleteSurvey(id));
            m_BtnBackToHome.onClick.AddListener(() => SNSurveyListControl.Api.ClickBackToHome());
            m_BtnDeleteSurvey.onClick.AddListener(DeleteSurvey);

            m_InitViewList = new();
            m_QuestionContainer = m_SurveyQuestionRadioView.transform.parent;

            m_IsInit = true;
            m_TxtStat = m_TxtSurveyStatus.text;
        }

        foreach (var item in from Transform item in m_QuestionContainer
                             where item.gameObject.name.Contains("Clone")
                             select item)
        {
            Destroy(item.gameObject);
        }

        m_TxtSurveyTitle.text = title;
        m_TxtSurveyStatus.text = GetStatus(status);
        m_TxtSurveyTitle.gameObject.SetActive(true);

        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), RenderDetailPage));
    }

    private string GetStatus(string status)
    {
        string txtStatus = status switch
        {
            "Active" => m_TxtActive,
            "InActive" => m_TxtInActive,
            "Draft" => m_TxtDraft,
            "Expired" => m_TxtExpired,
            "PackPurchased" => m_TxtPackPurchased,
            _ => "N/A"
        };

        return m_TxtStat + $"\n<b>{txtStatus}</b>";
    }

    private void DeleteSurvey()
    {
        SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtDoYouSureDeleteSurvey, m_TxtConfirm, m_TxtNo, onConfirm: () =>
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

    private List<AnswerDTO> GetAllAnswers()
    {
        List<AnswerDTO> answers = new();
        foreach (var view in m_InitViewList)
        {
            answers.Add(view.GetAnswer());
        }
        return answers;
    }

    private void OnClickCompleteSurvey(int id)
    {
        if (!ValidateAnswers())
        {
            // POPUP WARNING SOMETHING WRONG
            Debug.Log("SURVEY NOT FINISH");
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

    private void RenderDetailPage(SNSurveyQuestionDetailDTO data)
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
