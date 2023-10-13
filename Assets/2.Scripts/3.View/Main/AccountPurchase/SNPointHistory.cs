using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNPointHistory : MonoBehaviour
{
    private InputField m_IpfSearch;
    private Dropdown m_DropdownFilter;
    private Button m_BtnSearch;

    private Text m_TxtNoHistory;

    private SNPointHistoryItem m_SpawnItem;

    public void Init()
    {
        m_DropdownFilter = transform.Find("Body/PnlSearchBar/DropdownFilter").GetComponent<Dropdown>();
        m_TxtNoHistory = transform.Find("Body/PnlItemsList/TxtNoHistory").GetComponent<Text>();
        m_SpawnItem = transform.Find("Body/PnlItemsList/PnlItems").GetComponent<SNPointHistoryItem>();

        m_DropdownFilter.onValueChanged.AddListener(delegate { FilterHistory(); });

        DefaultValue();
    }

    private void DefaultValue()
    {
        StartCoroutine(SNApiControl.Api.GetListData<SNPointsHistoryDTO>(SNConstant.POINTS_PURCHASE_HISTORY, null, RenderList));
    }
    private void FilterHistory()
    {
        ClearList();

        StartCoroutine(SNApiControl.Api.GetListData<SNPointsHistoryDTO>(SNConstant.POINTS_PURCHASE_HISTORY, new Dictionary<string, string>()
        {
            {"type", m_DropdownFilter.options[m_DropdownFilter.value].text }
        }, RenderList));
    }

    private void RenderList(SNPointsHistoryDTO[] data)
    {
        foreach (SNPointsHistoryDTO item in data)
        {
            SNPointHistoryItem obj = Instantiate(m_SpawnItem, m_SpawnItem.transform.parent);
            obj.Init(item.date, item.point.ToString());
            obj.gameObject.SetActive(true);
        }

        m_TxtNoHistory.gameObject.SetActive(data.Length == 0 ? true : false);
    }

    private void ClearList()
    {
        foreach (Transform item in m_SpawnItem.transform.parent)
        {
            if (item.name == "PnlItems(Clone)")
            {
                Destroy(item.gameObject);
            }
        }
    }
}
