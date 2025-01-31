using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSurveyQuestionBaseView : MonoBehaviour
{
    private Text m_TxtOrder;
    private Toggle m_TglRequire;
    private Button m_BtnDelete; // Re-arrage question order again

    private int m_QuestionOrder;

    public void InitBase(int order)
    {
        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_TglRequire = transform.Find("BtnGroup/TglRequire").GetComponent<Toggle>();
        m_BtnDelete = transform.Find("BtnGroup/BtnDelete").GetComponent<Button>();

        m_BtnDelete.onClick.AddListener(DeleteItem);
        SetOrder(order);
    }

    public virtual void Init()
    {
        // Base implementation of the Init() method
        // You can provide a default implementation here or leave it empty
    }

    public virtual SNSectionQuestionRequestDTO GetQuestionData()
    {
        // Base implementation of the Init() method
        // You can provide a default implementation here or leave it empty
        return new SNSectionQuestionRequestDTO();
    }

    private void DeleteItem()
    {
        Destroy(gameObject);
        // Call reorder
        SNCreateSurveyControl.Api.DeleteItemReOrderQuestionList(gameObject);
    }

    public void SetOrder(int order)
    {
        if (m_TxtOrder != null)
        {
            m_TxtOrder.text = $"{order}.";
            m_QuestionOrder = order;
        }
        else
        {
            Debug.LogWarning("m_TxtOrder is null. Cannot set the order.");
        }
    }

    public int GetOrder()
    {
        return m_QuestionOrder;
    }

    public bool GetRequire()
    {
        return m_TglRequire.isOn;
    }
}
