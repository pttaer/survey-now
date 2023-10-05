using UnityEngine;
using UnityEngine.UI;

public class SNMainRecordDetailView : MonoBehaviour
{
    private Text m_TxtPoints;
    private Text m_TxtMoney;
    private Text m_TxtDate;

    private GameObject m_GoMomo;
    private GameObject m_GoBank;

    private int m_Points;
    private const int TEN = 10;

    public void Init(string date, string points)
    {
        m_TxtPoints = transform.Find("Body/RightSide/TxtPoints").GetComponent<Text>();
        m_TxtMoney = transform.Find("Body/RightSide/TxtMoney").GetComponent<Text>();
        m_TxtDate = transform.Find("Body/RightSide/TxtDate").GetComponent<Text>();

        m_GoMomo = transform.Find("Body/RightSide/Momo").gameObject;
        m_GoBank = transform.Find("Body/RightSide/Bank").gameObject;

        m_TxtPoints.text = points;

        int.TryParse(points, out m_Points);
        m_TxtMoney.text = $"{m_Points * TEN} VND";

        m_TxtDate.text = date;

        m_GoMomo.SetActive(false);
        m_GoBank.SetActive(false);
    }
}
