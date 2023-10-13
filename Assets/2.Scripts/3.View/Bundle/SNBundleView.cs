using System;
using UnityEngine;
using UnityEngine.UI;

public class SNBundleView : MonoBehaviour
{
    private Button m_btnMenu;
    private GameObject m_BundleItems;

    private SNBundleItemView m_SpawnItem;
    private SNBundleFormView m_FormView;
    private SNBundleNoticeView m_NoticeView;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_btnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_SpawnItem = transform.Find("Body/Scroll View/Viewport/Content/PnlBundle").GetComponent<SNBundleItemView>();
        m_FormView = transform.Find("Body/PopupForm").GetComponent<SNBundleFormView>();
        m_NoticeView = transform.Find("Body/PopupNotice").GetComponent<SNBundleNoticeView>();

        m_btnMenu.onClick.AddListener(OnClickOpenMenu);

        m_FormView.Init();

        SNBundleControl.Api.onOpenPopupForm += OpenPopupForm;
        SNBundleControl.Api.onOpenPopupNotice += OpenPopupNotice;

        DefaultValue();

    }

    private void OnDestroy()
    {
        SNBundleControl.Api.onOpenPopupForm -= OpenPopupForm;
        SNBundleControl.Api.onOpenPopupNotice -= OpenPopupNotice;
    }

    private void DefaultValue()
    {
        StartCoroutine(SNApiControl.Api.GetListData<SNPacks>(SNConstant.PACKS_ALL, null, RenderList));
    }

    private void RenderList(SNPacks[] data)
    {
        foreach (SNPacks item in data)
        {
            SNBundleItemView obj = Instantiate(m_SpawnItem, m_SpawnItem.transform.parent);
            obj.Init(item.name, item.packType, item.benefits);
            obj.gameObject.SetActive(true);
        }
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }

    private void OpenPopupForm(string packtype)
    {
        m_FormView.gameObject.SetActive(true);
    }

    private void OpenPopupNotice(string remaindingAmount)
    {
        m_NoticeView.Init(remaindingAmount);
        m_NoticeView.gameObject.SetActive(true);
    }
}
