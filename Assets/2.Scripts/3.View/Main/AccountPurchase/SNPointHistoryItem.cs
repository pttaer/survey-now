using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNPointHistoryItem : MonoBehaviour
{
    private Text m_TxtDate;
    private Text m_TxtPoints;

    public void Init(string date, string points)
    {
        m_TxtDate = transform.Find("ColLabel/TxtLeft").GetComponent<Text>();
        m_TxtPoints = transform.Find("ColLabel/TxtRight").GetComponent<Text>();

        DefaultValue(date, points);
    }

    private void DefaultValue(string date, string points)
    {
        m_TxtDate.text = date;
        m_TxtPoints.text = points;
    }
}
