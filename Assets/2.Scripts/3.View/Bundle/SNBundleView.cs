using UnityEngine;
using UnityEngine.UI;

public class SNBundleView : MonoBehaviour
{
    private Button m_btnMenu;
    private GameObject m_BundleItems;

    private SNBundleItemView[] m_BundleItemList;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_btnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();
        m_BundleItems = transform.Find("Body/Viewport/Content").gameObject;

        m_btnMenu.onClick.AddListener(OnClickOpenMenu);

        RefItems();
    }

    private void RefItems()
    {
        m_BundleItemList = m_BundleItems.GetComponentsInChildren<SNBundleItemView>();

        foreach (SNBundleItemView item in m_BundleItemList)
        {
            item.Init();
        }
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
