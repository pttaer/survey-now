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
    private const string PNL_PROFILE_EDIT = "PnlProfileEdit";
    private const string PNL_CONTACT_EDIT = "PnlContactEdit";
    private const string PNL_CAREER_EDIT = "PnlCareerEdit";

    public new void Init()
    {
        m_BtnCancel = transform.Find("TopBar/BtnCancel").GetComponent<Button>();
        m_BtnSubmit = transform.Find("TopBar/BtnApprove").GetComponent<Button>();

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
        m_IpfFullname = rightSide.Find("IpfInfo").GetComponent<InputField>();
        m_DrDwGender = rightSide.Find("DropDown/Dropdown").GetComponent<Dropdown>();
        m_IpfDob = rightSide.Find("DatePicker").GetComponent<InputField>();

        m_DrDwGender.onValueChanged.AddListener(SetCurrentGenderForm);
    }

    private void InitPnlContactEdit()
    {
        Transform rightSide = transform.Find("Body/RightSide");
        m_IpfAddress = rightSide.Find("IpfInfo").GetComponent<InputField>();
        m_IpfPhoneNumber = rightSide.Find("IpfInfo_1").GetComponent<InputField>();
        m_IpfEmail = rightSide.Find("IpfInfo_2").GetComponent<InputField>();
    }

    private void InitPnlCareerEdit()
    {
        Transform rightSide = transform.Find("Body/RightSide");
        m_IpfField = rightSide.Find("DropdownDistrict_1").GetComponent<InputField>();
        m_IpfIncome = rightSide.Find("DropdownDistrict_2").GetComponent<InputField>();
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
        if (transform.name == (pnlName + "Edit"))
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void SubmitForm()
    {
        if (transform.name == PNL_PROFILE_EDIT || transform.name == PNL_CONTACT_EDIT)
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
        else if (transform.name == PNL_CAREER_EDIT)
        {
            
        }
    }
}
