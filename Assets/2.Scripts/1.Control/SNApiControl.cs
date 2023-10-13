using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SNApiControl
{
    public static SNApiControl Api;

    public static UnityWebRequest WebRequestWithAuthorizationHeader(string url, string method)
    {
        UnityWebRequest request = new UnityWebRequest(url, method.ToUpper());
        //Debug.Log("Token " + FAMConstant.BEARER_TOKEN);
#if UNITY_ANDROID
        Debug.Log("Calling api with token: " + PlayerPrefs.GetString(SNConstant.BEARER_TOKEN_CACHE));
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString(SNConstant.BEARER_TOKEN_CACHE));
#endif

#if UNITY_EDITOR
        Debug.Log("Calling with editor token: " + SNConstant.BEARER_TOKEN_EDITOR);
        request.SetRequestHeader("Authorization", SNConstant.BEARER_TOKEN_EDITOR);
#endif
        return request;
    }

    public IEnumerator GetListData<T>(string url, Dictionary<string, string> param = null, Action<T[]> RenderPage = null, bool isNotShowSorry = false)
    {
        SNControl.Api.ShowLoading();

        if (param != null)
        {
            url += "?";

            foreach (var item in param)
            {
                url = url + item.Key + "=" + item.Value + "&";
            }
        }

        Debug.Log("CALL " + url);

        UnityWebRequest request = WebRequestWithAuthorizationHeader(url, SNConstant.METHOD_GET);

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
            SNControl.Api.HideLoading();

            string response = request.downloadHandler.text;

            if (JsonConvert.DeserializeObject(response).GetType() == typeof(JArray))
            {
                T[] dataArray = JsonConvert.DeserializeObject<T[]>(response);

                RenderPage?.Invoke(dataArray);
            }
            else
            {
                SNListDTO<T> data = JsonConvert.DeserializeObject<SNListDTO<T>>(response);

                RenderPage?.Invoke(data.results);
            }
        }
        else
        {
            SNControl.Api.HideLoading();
            // Show popup error

            Debug.LogError("test error: " + request.error);
        }
    }

    public IEnumerator GetData<T>(string url, Dictionary<string, string> param = null, Action<T> RenderPage = null)
    {
        SNControl.Api.ShowLoading();

        if (param != null)
        {
            url += "?";

            foreach (var item in param)
            {
                url = url + item.Key + "=" + item.Value + "&";
            }
        }

        Debug.Log("CALL " + url);

        UnityWebRequest request = WebRequestWithAuthorizationHeader(url, SNConstant.METHOD_GET);


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
            string response = request.downloadHandler.text;

            T data = JsonConvert.DeserializeObject<T>(response);
            RenderPage?.Invoke(data);
        }
        else
        {
            // Show popup error
            Debug.LogError("test error: " + request.error);
        }
        SNControl.Api.HideLoading();
    }

    public IEnumerator SetRequestMemberAccess(string url, int memberId, int requestAccess)
    {
        UnityWebRequest request = WebRequestWithAuthorizationHeader(url, SNConstant.METHOD_PUT);
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

    public IEnumerator PostData<T>(string uri, T formData, Action callback = null, bool loadCurrentSceneAgain = false, string sceneName = "", bool isEventsSceneLoad = false)
    {
        SNControl.Api.ShowLoading();

        string jsonData = JsonConvert.SerializeObject(formData, Formatting.Indented);
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = WebRequestWithAuthorizationHeader(uri, SNConstant.METHOD_POST);
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

        if (request.result == UnityWebRequest.Result.Success)
        {
            SNControl.Api.HideLoading();

            Debug.LogError("SENT OK: ");
            callback?.Invoke();
        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
        SNControl.Api.HideLoading();
    }

    public IEnumerator DelItem(string uri, Action callback = null)
    {
        SNControl.Api.ShowLoading();

        UnityWebRequest request = SNApiControl.WebRequestWithAuthorizationHeader(uri, SNConstant.METHOD_DELETE);

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        string response = request.downloadHandler.text;

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Handled request access successfully: " + response);
            callback?.Invoke();
        }
        else
        {
            Debug.Log("error: " + request.error);
            callback?.Invoke();
        }
        SNControl.Api.HideLoading();
    }

    public void Logout()
    {

    }

    public IEnumerator Login(string email, string password, Action callback = null)
    {
        SNControl.Api.ShowLoading();
        UnityWebRequest request = new UnityWebRequest(SNConstant.LOGIN, SNConstant.METHOD_POST.ToUpper());

        JObject data = new JObject()
        {
            {"email", email },
            {"password", password}
        };

        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

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
            string response = request.downloadHandler.text;

            SNUserResponseDTO userData = JsonConvert.DeserializeObject<SNUserResponseDTO>(response);
            string token = userData.token;

            Debug.Log($"Response from {SNConstant.LOGIN}: " + response);

            SNModel.Api.CurrentUser = JsonConvert.DeserializeObject<SNUserDTO>(response);

            PlayerPrefs.SetString(SNConstant.BEARER_TOKEN_CACHE, SNModel.Api.CurrentUser.Token);
            PlayerPrefs.SetString(SNConstant.USER_EMAIL_CACHE, userData.email);
            PlayerPrefs.SetString(SNConstant.USER_FULLNAME_CACHE, userData.fullName);

            Debug.Log("Set token: " + PlayerPrefs.GetString(SNConstant.BEARER_TOKEN_CACHE));

#if UNITY_EDITOR
            SNConstant.BEARER_TOKEN_EDITOR = "Bearer " + SNModel.Api.CurrentUser.Token;
#endif
            callback?.Invoke();
        }
        else
        {
            Debug.LogError("test error: " + request.error);
            SNControl.Api.FailLogin();
        }
        SNControl.Api.HideLoading();
    }

    public IEnumerator Register(SNUserDTO data, Action callback = null)
    {
        SNControl.Api.ShowLoading();
        UnityWebRequest request = new UnityWebRequest(SNConstant.REGISTER, SNConstant.METHOD_POST.ToUpper());

        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

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
            string response = request.downloadHandler.text;
            Debug.Log($"Response from {SNConstant.REGISTER}: " + response);
            callback?.Invoke();
        }
        else
        {
            Debug.LogError("test error: " + request.error);
        }
        SNControl.Api.HideLoading();
    }

    public IEnumerator PurchasePoints(int amount, Action<string> callback = null)
    {
        UnityWebRequest request = WebRequestWithAuthorizationHeader(SNConstant.POINTS_PURCHASE, SNConstant.METHOD_POST.ToUpper());

        SNPaymentDTO payment = new SNPaymentDTO(amount);

        string jsonData = JsonConvert.SerializeObject(payment, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        // Check that downloadHandler is not null
        if (request.downloadHandler != null)
        {
            Debug.Log("request data= " + jsonData);
        }
        else
        {
            Debug.Log("Error: downloadHandler is null");
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log($"Response from {SNConstant.POINTS_PURCHASE}: " + response);

            SNPointsPurchaseDTO data = JsonConvert.DeserializeObject<SNPointsPurchaseDTO>(response);

            callback?.Invoke(data.deeplink);
        }
        else
        {
            Debug.LogError("test error: " + request.error);
        }
    }

    public IEnumerator MomoReturn(SNMomoRedirect momoData, string param, Action callback = null)
    {
        UnityWebRequest request = WebRequestWithAuthorizationHeader(SNConstant.POINTS_PURCHASE_RETURN + param + "userId=" + SNModel.Api.CurrentUser.Id, SNConstant.METHOD_GET.ToUpper());

        yield return request.SendWebRequest();

        // Check that downloadHandler is not null
        if (request.downloadHandler != null)
        {
            Debug.Log("request data= " + SNConstant.POINTS_PURCHASE_RETURN + param + "userId=" + SNModel.Api.CurrentUser.Id);
        }
        else
        {
            Debug.Log("Error: downloadHandler is null");
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log($"Response from {SNConstant.POINTS_PURCHASE_RETURN}: " + response);

            callback?.Invoke();
        }
        else
        {
            Debug.LogError("test error: " + request.error);
        }
    }

    public IEnumerator PostSurveyTest(SNSurveyRequestDTO postData)
    {
        // Example post data

        /*postData = new SNSurveyRequestDTO()
        {
            Title = "okay",
            Description = "okay",
            PackType = "Basic",
            StartDate = DateTime.Now,
            ExpiredDate = DateTime.Now,
            Sections = new List<SNSectionRequestDTO>()
            {
                new SNSectionRequestDTO()
                {
                    Order = 20,
                    Title = "okay",
                    Description = "okay",
                    Questions = new List<SNQuestionRequestDTO>()
                    {
                        new SNQuestionRequestDTO()
                        {
                            Order = 100,
                            Type = "Text",
                            IsRequired = true,
                            MultipleOptionType = "NoLimit",
                            LimitNumber = 20,
                            Title = "okay",
                            ResourceUrl = "okay",
                            RowOptions = new List<SNRowOptionRequestDTO>()
                            {
                                new SNRowOptionRequestDTO()
                                {
                                    Order = 20,
                                    IsCustom = true,
                                    Content = "okay"
                                }
                            },
                            ColumnOptions = new List<SNColumnOptionRequestDTO>()
                            {
                                new SNColumnOptionRequestDTO()
                                {
                                    Order = 20,
                                    Content = "okay"
                                }
                            }
                        },
                        new SNQuestionRequestDTO()
                        {
                            Order = 10,
                            Type = "Text",
                            IsRequired = true,
                            MultipleOptionType = "NoLimit",
                            LimitNumber = 20,
                            Title = "okay",
                            ResourceUrl = "okay",
                            RowOptions = new List<SNRowOptionRequestDTO>()
                            {
                                new SNRowOptionRequestDTO()
                                {
                                    Order = 20,
                                    IsCustom = true,
                                    Content = "okay"
                                }
                            },
                            ColumnOptions = new List<SNColumnOptionRequestDTO>()
                            {
                                new SNColumnOptionRequestDTO()
                                {
                                    Order = 20,
                                    Content = "okay"
                                }
                            }
                        },
                        new SNQuestionRequestDTO()
                        {
                            Order = 88,
                            Type = "Text",
                            IsRequired = true,
                            MultipleOptionType = "NoLimit",
                            LimitNumber = 20,
                            Title = "okay",
                            ResourceUrl = "okay",
                            RowOptions = new List<SNRowOptionRequestDTO>()
                            {
                                new SNRowOptionRequestDTO()
                                {
                                    Order = 20,
                                    IsCustom = true,
                                    Content = "okay"
                                }
                            },
                            ColumnOptions = new List<SNColumnOptionRequestDTO>()
                            {
                                new SNColumnOptionRequestDTO()
                                {
                                    Order = 20,
                                    Content = "okay"
                                }
                            }
                        }
                    }
                }
            }
        };*/
        string jsonData = JsonConvert.SerializeObject(postData, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = WebRequestWithAuthorizationHeader(SNConstant.SURVEY_POST, SNConstant.METHOD_POST);

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error posting data: " + request.error);
        }
        else
        {
            Debug.Log("Data posted successfully!\n\n" + jsonData);
        }
    }
}