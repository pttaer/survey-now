using UnityEngine;
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
        m_BtnDetail = GetComponent<Button>();
        m_TxtTitle = transform.Find("TxtTitle").GetComponent<Text>();
        m_TxtPoints = transform.Find("TxtTitle/TxtPoints").GetComponent<Text>();
        m_TxtDate = transform.Find("Body/LeftSide/TxtDate").GetComponent<Text>();
        m_TxtQuestionAmount = transform.Find("Body/LeftSide/TxtQuestionAmount").GetComponent<Text>();

        m_BtnDetail.onClick.AddListener(OpenDetail);

        //Default Value
        m_TxtTitle.text = data.Title;
        m_TxtPoints.text = data.Point.ToString();
        m_TxtDate.text = data.StartDate;
        m_TxtQuestionAmount.text = data.TotalQuestion.ToString();

        m_Data = data;
    }

    private void OpenDetail()
    {
        SNSurveyListControl.Api.OpenSurveyDetail(m_Data);
    }
}
