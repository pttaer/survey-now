using DG.Tweening;
//using Firebase.Auth;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SNControl
{
    //private FirebaseAuth auth = FirebaseAuth.DefaultInstance;
    private string googleIdToken = "529134634305-oin934ukcggp0qspi786jqgvsoqq52o1.apps.googleusercontent.com";
    private string googleAccessToken = "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";

    // Start is called before the first frame update
    private static SNControl m_api;

    public Action<bool> OnLoadShowLoading;
    public Action CheckingSchoolIdEvent;
    public Action<bool, string> OnLoadFailShowSorry;
    public Action<bool> OnCheckSchoolSetMain;

    private Action<string, string, string, string, Action, Action, Action> m_showFAMPopupEvent; //notify show popup message

    public Action<string, string, string, string, Action, Action, Action> ShowFAMPopupEvent
    {
        get { return m_showFAMPopupEvent; }
        set { m_showFAMPopupEvent = value; }
    }

    private Action<string, string, string, string, Action<string>, Action, Action, bool> m_showFAMPopupInputEvent; //notify show popup message

    public Action<string, string, string, string, Action<string>, Action, Action, bool> ShowFAMPopupInputEvent
    {
        get { return m_showFAMPopupInputEvent; }
        set { m_showFAMPopupInputEvent = value; }
    }

    private string[] unloadScenes = new string[]
    {
        SNConstant.SCENE_MAIN,
        SNConstant.SCENE_MENU,
    };

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

        //merge language general object folder
        /*TextAsset goLanguage = ResourceObject.GetResource<TextAsset>(HAGOConstant.PATH_LANGUAGE);
        I18N.instance.MergeMoreData(goLanguage.text, false);*/

        //if (PlayerPrefs.HasKey(SNConstant.BEARER_TOKEN_CACHE))
        //{
        //    CheckingSchoolIdEvent?.Invoke();
        //}
        //else
        //{
        //    LoadScene(SNConstant.SCENE_LOGIN);
        //}
    }

    public void UnloadAllScenes()
    {
        foreach (string scene in unloadScenes)
        {
            UnLoadScene(scene);
        }
    }

    // Unload all scene in the currentScenes array and load main and menu
    public void BackToMainScene(bool unloadAll = false, params string[] scenesToUnload)
    {
        if (!unloadAll)
        {
            foreach (string scene in scenesToUnload)
            {
                UnLoadScene(scene);
            }
        }
        else
        {
            UnloadAllScenes();
        }

        LoadScene(SNConstant.SCENE_MAIN);
        LoadScene(SNConstant.SCENE_MENU);
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
        ShowFAMPopupEvent?.Invoke(title, content, btnConfirmText, btnElseText, onConfirm, onElse, onExit);
    }

    public void ShowFAMPopup(string title, string content, string btnConfirmText, string btnElseText, Action<string> onConfirm, Action onElse = null, Action onExit = null, bool isShowInputField = false)
    {
        ShowFAMPopupInputEvent?.Invoke(title, content, btnConfirmText, btnElseText, onConfirm, onElse, onExit, isShowInputField);
    }

    public void UnloadAllThenLoadScene(string[] sceneName)
    {
        UnloadAllScenes();

        foreach (string scene in sceneName)
        {
            LoadScene(scene);
        }
    }

    // Need about 0.5sec to load
    public void LoadScene(string sceneName)
    {
        HideSorry();
        if (SNModel.Api.IsUnloadingScene)
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                LoadScene(sceneName);
            });
            return;
        }

        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            DOVirtual.DelayedCall(0.025f, () =>
            {
                Scene targetScene = SceneManager.GetSceneByName(sceneName);

                GameObject[] rootObjects = targetScene.GetRootGameObjects();

                if (rootObjects.Length > 0)
                {
                    Transform sceneContainer = rootObjects[0].transform;
                    CanvasGroup sceneCanvasGroup = sceneContainer.GetComponent<CanvasGroup>();
                    TweenUtils.TweenFadeIn(sceneCanvasGroup, 0.3f);
                }
            });
        }
    }

    public void UnLoadScene(string sceneName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        if (!targetScene.isLoaded || !targetScene.IsValid())
        {
            return;
        }

        GameObject[] rootObjects = targetScene.GetRootGameObjects();
        if (rootObjects.Length > 0)
        {
            Transform sceneContainer = rootObjects[0].transform.Find("Container");

            SNModel.Api.IsUnloadingScene = true;

            TweenUtils.TweenSwipeOut(sceneContainer, Vector3.left, 0.3f, () =>
            {
                SceneManager.UnloadSceneAsync(sceneName);
                SNModel.Api.IsUnloadingScene = false;
            });
        }
    }

    public void SetText(string targetTxt, Text txtToSet)
    {
        string txt = "";
        int duration = 1;

        DOTween.To(
            () => txt,
            x => txt = x,
            targetTxt,
            duration
        ).OnUpdate(() => txtToSet.text = txt).SetEase(Ease.OutExpo);
    }

    public void SetDuration(float toValue, Text txtToSet)
    {
        float fromValue = 0;
        int duration = 1;

        DOVirtual.Float(
            fromValue,
            toValue,
            duration,
            (v) => txtToSet.text = v.ToString("0") + SNConstant.TIME_UNIT).SetEase(Ease.OutExpo);
    }

    #endregion UTILS
}