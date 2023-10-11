using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SNDoSurveyDTO;

public class SNQuestionMultipleView : SNInitView
{
    private Text m_TxtOrder;
    private Text m_Title;
    private GameObject m_RequireMark;

    private GameObject m_ToggleItemPref;
    private ToggleGroup m_TglGroup;

    private List<SNQuestionToggleItemView> m_ItemViewList;
    private int m_QuestionId;

    public override void Init(SNSectionQuestionDTO data)
    {
        if (data == null) return;
        m_QuestionId = data.id;

        m_TxtOrder = transform.Find("TopBar/TxtOrder").GetComponent<Text>();
        m_Title = transform.Find("TopBar/TxtTitle").GetComponent<Text>();
        m_RequireMark = transform.Find("TopBar/RequireMark").gameObject;

        m_ToggleItemPref = transform.Find("ToggleGr/ToggleItem").gameObject;
        m_TglGroup = transform.Find("ToggleGr").GetComponent<ToggleGroup>();

        m_TxtOrder.text = data.order.ToString();
        m_Title.text = data.title;
        m_RequireMark.SetActive(data.isRequire);

        m_ItemViewList = new List<SNQuestionToggleItemView>();

        foreach (var row in data?.rowOptions)
        {
            if (!string.IsNullOrEmpty(row.content))
            {
                GenerateQuestionChoices(row);
            }
        }

        m_TglGroup.allowSwitchOff = true;
        m_TglGroup.SetAllTogglesOff();
    }

    private void GenerateQuestionChoices(SNSectionQuestionRowOptionDTO data)
    {
        GameObject go = Instantiate(m_ToggleItemPref, m_TglGroup.transform);
        SNQuestionToggleItemView view = go.GetComponent<SNQuestionToggleItemView>();
        m_ItemViewList.Add(view);
        view.Init(data);
    }

    public override AnswerDTO GetAnswer()
    {
        List<AnswerOptionDTO> answerOptions = new();

        foreach (SNQuestionToggleItemView view in m_ItemViewList)
        {
            if (view.IsTglOn())
            {
                AnswerOptionDTO answer = new()
                {
                    RowOrder = m_ItemViewList.IndexOf(view),
                    ColumnOrder = null,
                    Content = null
                };
                answerOptions.Add(answer);
            }
        }

        return new AnswerDTO()
        {
            QuestionId = m_QuestionId,
            Content = null,
            RateNumber = null,
            AnswerOptions = answerOptions
        };
    }

    public override bool Validate()
    {
        bool answerSelected = false;

        foreach (var item in m_ItemViewList)
        {
            if (item.IsTglOn())
            {
                return true;
            }
        }

        return answerSelected;
    }
}
