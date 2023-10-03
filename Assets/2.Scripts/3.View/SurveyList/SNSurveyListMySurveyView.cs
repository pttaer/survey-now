using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSurveyListMySurveyView : MonoBehaviour
{
    private List<SNSurveyRecordView> m_NewsAndEventsPrefabList;
    private SNSurveyRecordView m_NewsAndEventsPrefab;

    public void Init()
    {
        StartCoroutine(SNApiControl.Api.GetListData<SNSurveyResponseDTO>(SNConstant.SURVEY_GET_ALL, RenderPage));

        m_NewsAndEventsPrefab = transform.parent.transform.Find("SpawnItem/SurveyRecord").GetComponent<SNSurveyRecordView>();
    }

    private void RenderPage<T>(T[] datas)
    {
        foreach (var data in datas)
        {
            bool isAlreadyOk = false;
            foreach (var prefab in m_NewsAndEventsPrefabList)
            {
                if (!prefab.gameObject.activeInHierarchy)
                {
                    RenderItem(data, prefab);
                    isAlreadyOk = true;
                    break;
                }
            }
            if (!isAlreadyOk)
            {
                GameObject go = Instantiate(m_NewsAndEventsPrefab.gameObject, transform.Find("Viewport/Content"));
                RenderItem(data, go.GetComponent<SNSurveyRecordView>());
            }
        }
    }

    private void RenderItem<T>(T data, SNSurveyRecordView view)
    {
        view.gameObject.SetActive(true);
        if (!m_NewsAndEventsPrefabList.Contains(view))
        {
            m_NewsAndEventsPrefabList.Add(view);
        }

        if (data is SNSurveyResponseDTO newsData)
        {
            view.Init(newsData);
        }
        else
        {
            // Handle unsupported data type
            view.gameObject.SetActive(false);
        }
    }
}
