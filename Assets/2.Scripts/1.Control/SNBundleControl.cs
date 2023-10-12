using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNBundleControl
{
    public static SNBundleControl Api;

    public Action<string> onOpenPopupForm;
    public Action<string> onOpenPopupNotice;

    public string m_CurrentPackType;

    public void OnOpenPopupForm(string packtype)
    {
        onOpenPopupForm?.Invoke(packtype);

        m_CurrentPackType = packtype;
    }

    public void OnOpenPopupNotice(string remaindingAmount)
    {
        onOpenPopupNotice?.Invoke(remaindingAmount);
    }
}
