using System;
using UnityEngine;
using UnityEngine.UI;

public class SNPnlEmptyView : MonoBehaviour
{
    private Text m_TxtEmpty;
    private Text m_TxtBtn;
    private Button m_Btn;

    private bool m_IsInited;

    // Override txts
    [SerializeField] string m_EmptyTxt;
    [SerializeField] string m_BtnTxt;

    public void Init(string emptyTxt = null, string btnTxt = null, Action onClickCallback = null)
    {
        if (!m_IsInited)
        {
            FindComponents();
        }

        m_TxtEmpty.text = string.IsNullOrEmpty(m_EmptyTxt) ? m_TxtEmpty.text : m_EmptyTxt;
        m_TxtEmpty.text = string.IsNullOrEmpty(emptyTxt) ? m_TxtEmpty.text : emptyTxt;

        m_TxtBtn.text = string.IsNullOrEmpty(m_BtnTxt) ? m_TxtBtn.text : m_BtnTxt;
        m_TxtBtn.text = string.IsNullOrEmpty(btnTxt) ? m_TxtBtn.text : btnTxt;

        m_Btn.onClick.RemoveAllListeners();
        m_Btn.onClick.AddListener(() => onClickCallback?.Invoke());

        m_Btn.gameObject.SetActive(onClickCallback != null);
        gameObject.SetActive(true);
    }

    private void FindComponents()
    {
        m_TxtEmpty = transform.Find("TxtEmpty")?.GetComponent<Text>();
        m_TxtBtn = transform.Find("Btn/TxtBtn")?.GetComponent<Text>();
        m_Btn = transform.Find("Btn")?.GetComponent<Button>();

        if (m_TxtEmpty == null || m_TxtBtn == null || m_Btn == null)
        {
            Debug.LogError("One or more required components are not assigned in the inspector!");
            if (m_TxtEmpty == null) Debug.LogError("- TxtEmpty component is missing. -");
            if (m_TxtBtn == null) Debug.LogError("- TxtBtn component is missing. -");
            if (m_Btn == null) Debug.LogError("- Btn component is missing. -");
            return;
        }
        m_IsInited = true;
    }
}