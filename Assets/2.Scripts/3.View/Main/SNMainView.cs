using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainView : MonoBehaviour
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
        print("v");
        SNMainControl.Api.OpenMenuPnl();
    }
}
