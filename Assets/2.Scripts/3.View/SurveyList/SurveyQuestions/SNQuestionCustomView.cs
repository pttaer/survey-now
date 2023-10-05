using UnityEngine;
using UnityEngine.UI;

public class SNQuestionCustomView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private InputField m_IpfAnswer;

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_IpfAnswer = transform.Find("ToggleGr/IpfAnswer").GetComponent<InputField>();

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);
    }

    public string GetAnswer()
    {
        return m_IpfAnswer.text;
    }
}
