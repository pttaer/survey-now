using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainProfileItemView : MonoBehaviour
{
    // PROFILE VALUES
    protected Text m_TxtFullname;
    protected Text m_TxtGender;
    protected Text m_TxtDob;
    // CONTACT VALUES
    protected Text m_TxtAddress;
    protected Text m_TxtPhoneNumber;
    protected Text m_TxtEmail;

    // OCCUPATION VALUES
    protected Text m_TxtField;
    protected Text m_TxtIncome;
    protected Text m_TxtPlaceOfWork;

    public void Init()
    {
        if (transform.name == "PnlProfile")
        {
            InitPnlProfile();
        }
        else if (transform.name == "PnlContact")
        {
            InitPnlContact();
        }
        else
        {
            InitPnlCareer();
        }
    }

    public void InitPnlCareer()
    {
        m_TxtField = transform.Find("PnlInfo/Body/RightSide/TxtLabel").GetComponent<Text>();
        m_TxtIncome = transform.Find("PnlInfo/Body/RightSide/TxtLabel_2").GetComponent<Text>();
        m_TxtPlaceOfWork = transform.Find("PnlInfo/Body/RightSide/TxtLabel_3").GetComponent<Text>();
    }

    public void InitPnlContact()
    {
        m_TxtAddress = transform.Find("PnlInfo/Body/RightSide/TxtLabel_1").GetComponent<Text>();
        m_TxtPhoneNumber = transform.Find("PnlInfo/Body/RightSide/TxtLabel_2").GetComponent<Text>();
        m_TxtEmail = transform.Find("PnlInfo/Body/RightSide/TxtLabel_3").GetComponent<Text>();

        m_TxtAddress.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.Address) ? SNModel.Api.CurrentUser.Address : "--";
        m_TxtPhoneNumber.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.PhoneNumber) ? SNModel.Api.CurrentUser.PhoneNumber : "--";
        m_TxtEmail.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.Email) ? SNModel.Api.CurrentUser.Email : "--";
    }

    public void InitPnlProfile()
    {
        m_TxtFullname = transform.Find("PnlInfo/Body/RightSide/TxtLabel_1").GetComponent<Text>();
        m_TxtGender = transform.Find("PnlInfo/Body/RightSide/TxtLabel_2").GetComponent<Text>();
        m_TxtDob = transform.Find("PnlInfo/Body/RightSide/TxtLabel_3").GetComponent<Text>();

        m_TxtFullname.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.FullName) ? SNModel.Api.CurrentUser.FullName : "--";
        m_TxtGender.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.Gender) ? SNModel.Api.CurrentUser.Gender : "--";
        m_TxtDob.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.DateOfBirth) ? SNModel.Api.CurrentUser.DateOfBirth : "--";
    }
}
