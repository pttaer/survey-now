using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNBundleControl
{
    public static SNBundleControl Api;

    public void OpenPayment()
    {
        SNControl.Api.UnloadThenLoadScene(SNConstant.SCENE_PAYMENT);
    }
}
