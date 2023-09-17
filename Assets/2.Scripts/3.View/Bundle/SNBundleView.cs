using UnityEngine;
using UnityEngine.UI;

public class SNBundleView : MonoBehaviour
{
    private Button m_btnMenu;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        m_btnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();

        m_btnMenu.onClick.AddListener(OnClickOpenMenu);
    }

    private void OnClickOpenMenu()
    {
        SNMainControl.Api.OpenMenuPnl();
    }
}
