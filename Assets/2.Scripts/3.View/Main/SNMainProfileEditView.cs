using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SNMainProfileEditView : /*SNMainProfileItemView*/ MonoBehaviour
{
    private Button m_BtnCancel;
    private Button m_BtnSubmit;

    // PROFILE VALUES
    protected InputField m_IpfFullname;
    protected Dropdown m_DrDwGender;
    protected Text m_TxtDob;
    // CONTACT VALUES
    protected InputField m_IpfAddress;
    protected InputField m_IpfPhoneNumber;
    protected InputField m_IpfEmail;

    // OCCUPATION VALUES
    protected InputField m_IpfField;
    protected InputField m_IpfIncome;
    protected InputField m_IpfPlaceOfWork;

    private string m_CurrentGenderForm;
    private const string PNL_PROFILE_EDIT = "PnlProfileEdit";
    private const string PNL_CONTACT_EDIT = "PnlContactEdit";
    private const string PNL_CAREER_EDIT = "PnlCareerEdit";

    public new void Init()
    {
        if (transform.name == PNL_PROFILE_EDIT)
        {
            InitPnlProfileEdit();
        }
        else if (transform.name == PNL_CONTACT_EDIT)
        {
            InitPnlContactEdit();
        }
        else if (transform.name == PNL_CAREER_EDIT)
        {
            InitPnlCareerEdit();
        }

        m_BtnCancel.onClick.AddListener(ClosePnl);
        m_BtnSubmit.onClick.AddListener(SubmitForm);

        SNProfileControl.Api.OnCloseEditPnlEvent += ClosePnlWithName;
    }

    private void OnDestroy()
    {
        SNProfileControl.Api.OnCloseEditPnlEvent -= ClosePnlWithName;
    }
    private void InitPnlProfileEdit()
    {
        Transform rightSide = transform.Find("Body/RightSide");
        m_BtnCancel = transform.Find("TopBar/BtnCancel").GetComponent<Button>();
        m_BtnSubmit = transform.Find("TopBar/BtnApprove").GetComponent<Button>();
        m_IpfFullname = rightSide.Find("IpfInfo").GetComponent<InputField>();
        m_DrDwGender = rightSide.Find("DropDown/Dropdown").GetComponent<Dropdown>();
        m_TxtDob = rightSide.Find("DatePicker/Text").GetComponent<Text>();

        m_DrDwGender.onValueChanged.AddListener(SetCurrentGenderForm);
    }

    private void InitPnlContactEdit()
    {
        Transform rightSide = transform.Find("Body/RightSide");
        m_BtnCancel = transform.Find("TopBar/BtnCancel").GetComponent<Button>();
        m_BtnSubmit = transform.Find("TopBar/BtnApprove").GetComponent<Button>();
        m_IpfAddress = rightSide.Find("IpfInfo").GetComponent<InputField>();
        m_IpfPhoneNumber = rightSide.Find("IpfInfo_1").GetComponent<InputField>();
        m_IpfEmail = rightSide.Find("IpfInfo_2").GetComponent<InputField>();
    }

    private void InitPnlCareerEdit()
    {
        Transform rightSide = transform.Find("Body/RightSide");
        m_BtnCancel = transform.Find("TopBar/BtnCancel").GetComponent<Button>();
        m_BtnSubmit = transform.Find("TopBar/BtnApprove").GetComponent<Button>();
        m_IpfField = rightSide.Find("DropdownDistrict_1/TxtLabel_1").GetComponent<InputField>();
        m_IpfIncome = rightSide.Find("DropdownDistrict_2/TxtLabel").GetComponent<InputField>();
        m_IpfPlaceOfWork = rightSide.Find("IpfInfo_2").GetComponent<InputField>();
    }

    private void SetCurrentGenderForm(int value)
    {
        m_CurrentGenderForm = m_DrDwGender.options[value].text;
    }


    private void ClosePnl()
    {
        transform.gameObject.SetActive(false);
    }

    private void ClosePnlWithName(string pnlName)
    {
        if (this != null)
        {
            if (transform.name == (pnlName + "Edit"))
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void SubmitForm()
    {
        switch (transform.name)
        {
            case PNL_PROFILE_EDIT:
                {
                    DateTime dob = DateTime.Parse(m_TxtDob.text.Replace("/", "-"));
                    Debug.Log("formData dob " + dob + " " + m_TxtDob.text.Replace("/", "-"));
                    Debug.Log("formData m_IpfFullname " + m_IpfFullname.text);
                    Debug.Log("formData m_CurrentGenderForm " + m_CurrentGenderForm);

                    SNUserRequestDTO updateInfo = new SNUserRequestDTO()
                    {
                        FullName = m_IpfFullname.text,
                        Gender = m_CurrentGenderForm,
                        DateOfBirth = dob
                    };

                    Debug.Log("formData " + updateInfo.ToString());

                    if (updateInfo == null) return;

                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), updateInfo, () =>
                    {

                    }));
                    break;
                }
            case PNL_CONTACT_EDIT:
                {
                    AddressRequest address = new()
                    {
                        Detail = m_IpfAddress.text,
                    };

                    SNUserRequestDTO updateInfo = new SNUserRequestDTO()
                    {
                        Email = m_IpfEmail.text,
                        Address = address,
                    };

                    Debug.Log("formData " + updateInfo);

                    if (updateInfo == null) return;

                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), updateInfo, () =>
                    {

                    }));
                    break;
                }

            case PNL_CAREER_EDIT:
                break;
        }
    }
}
