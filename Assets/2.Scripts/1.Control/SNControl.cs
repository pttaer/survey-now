//using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SNControl
{
    // Start is called before the first frame update
    private static SNControl m_api;

    public Action<bool> OnLoadShowLoading;
    public Action CheckingSchoolIdEvent;
    public Action<bool, string> OnLoadFailShowSorry;
    public Action<bool> OnCheckSchoolSetMain;

    private Action<string, string, string, string, Action, Action, Action> m_showSNPopupEvent; //notify show popup message

    public Action<string, string, string, string, Action, Action, Action> ShowSNPopupEvent
    {
        get { return m_showSNPopupEvent; }
        set { m_showSNPopupEvent = value; }
    }

    private Action<string, string, string, string, Action<string>, Action, Action, bool> m_showFAMPopupInputEvent; //notify show popup message

    public Action<string, string, string, string, Action<string>, Action, Action, bool> ShowFAMPopupInputEvent
    {
        get { return m_showFAMPopupInputEvent; }
        set { m_showFAMPopupInputEvent = value; }
    }

    public static SNControl Api
    {
        get
        {
            if (m_api == null)
            {
                m_api = new SNControl();
            }
            return m_api;
        }
    }

    public void Init()
    {
        //init other controls here
        PrefsUtils.Api = new PrefsUtils();
        SNMainControl.Api = new SNMainControl();
        SNMenuControl.Api = new SNMenuControl();
        SNSurveyListControl.Api = new SNSurveyListControl();
        SNCreateSurveyControl.Api = new SNCreateSurveyControl();
        SNBundleControl.Api = new SNBundleControl();
        SNApiControl.Api = new SNApiControl();
        SNDeeplinkControl.Api = new SNDeeplinkControl();
        PrefsUtils.Api = new();
        SNMainControl.Api = new();
        SNMenuControl.Api = new();
        SNSurveyListControl.Api = new();
        SNCreateSurveyControl.Api = new();
        SNApiControl.Api = new();
        SNSurveyLikertQuestionControl.Api = new();

        // Default value
        SNModel.Api.ScenesLoaded.Clear();
        UnloadThenLoadScene(SNConstant.SCENE_LOGIN);
        LoadScene(SNConstant.SCENE_MENU);

        Debug.Log("INIT");
    }
    #region UTILS

    //show hide loading
    //isShow: true = show, false = hide

    public void ShowLoading()
    {
        LoadShowLoading(true);
    }

    public void HideLoading()
    {
        LoadShowLoading(false);
    }

    private void LoadShowLoading(bool isShow)
    {
        OnLoadShowLoading?.Invoke(isShow);
    }

    public void ShowSorry(string txtShow = null)
    {
        LoadFailShowSorry(true, txtShow);
    }

    public void HideSorry(string txtShow = null)
    {
        LoadFailShowSorry(false, txtShow);
    }

    private void LoadFailShowSorry(bool isShow, string txtShow)
    {
        OnLoadFailShowSorry?.Invoke(isShow, txtShow);
    }

    public void ShowMain(bool isShow)
    {
        OnCheckSchoolSetMain?.Invoke(isShow);
    }

    public void ShowFAMPopup(string title, string content, string btnConfirmText, string btnElseText, Action onConfirm = null, Action onElse = null, Action onExit = null)
    {
        ShowSNPopupEvent?.Invoke(title, content, btnConfirmText, btnElseText, onConfirm, onElse, onExit);
    }

    public void ShowFAMPopup(string title, string content, string btnConfirmText, string btnElseText, Action<string> onConfirm, Action onElse = null, Action onExit = null, bool isShowInputField = false)
    {
        ShowFAMPopupInputEvent?.Invoke(title, content, btnConfirmText, btnElseText, onConfirm, onElse, onExit, isShowInputField);
    }

    public void UnloadThenLoadScene(string sceneToLoad)
    {
        // Unload the current scene & remove scenes loaded beside loadfirst
        List<string> sceneNames = SceneManager.GetAllScenes()
                                              .Select(scene => scene.name)
                                              .ToList();

        foreach (var sceneName in sceneNames)
        {
            if (sceneName != SNConstant.SCENE_LOADFIRST && sceneName != SNConstant.SCENE_MENU)
            {
                if (SNModel.Api.ScenesLoaded.Contains(sceneName) && SNModel.Api.ScenesLoaded.Contains(sceneToLoad))
                {
                    SNModel.Api.ScenesLoaded.Remove(sceneName);
                }
                UnLoadScene(sceneName);
            }
        }

        LoadScene(sceneToLoad);
    }

    // Load and add scene to scenes loaded list
    private void LoadScene(string sceneToLoad)
    {
        if (!SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            if (!SNModel.Api.ScenesLoaded.Contains(sceneToLoad))
            {
                SNModel.Api.ScenesLoaded.Add(sceneToLoad);
            }

#if UNITY_EDITOR
            foreach (var scene in SNModel.Api.ScenesLoaded)
            {
                Debug.Log("SCENE LOADED: " + scene);
            }
#endif
        }
    }

    private void UnLoadScene(string sceneToUnLoad)
    {
        if (SceneManager.GetSceneByName(sceneToUnLoad).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneToUnLoad);
        }
    }

    // For opening one or many panels in the set of panels of one scene
    public void OpenPanel(GameObject pnlToShow, List<GameObject> pnlList, bool isCheckToTurnPanelOff = false)
    {
        if (pnlToShow.activeSelf && isCheckToTurnPanelOff)
        {
            pnlToShow.SetActive(false);
            return;
        }

        foreach (var pnl in pnlList)
        {
            if (pnl.gameObject == pnlToShow)
            {
                pnl.gameObject.SetActive(true);
                continue;
            }
            pnl.SetActive(false);
        }
    }

    /*public void OpeManyPanel(List<GameObject> pnlList, bool isCheckToTurnPanelOff = false, params GameObject[] pnlsToShow)
    {
        pnlList.ForEach(panel => panel.SetActive(!pnlsToShow.Contains(panel)));
        pnlsToShow.ToList().ForEach(panelToShow => panelToShow.SetActive(true));

        if (isCheckToTurnPanelOff)
        {
            pnlList.Where(panel => !pnlsToShow.Contains(panel)).ToList().ForEach(panel => panel.SetActive(false));
        }
    }*/

    public void OpenManyPanel(List<GameObject> pnlList, List<GameObject> pnlsToShow)
    {
        pnlList.ForEach(panel => panel.SetActive(false));
        pnlsToShow.ForEach(panel => panel.SetActive(true));
    }

    #endregion UTILS
}