using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SNSurveyRecordView : MonoBehaviour
{
    private Button m_BtnDetail;
    private Text m_TxtTitle;
    private Text m_TxtPoints;
    private Text m_TxtDate;
    private Text m_TxtQuestionAmount;
    private SNSurveyResponseDTO m_Data;

    public void Init(SNSurveyResponseDTO data)
    {
        var topBar = transform.Find("TopBar");
        var body = transform.Find("Body/LeftSide");

        m_BtnDetail = GetComponent<Button>();

        m_TxtTitle = topBar.Find("TxtTitle").GetComponent<Text>();
        m_TxtPoints = topBar.Find("TxtPoints").GetComponent<Text>();
        m_TxtDate = body.Find("TxtDate").GetComponent<Text>();
        m_TxtQuestionAmount = body.Find("TxtQuestionAmount").GetComponent<Text>();

        m_BtnDetail.onClick.AddListener(OpenDetail);

        //Default Value
        m_TxtTitle.text = data.Title;
        m_TxtPoints.text = data.Point.ToString() + m_TxtPoints.text;
        m_TxtDate.text = data.StartDate + " - " + data.ExpiredDate;
        m_TxtQuestionAmount.text = data.TotalQuestion.ToString() + m_TxtQuestionAmount.text;

        m_Data = data;
    }

    private void OpenDetail()
    {
        SNSurveyListControl.Api.OpenSurveyDetail(m_Data);
    }
}
