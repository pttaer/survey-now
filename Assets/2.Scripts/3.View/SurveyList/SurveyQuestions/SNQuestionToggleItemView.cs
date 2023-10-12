using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SNQuestionToggleItemView : MonoBehaviour
{
    private Toggle m_Tgl;
    private Text m_TxtLabel;

    private int m_ItemOrder;

    public void Init(SNSectionQuestionRowOptionDTO data)
    {
        if (data == null) return;

        m_Tgl = GetComponent<Toggle>();
        m_TxtLabel = transform.Find("TxtLabel").GetComponent<Text>();

        m_TxtLabel.text = data.content;

        gameObject.SetActive(true);
        m_Tgl.SetIsOnWithoutNotify(false);
    }

    public void Init(SNSectionQuestionColumnOptionDTO data)
    {
        if (data == null) return;
        m_ItemOrder = data.order;

        m_Tgl = GetComponent<Toggle>();
        m_TxtLabel = transform.Find("TxtLabel").GetComponent<Text>();

        m_TxtLabel.text = data.content;

        gameObject.SetActive(true);
        m_Tgl.SetIsOnWithoutNotify(false);
    }

    public bool IsTglOn()
    {
        return m_Tgl.isOn;
    }

    public int GetColumnOrder()
    {
        return m_ItemOrder;
    }
}
