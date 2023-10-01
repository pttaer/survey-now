using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SNApiControl
{
    public static SNApiControl Api;

    public static UnityWebRequest WebRequestWithAuthorizationHeader(string url, string method)
    {
        UnityWebRequest request = new UnityWebRequest(url, method.ToUpper());
        //Debug.Log("Token " + SNConstant.BEARER_TOKEN);
        request.SetRequestHeader("Authorization", PlayerPrefs.GetString(SNConstant.BEARER_TOKEN_CACHE));
#if UNITY_EDITOR
        request.SetRequestHeader("Authorization", SNConstant.BEARER_TOKEN);
#endif
        return request;
    }

    public IEnumerator GetListData<T>(string uri, string method, Action<T[]> RenderPage, bool isNotShowSorry = false)
    {
        UnityWebRequest request = WebRequestWithAuthorizationHeader(uri, method);

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
        }
        else
        {
            Debug.LogError("test error: " + request.error);
        }
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

    public IEnumerator PostData<T>(string uri, T formData, bool loadCurrentSceneAgain = false, string sceneName = "", bool isEventsSceneLoad = false)
    {
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

        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);

        }
    }

    public IEnumerator DelItem(string uri)
    {
        UnityWebRequest request = SNApiControl.WebRequestWithAuthorizationHeader(uri, SNConstant.METHOD_DELETE);

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

    public void Logout()
    {

    }

    public IEnumerator Login(string email, string password, Action callback = null)
    {
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
            SNConstant.BEARER_TOKEN = "Bearer " + token;
            Debug.Log("Login succeeded! " + "Bearer " + token);
            PlayerPrefs.SetString(SNConstant.BEARER_TOKEN_CACHE, SNConstant.BEARER_TOKEN);

            callback?.Invoke();
        }
        else
        {
            Debug.LogError("test error: " + request.error);
        }
    }

    public IEnumerator Register(SNUserDTO data, Action callback = null)
    {
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
    }

    private const string url = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/surveys"; // Replace with the actual API endpoint URL

    public IEnumerator PostSurveyTest()
    {
        SNSurveyRequestDTO postData = new SNSurveyRequestDTO()
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
        };

        string jsonData = JsonConvert.SerializeObject(postData, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = WebRequestWithAuthorizationHeader(url, SNConstant.METHOD_POST);
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
            Debug.Log("Data posted successfully!");
        }
    }
}