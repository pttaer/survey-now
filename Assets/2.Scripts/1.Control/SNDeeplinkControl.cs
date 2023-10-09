using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SNDeeplinkControl : MonoBehaviour
{
    public static SNDeeplinkControl Api { get; set; }
    public string deeplinkURL;
    public Action<SNMomoRedirect, string> onReturnFromMomo;

    public void Awake()
    {
        if (Api == null)
        {
            Api = this;
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";
        }
    }

    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;

        Debug.Log("Deeplink return: " + deeplinkURL);

        // Decode the URL to determine action. 
        // In this example, the app expects a link formatted like this:
        // unitydl://mylink?scene1
        Dictionary<string, string> kvData = new Dictionary<string, string>();
        string[] parameters = url.Split('?')[1].Split('&');

        foreach (string param in parameters)
        {
            int index = param.IndexOf('=');

            string key = param.Substring(0, index);
            string value = param.Substring(index + 1);

            kvData.Add(key, value);
        }

        SNMomoRedirect momoData = new SNMomoRedirect(
                                    partnerCode: kvData["partnerCode"],
                                    orderId: kvData["orderId"],
                                    requestId: kvData["requestId"],
                                    amount: long.Parse(kvData["amount"]),
                                    orderInfo: kvData["orderInfo"],
                                    orderType: kvData["orderType"],
                                    transId: kvData["transId"],
                                    resultCode: kvData["resultCode"],
                                    message: kvData["message"],
                                    payType: kvData["payType"],
                                    responseTime: kvData["responseTime"],
                                    extraData: kvData["extraData"],
                                    signature: kvData["signature"]
                                    );

        OnReturnFromMomo(momoData, deeplinkURL[(deeplinkURL.IndexOf('?'))..]);
    }

    public void OnReturnFromMomo(SNMomoRedirect momoData, string urlParam)
    {
        onReturnFromMomo?.Invoke(momoData, urlParam);
    }
}
