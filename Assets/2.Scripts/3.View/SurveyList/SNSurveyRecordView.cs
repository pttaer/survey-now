using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyRecordView : MonoBehaviour
{
    private Button m_BtnDetail;
    private Text m_TxtTitle;
    private Text m_TxtPoints;
    private Text m_TxtDate;
    private Text m_TxtQuestionAmount;

    private void Start()
    {
        Init("none", "none", "none", "none"); // Test
    }

    public void Init(string title, string points, string date, string questionAmount)
    {
        m_BtnDetail = GetComponent<Button>();
        m_TxtTitle = transform.Find("TxtTitle").GetComponent<Text>();
        m_TxtPoints = transform.Find("TxtTitle/TxtPoints").GetComponent<Text>();
        m_TxtDate = transform.Find("Body/LeftSide/TxtDate").GetComponent<Text>();
        m_TxtQuestionAmount = transform.Find("Body/LeftSide/TxtQuestionAmount").GetComponent<Text>();

        m_BtnDetail.onClick.AddListener(OpenDetail);

        //Default Value
        m_TxtTitle.text = title;
        m_TxtPoints.text = points;
        m_TxtDate.text = date;
        m_TxtQuestionAmount.text = questionAmount;
    }

    private void OpenDetail()
    {
        SNSurveyListControl.Api.OpenSurveyDetail();
    }
}
