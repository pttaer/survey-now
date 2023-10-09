using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNPointHistory : MonoBehaviour
{
    private InputField m_IpfSearch;
    private Button m_BtnSearch;

    private Text m_TxtNoHistory;

    private SNPointHistoryItem m_SpawnItem;

    public void Init()
    {
        m_IpfSearch = transform.Find("Body/PnlSearchBar/IpfSearch").GetComponent<InputField>();
        m_IpfSearch = transform.Find("Body/PnlSearchBar/BtnSearch").GetComponent<InputField>();

        m_TxtNoHistory = transform.Find("Body/PnlItemsList/TxtNoHistory").GetComponent<Text>();
        m_SpawnItem = transform.Find("Body/PnlItemsList/PnlItems").GetComponent<SNPointHistoryItem>();

        DefaultValue();
    }

    private void DefaultValue()
    {
        Debug.Log("right here");
        StartCoroutine(SNApiControl.Api.GetListData<SNPointsHistoryDTO>(SNConstant.POINTS_PURCHASE_HISTORY, SNConstant.METHOD_GET, RenderList));
    }

    private void RenderList(SNPointsHistoryDTO[] data)
    {
        foreach (SNPointsHistoryDTO item in data)
        {
            m_SpawnItem.Init(item.date, item.point.ToString());
            Instantiate(m_SpawnItem, m_SpawnItem.transform.parent);
            m_SpawnItem.gameObject.SetActive(true);
        }

        m_TxtNoHistory.gameObject.SetActive(data.Length == 0 ? true : false);
    }
}
