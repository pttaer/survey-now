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
    private Transform m_Viewport;

    private Text m_TxtNoHistory;

    private SNPointHistoryItem m_SpawnItem;

    private Dictionary<string, string> m_HistoryFiler = new Dictionary<string, string>()
    {
        {"Điểm đã mua", "PurchasePoint" },
        {"Dùng điền khảo sát", "DoSurvey" },
        {"Điểm tặng", "GiftPoint" },
        {"Điểm hoàn trả", "RefundPoint" },
        {"Điểm đổi tiền", "RedeemPoint" },
        {"Điểm mua gói", "PackPurchase" },
        {"Điểm được tặng", "ReceiveGift" }
    };

    public void Init()
    {
        m_DropdownFilter = transform.Find("Body/PnlSearchBar/DropdownFilter").GetComponent<Dropdown>();
        m_TxtNoHistory = transform.Find("Body/PnlItemsList/Viewport/Content/TxtNoHistory").GetComponent<Text>();
        m_SpawnItem = transform.Find("Body/PnlItemsList/Viewport/Content/PnlItems").GetComponent<SNPointHistoryItem>();
        m_Viewport = transform.Find("Body/PnlItemsList/Viewport").transform;

        m_DropdownFilter.onValueChanged.AddListener(delegate { FilterHistory(); });

        Invoke(nameof(DefaultValue), 0.5f);
    }

    private void DefaultValue()
    {
        List<string> optionsValue = new();
        foreach (var item in m_HistoryFiler)
        {
            optionsValue.Add(item.Key);
        }
        m_DropdownFilter.AddOptions(optionsValue);


        StartCoroutine(SNApiControl.Api.GetListData<SNPointsHistoryDTO>(SNConstant.POINTS_PURCHASE_HISTORY, null, RenderList));
    }
    private void FilterHistory()
    {
        ClearList();

        StartCoroutine(SNApiControl.Api.GetListData<SNPointsHistoryDTO>(SNConstant.POINTS_PURCHASE_HISTORY, new Dictionary<string, string>()
        {
            {"type", m_HistoryFiler.GetValueOrDefault(m_DropdownFilter.options[m_DropdownFilter.value].text) }
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

        float viewHeight = data.Length == 0 ? 0f : 300f;

        m_Viewport.GetComponent<LayoutElement>().preferredHeight = viewHeight;
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
