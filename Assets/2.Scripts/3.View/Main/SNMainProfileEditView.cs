using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNMainProfileEditView : SNMainProfileItemView
{
    private Button m_BtnCancel;
    private Button m_BtnSubmit;

    // PROFILE VALUES
    protected InputField m_IpfFullname;
    protected Dropdown m_DrDwGender;
    protected InputField m_IpfDob;
    // CONTACT VALUES
    protected InputField m_IpfAddress;
    protected InputField m_IpfPhoneNumber;
    protected InputField m_IpfEmail;

    // OCCUPATION VALUES
    protected InputField m_IpfField;
    protected InputField m_IpfIncome;
    protected InputField m_IpfPlaceOfWork;

    private string m_CurrentGenderForm;

    public new void Init()
    {
        m_BtnCancel = transform.Find("TopBar/BtnCancel").GetComponent<Button>();
        m_BtnSubmit = transform.Find("TopBar/BtnApprove").GetComponent<Button>();

        if (transform.name == "PnlProfileEdit")
        {
            InitPnlProfileEdit();
        }
        else if (transform.name == "PnlContactEdit")
        {
            InitPnlContactEdit();
        }
        else if (transform.name == "PnlCareerEdit")
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


    private void InitPnlCareerEdit()
    {
        m_IpfField = transform.Find("Body/RightSide/DropdownDistrict_1").GetComponent<InputField>();
        m_IpfIncome = transform.Find("Body/RightSide/DropdownDistrict_2").GetComponent<InputField>();
        m_IpfPlaceOfWork = transform.Find("Body/RightSide/IpfInfo_2").GetComponent<InputField>();
    }

    private void InitPnlContactEdit()
    {
        m_IpfAddress = transform.Find("Body/RightSide/IpfInfo").GetComponent<InputField>();
        m_IpfPhoneNumber = transform.Find("Body/RightSide/IpfInfo_1").GetComponent<InputField>();
        m_IpfEmail = transform.Find("Body/RightSide/IpfInfo_2").GetComponent<InputField>();
    }

    private void InitPnlProfileEdit()
    {
        m_IpfFullname = transform.Find("Body/RightSide/IpfInfo").GetComponent<InputField>();
        m_DrDwGender = transform.Find("Body/RightSide/DropDown/Dropdown").GetComponent<Dropdown>();
        m_IpfDob = transform.Find("Body/RightSide/DatePicker").GetComponent<InputField>();

        m_DrDwGender.onValueChanged.AddListener(SetCurrentGenderForm);
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
        if (transform.name == (pnlName + "Edit"))
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void SubmitForm()
    {
        if (transform.name == "PnlProfileEdit" || transform.name == "PnlContactEdit")
        {
            SNUserDTO updateInfo = new ()
            {
                FullName = m_IpfFullname.text,
                Gender = m_CurrentGenderForm,
                DateOfBirth = m_IpfDob.text,
                Address = m_IpfAddress.text,
                PhoneNumber = m_IpfPhoneNumber.text,
                Email = m_IpfEmail.text
            };

            StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), updateInfo, () =>
            {

            }));
        }
        else if (transform.name == "PnlCareerEdit")
        {
            
        }
    }
}
