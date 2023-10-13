using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SNSurveyListMySurveyView : MonoBehaviour
{
    private List<SNSurveyRecordView> m_SurveyRecordList;
    private SNSurveyRecordView m_SurveyRecordPrefab;

    public void InitHome()
    {
        StartCoroutine(SNApiControl.Api.GetListData<SNSurveyResponseDTO>(SNConstant.SURVEY_GET_HOME, null, RenderPage));
        m_SurveyRecordList = new();
        m_SurveyRecordPrefab = transform.parent.transform.Find("SpawnItem/SurveyRecord").GetComponent<SNSurveyRecordView>();
    }

    public void InitMySurvey()
    {
        StartCoroutine(SNApiControl.Api.GetListData<SNSurveyResponseDTO>(SNConstant.SURVEY_GET_MY_SURVEY, renderPage: RenderPage));
        m_SurveyRecordList = new();
        m_SurveyRecordPrefab = transform.parent.transform.Find("SpawnItem/SurveyRecord").GetComponent<SNSurveyRecordView>();
    }

    private void RenderPage<T>(T[] datas)
    {
        foreach (var data in datas)
        {
            bool isAlreadyOk = false;
            foreach (var prefab in m_SurveyRecordList)
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
                if (data is SNSurveyResponseDTO newsData)
                {
                    if (SceneManager.GetSceneByName(SNConstant.SCENE_HOME).isLoaded && newsData.Status != "Active")
                    {
                        continue;
                    }
                }

                GameObject go = Instantiate(m_SurveyRecordPrefab.gameObject, transform.Find("Viewport/Content"));
                RenderItem(data, go.GetComponent<SNSurveyRecordView>());
            }
        }
    }

    private void RenderItem<T>(T data, SNSurveyRecordView view)
    {
        view.gameObject.SetActive(true);
        if (!m_SurveyRecordList.Contains(view))
        {
            m_SurveyRecordList.Add(view);
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
