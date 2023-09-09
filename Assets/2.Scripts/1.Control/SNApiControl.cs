using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SNApiControl
{
    /* public static FAMApiControl Api;

     public static string cheatImageUrl;

     public static UnityWebRequest WebRequestWithAuthorizationHeader(string url, string method)
     {
         UnityWebRequest request = new UnityWebRequest(url, method.ToUpper());
         //Debug.Log("Token " + FAMConstant.BEARER_TOKEN);
         request.SetRequestHeader("Authorization", PlayerPrefs.GetString(FAMConstant.BEARER_TOKEN_CACHE));

 #if UNITY_EDITOR
         request.SetRequestHeader("Authorization", FAMConstant.BEARER_TOKEN);
 #endif

         return request;
     }

     public IEnumerator GetListData<T>(string api, string method, Action<T[]> RenderPage, bool isNotShowSorry = false)
     {
         Debug.Log("CALL " + api);

         if (!isNotShowSorry) FAMControl.Api.HideSorry();
         FAMControl.Api.ShowLoading();
         UnityWebRequest request = WebRequestWithAuthorizationHeader(api, method);

         request.downloadHandler = new DownloadHandlerBuffer();
         yield return request.SendWebRequest();

         Debug.Log("request result= " + request.result);

         // Check that downloadHandler is not null
         if (request.downloadHandler != null)
         {
             Debug.Log("request data= " + request.downloadHandler.text);
         }
         else
         {
             Debug.Log("Error: downloadHandler is null");
         }

         if (request.result == UnityWebRequest.Result.Success)
         {
             FAMControl.Api.HideLoading();
             string response = request.downloadHandler.text;
             if (!isNotShowSorry)
             {
                 ResponseData<T> jsondata = JsonConvert.DeserializeObject<ResponseData<T>>(response);
                 T[] datas = jsondata.items.Select(dto => dto).ToArray();
                 RenderPage(datas);
                 if (datas.Length == 0)
                 {
                     FAMControl.Api.ShowSorry("Sorry, there are <b>no more items.</b>");
                     FAMPaginationControl.Api.NoMoreDataDisableNextButton();
                 }
             }
             if (isNotShowSorry)
             {
                 T[] jsondata = JsonConvert.DeserializeObject<T[]>(response).Select(dto => dto).ToArray();
                 RenderPage(jsondata);
             }
         }
         else
         {
             if (!isNotShowSorry) FAMControl.Api.ShowSorry();
             FAMControl.Api.HideLoading();
             if (!isNotShowSorry) FAMPaginationControl.Api.NoMoreDataDisableNextButton();
             Debug.LogError("test error: " + request.error);
         }
     }

     public IEnumerator SetRequestMemberAccess(string url, int memberId, int requestAccess)
     {
         UnityWebRequest request = WebRequestWithAuthorizationHeader(url, FAMConstant.METHOD_PUT);
         request.SetRequestHeader("Content-Type", "application/json");

         JObject json = new JObject
         {
             { "id", memberId },
             { "requestStatus", requestAccess }
         };

         byte[] rawJsonData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));

         request.uploadHandler = new UploadHandlerRaw(rawJsonData);
         request.downloadHandler = new DownloadHandlerBuffer();

         yield return request.SendWebRequest();

         string response = request.downloadHandler.text;

         if (request.result == UnityWebRequest.Result.Success)
         {
             Debug.Log("Handled request access successfully: " + response);
         }
         else
         {
             Debug.Log("error: " + request.error);
         }
     }

     public IEnumerator SendData<T>(string api, T formData, bool loadCurrentSceneAgain = false, string sceneName = "", bool isEventsSceneLoad = false)
     {
         Debug.Log("GOBISH");

         FAMControl.Api.ShowLoading();

         string jsonData = JsonConvert.SerializeObject(formData, Formatting.Indented);
         byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

         UnityWebRequest request = WebRequestWithAuthorizationHeader(api, FAMConstant.METHOD_POST);
         request.SetRequestHeader("Content-Type", "application/json");
         request.uploadHandler = new UploadHandlerRaw(jsonBytes);
         request.downloadHandler = new DownloadHandlerBuffer();

         yield return request.SendWebRequest();

         Debug.Log("request result= " + request.result);

         if (request.uploadHandler != null)
         {
             byte[] uploadedData = request.uploadHandler.data;
             string uploadedDataString = System.Text.Encoding.UTF8.GetString(uploadedData);
             Debug.Log("Uploaded data: " + uploadedDataString);
         }
         else
         {
             Debug.Log("Error: uploadHandler is null");
         }

         Debug.Log("Response: " + request.downloadHandler.text);
         if (request.result == UnityWebRequest.Result.Success)
         {
             FAMControl.Api.HideLoading();
             Debug.Log("GOOD SEND");
             if (!loadCurrentSceneAgain) FAMMenuControl.Api.ClickBack();
             else
             {
                 FAMControl.Api.UnloadAllThenLoadScene(sceneName);
             }
             if (isEventsSceneLoad) DOVirtual.DelayedCall(0.6f, () => FAMMenuControl.Api.BackFromDetailChangeTab(false));
             // Action to handle load & back to previous scene
         }
         else
         {
             FAMControl.Api.HideLoading();
             Debug.LogError("Error sending data: " + request.error);
             // Action to popup fail
         }
     }

     public IEnumerator DelItem(FAMNewsDTO newsData, FAMEventsDTO eventData)
     {
         FAMControl.Api.ShowLoading();

         int id = FAMModel.Api.CurrentDetailType == FAMDetailType.News ? newsData.Id : eventData.Id;

         string uri = FAMModel.Api.CurrentDetailType == FAMDetailType.News ? FAMConstant.NEWS : FAMConstant.EVENTS;

         uri = uri + "/" + id.ToString();

         Debug.Log("uri: " + uri);

         UnityWebRequest request = FAMApiControl.WebRequestWithAuthorizationHeader(uri, FAMConstant.METHOD_DELETE);

         request.downloadHandler = new DownloadHandlerBuffer();

         yield return request.SendWebRequest();

         string response = request.downloadHandler.text;

         if (request.result == UnityWebRequest.Result.Success)
         {
             Debug.Log("Handled request access successfully: " + response);
             FAMMenuControl.Api.ClickBack();
             FAMControl.Api.HideLoading();
         }
         else
         {
             Debug.Log("error: " + request.error);
             FAMControl.Api.HideLoading();
         }
     }

     public void CheckSchoolRequestExist()
     {
         CallBooleanApi(FAMConstant.SCHOOL_CHECK_EXIST_REQUEST, (handler) =>
         {
             bool responseValue = bool.Parse(handler.text);
             Debug.Log("API RESPONSE CHECK SCHOOL REQUEST EXIST: " + responseValue);
             DOVirtual.DelayedCall(2f, () => FAMLoginControl.Api.ShowPanelWaiting(responseValue)); // Trigger school check if true
             DOVirtual.DelayedCall(2f, () => FAMLoginControl.Api.ShowSchoolCreate(!responseValue));
             DOVirtual.DelayedCall(2f, () => FAMControl.Api.HideLoading());
         });
     }

     public void CheckSchoolRequestAccept()
     {
         CallBooleanApi(FAMConstant.SCHOOL_CHECK_CREATE_REQUEST, (handler) =>
         {
             bool responseValue = bool.Parse(handler.text);
             Debug.Log("API RESPONSE CHECK SCHOOL REQUEST ACCEPT: " + responseValue);
             FAMLoginControl.Api.ShowPanelWaiting(!responseValue);
             if (responseValue) Logout();

             // Send noti
 #if PLATFORM_ANDROID
             if (responseValue) FAMNotificationControl.Api.StartNoti("School request alumni", "Your school registation got accepted", "1", "school");
 #endif
             FAMControl.Api.HideLoading();
         });
     }

     private void Logout()
     {
         PlayerPrefs.DeleteKey(FAMConstant.BEARER_TOKEN_CACHE);
         PlayerPrefs.DeleteKey(FAMConstant.PROFILE_CACHE);
         FAMControl.Api.UnloadAllThenLoadScene(FAMConstant.SCENE_LOGIN);
         FAMControl.Api.UnLoadScene(FAMConstant.SCENE_MENU);
     }

     private void CallBooleanApi(string api, Action<DownloadHandlerBuffer> callback)
     {
         DOVirtual.DelayedCall(0.5f, () => FAMControl.Api.ShowMain(false));
         FAMControl.Api.ShowLoading();

         UnityWebRequest request = WebRequestWithAuthorizationHeader(api, FAMConstant.METHOD_GET);
         DownloadHandlerBuffer handler = new DownloadHandlerBuffer();
         request.downloadHandler = handler;

         request.SendWebRequest();

         // Wait the response
         while (!request.isDone) ;

         if (request.result == UnityWebRequest.Result.Success)
         {
             callback(handler);
         }
         else
         {
             FAMControl.Api.HideLoading();
             Debug.LogError("API request failed with result: " + request.result);
         }
     }

     public IEnumerator CheckSchoolId()
     {
         DOVirtual.DelayedCall(0.5f, () => FAMControl.Api.ShowMain(false));
         FAMControl.Api.ShowLoading();

         UnityWebRequest request = WebRequestWithAuthorizationHeader(FAMConstant.SCHOOL_ACCESS_REQUEST, FAMConstant.METHOD_GET);

         request.downloadHandler = new DownloadHandlerBuffer();

         yield return request.SendWebRequest();

         Debug.Log("request result= " + request.result);
         string response = request.downloadHandler.text;

         Debug.Log(response);

         if (request.result == UnityWebRequest.Result.Success)
         {
             FAMLoginControl.Api.LoadNextScene();

             Debug.Log("Handled request access successfully: " + response);

             if (response == "-1")
             {
                 FAMControl.Api.HideLoading();
                 Debug.Log("no school");
                 CheckSchoolRequestExist();
             }
             else
             {
                 FAMControl.Api.HideLoading();
                 DOVirtual.DelayedCall(0.5f, () => FAMControl.Api.ShowMain(true));
                 Debug.Log("school already");
             }
         }
         else
         {
             FAMControl.Api.HideLoading();
             Debug.Log("error: " + request.error);
         }
     }*/
}