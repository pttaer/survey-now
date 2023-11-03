using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNProfileControl
{
    public static SNProfileControl Api;

    public Action<string> OnCloseEditPnlEvent;

    public void OnCloseEditPnl(string pnlName)
    {
        OnCloseEditPnlEvent?.Invoke(pnlName);
    }
}
