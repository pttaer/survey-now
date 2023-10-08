using UnityEngine;
using UnityEngine.UI;

public class SNMainPopupView : MonoBehaviour
{
    private Button m_BtnClose;

    void Start()
    {
        m_BtnClose = transform.Find("BtnClose").GetComponent<Button>();

        m_BtnClose.onClick.AddListener(() => {
            transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }
}
