using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SNSurveyAnswerDTO;

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

    [SerializeField] string m_Male;
    [SerializeField] string m_Female;

    private UserResponseDTO m_Data;

    public void Init(bool isForceReload = false)
    {
        if (m_Data == null || isForceReload)
        {
            StartCoroutine(SNApiControl.Api.GetData<UserResponseDTO>(SNConstant.USER_CURRENT_INFO, renderPage: (data) =>
            {
                m_Data = data;
                InitPnls();
            }));
        }
        else
        {
            InitPnls();
        }
    }

    private void InitPnls()
    {
        switch (transform.name)
        {
            case "PnlProfile":
                InitPnlProfile();
                break;
            case "PnlContact":
                InitPnlContact();
                break;
            default:
                InitPnlCareer();
                break;
        }
    }

    public void InitPnlCareer()
    {
        var rightSide = transform.Find("PnlInfo/Body/RightSide");

        m_TxtField = rightSide.Find("TxtLabel").GetComponent<Text>();
        m_TxtIncome = rightSide.Find("TxtLabel_2").GetComponent<Text>();
        m_TxtPlaceOfWork = rightSide.Find("TxtLabel_3").GetComponent<Text>();

        if (m_Data.Occupation != null)
        {
            m_TxtField.text = !string.IsNullOrEmpty(m_Data.Occupation.Field.ToString()) ? m_Data.Occupation.Field.ToString() : "--";
            m_TxtIncome.text = !string.IsNullOrEmpty(m_Data.Occupation.Income.ToString()) ? m_Data.Occupation.Income.ToString() : "--";
            m_TxtPlaceOfWork.text = !string.IsNullOrEmpty(m_Data.Occupation.PlaceOfWork) ? m_Data.Occupation.PlaceOfWork : "--";
        }
        else
        {
            m_TxtField.text = "--";
            m_TxtIncome.text = "--";
            m_TxtPlaceOfWork.text = "--";
        }
    }

    public void InitPnlContact()
    {
        var rightSide = transform.Find("PnlInfo/Body/RightSide");

        m_TxtAddress = rightSide.Find("TxtLabel_1").GetComponent<Text>();
        m_TxtPhoneNumber = rightSide.Find("TxtLabel_2").GetComponent<Text>();
        m_TxtEmail = rightSide.Find("TxtLabel_3").GetComponent<Text>();

        if (m_Data.Address != null)
        {
            m_TxtAddress.text = !string.IsNullOrEmpty(SNModel.Api.CurrentUser.Address) ? SNModel.Api.CurrentUser.Address : "--";
            m_TxtPhoneNumber.text = !string.IsNullOrEmpty(m_Data.PhoneNumber) ? m_Data.PhoneNumber : "--";
            m_TxtEmail.text = !string.IsNullOrEmpty(m_Data.Email) ? m_Data.Email : "--";
        }
        else
        {
            m_TxtAddress.text = "--";
            m_TxtPhoneNumber.text = "--";
            m_TxtEmail.text = "--";
        }
    }

    public void InitPnlProfile()
    {
        var rightSide = transform.Find("PnlInfo/Body/RightSide");

        m_TxtFullname = rightSide.Find("TxtLabel_1").GetComponent<Text>();
        m_TxtGender = rightSide.Find("TxtLabel_2").GetComponent<Text>();
        m_TxtDob = rightSide.Find("TxtLabel_3").GetComponent<Text>();

        string name = m_Data.FullName;
        string gender = SNModel.Api.CurrentUser.Gender;
        string isMale = gender == "Male" ? m_Male : m_Female;
        string dob = m_Data.DateOfBirth.ToString();

        m_TxtFullname.text = !string.IsNullOrEmpty(name) ? name : "--";
        m_TxtGender.text = !string.IsNullOrEmpty(gender) ? isMale : "--";
        m_TxtDob.text = !string.IsNullOrEmpty(dob) ? dob : "--";
    }
}
