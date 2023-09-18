using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainRecordItemView : MonoBehaviour
{
    private Button m_BtnRecord;
    private Text m_TxtDate;
    private Text m_TxtPoints;

    private SNHistoryRecordType m_TypeRecord;

    // There will be one more that decice MOMO or BANK being used

    private string m_Date;
    private string m_Points;

    public void Init(SNHistoryRecordType type, string date, string points)
    {
        m_BtnRecord = GetComponent<Button>();
        m_TxtDate = transform.Find("").GetComponent<Text>();
        m_TxtPoints = transform.Find("").GetComponent<Text>();

        m_BtnRecord.onClick.AddListener(CallDetail);

        m_TxtDate.text = date;
        m_TxtPoints.text = points;
    }

    private void CallDetail()
    {
        SNMainControl.Api.CallHistoryRecordDetail(m_TypeRecord, m_Date, m_Points);
    }
}
