using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMenuIconView : MonoBehaviour
{
    private Color m_ActiveBtnColor;
    private Color m_DeactiveBtnColor;

    private Button m_Btn;
    private Image m_ImgBtnColor;
    private Text m_TxtLabel;
    private const string BTN_HOME = "BtnHome";

    // Start is called before the first frame update
    void Start()
    {
        m_ActiveBtnColor = new Color(0.91f, 0.91f, 0.91f); // #E8E8E8
        m_DeactiveBtnColor = new Color(0f, 0.69f, 0.31f); // #00B14F

        m_Btn = GetComponent<Button>();
        m_ImgBtnColor = GetComponent<Image>();
        m_TxtLabel = transform.Find("Group/TxtLabel").GetComponent<Text>();

        if (m_Btn.name == BTN_HOME) SetButtonColor("BtnHome"); 
    }

    public void SetButtonColor(string btnToSet)
    {
        if (btnToSet == "BtnLogout") return;

        bool isOn = true;
        m_Btn.interactable = true;

        if (btnToSet == gameObject.name)
        {
            isOn = false;
            m_Btn.interactable = false;
        }

        m_ImgBtnColor.color = isOn ? m_ActiveBtnColor : m_DeactiveBtnColor;
        m_TxtLabel.color = isOn ? Color.black : Color.white;
    }
}
