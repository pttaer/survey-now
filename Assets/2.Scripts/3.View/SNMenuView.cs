using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMenuView : MonoBehaviour
{
    private Button m_btnMenu;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_btnMenu = transform.Find("TopBar/BtnMenu").GetComponent<Button>();

        m_btnMenu.onClick.AddListener(CloseMenu);

        SNMainControl.Api.onClickMenu += OpenMenu;
    }

    private void OnDestroy()
    {
        SNMainControl.Api.onClickMenu -= OpenMenu;
    }

    private void OpenMenu()
    {
        print("aaaa");
    }

    private void CloseMenu()
    {
        print("bbbb");
    }
}
