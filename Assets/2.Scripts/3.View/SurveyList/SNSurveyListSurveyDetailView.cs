using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static SNDoSurveyDTO;
using Newtonsoft.Json;

public class SNSurveyListSurveyDetailView : MonoBehaviour
{
    private GameObject m_SurveyQuestionRadioView;
    private GameObject m_SurveyQuestionMultipleView;
    private GameObject m_SurveyQuestionRatingView;
    private GameObject m_SurveyQuestionLikertView;
    private GameObject m_SurveyQuestionCustomView;

    private Button m_BtnComplete;

    private Transform m_QuestionContainer;
    private List<SNInitView> m_InitViewList;

    private bool m_IsInit = false;

    public void Init(int id)
    {
        Debug.Log("ID " + id);

        if (!m_IsInit)
        {
            m_SurveyQuestionRadioView = transform.Find("Viewport/Content/SurveyRecordRadio").gameObject;
            m_SurveyQuestionMultipleView = transform.Find("Viewport/Content/SurveyRecordMultiple").gameObject;
            m_SurveyQuestionRatingView = transform.Find("Viewport/Content/SurveyRecordRating").gameObject;
            m_SurveyQuestionLikertView = transform.Find("Viewport/Content/SurveyRecordLikert").gameObject;
            m_SurveyQuestionCustomView = transform.Find("Viewport/Content/SurveyRecordCustom").gameObject;

            m_BtnComplete = transform.Find("BtnComplete").GetComponent<Button>();
            m_BtnComplete.onClick.AddListener(() => OnClickCompleteSurvey(id));

            m_InitViewList = new();
            m_QuestionContainer = m_SurveyQuestionRadioView.transform.parent;

            m_IsInit = true;
        }

        foreach (var item in from Transform item in m_QuestionContainer
                             where item.gameObject.name.Contains("Clone")
                             select item)
        {
            Destroy(item.gameObject);
        }

        StartCoroutine(SNApiControl.Api.GetData<SNSurveyQuestionDetailDTO>(string.Format(SNConstant.SURVEY_GET_DETAIL, id), RenderDetailPage));
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

        //StartCoroutine(SNApiControl.Api.PostData<SurveyDTO>(SNConstant.SURVEY_DO, data));
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
