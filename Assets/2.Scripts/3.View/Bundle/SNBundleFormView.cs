using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNBundleFormView : MonoBehaviour
{
    private InputField m_IpfParticipants;
    private Text m_TxtAmount;
    private Button m_BtnSubmit;

    private int m_ParticipantAmount;

    public void Init()
    {
        m_IpfParticipants = transform.Find("IpfParticipants").GetComponent<InputField>();
        m_TxtAmount = transform.Find("GroupCalculate/TxtAmount").GetComponent<Text>();
        m_BtnSubmit = transform.Find("BtnSubmit").GetComponent<Button>();

        m_IpfParticipants.onValueChanged.AddListener(OnUpdateForm);

        m_BtnSubmit.onClick.AddListener(OnSubmit);
    }

    private void OnUpdateForm(string amount)
    {
        StartCoroutine(SNApiControl.Api.GetData<float>(SNConstant.PACKS_CALCULATE,
            new Dictionary<string, string>()
        {
            {"packType", SNBundleControl.Api.m_CurrentPackType},
            {"participants", amount }
        }, (data) =>
        {
            m_TxtAmount.text = data.ToString();
            m_ParticipantAmount = int.Parse(amount);
        }));
    }

    private void OnSubmit()
    {
        // OPEN NOTICE PNL IF TOO BROKE, ELSE PROCEED
        if (float.Parse(m_TxtAmount.text) > SNModel.Api.CurrentUser.Point)
        {
            SNBundleControl.Api.OnOpenPopupNotice((float.Parse(m_TxtAmount.text) - SNModel.Api.CurrentUser.Point).ToString());
        }
        else
        {
            SNPacksPurchaseDTO data = new(SNBundleControl.Api.m_CurrentPackType, 1, m_ParticipantAmount);

            _ = StartCoroutine(SNApiControl.Api.PostData(SNConstant.PACKS_PURCHASE, data, () =>
            {
                SNModel.Api.CurrentUser.Point -= float.Parse(m_TxtAmount.text);
            }));
        }

        transform.gameObject.SetActive(false);

    }
}
