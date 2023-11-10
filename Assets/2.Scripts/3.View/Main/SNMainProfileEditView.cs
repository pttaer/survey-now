using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SNMainProfileEditView : /*SNMainProfileItemView*/ MonoBehaviour
{
    private Button m_BtnCancel;
    private Button m_BtnSubmit;

    // PROFILE VALUES
    protected InputField m_IpfFullname;
    protected Dropdown m_DrDwGender;
    private Text m_Text;
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
    [SerializeField] string m_TxtWarning;
    [SerializeField] string m_TxtBack;
    [SerializeField] string m_TxtWarningIncorrectEmail;

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

        m_DrDwGender.SetValueWithoutNotify(SNModel.Api.CurrentUser.Gender == "Male" ? 0 : 1);
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
        m_Text = rightSide.Find("DropDown/Dropdown/Text").GetComponent<Text>();
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
        m_Text.text = m_CurrentGenderForm;
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
                m_Text.text = "";
            }
        }
    }

    private void SubmitForm()
    {
        switch (transform.name)
        {
            case PNL_PROFILE_EDIT:
                {
                    SNUserRequestDTO updateInfo = null;

                    bool isNameEmpty = string.IsNullOrEmpty(m_IpfFullname.text);
                    bool isDobEmpty = string.IsNullOrEmpty(m_TxtDob.text);
                    bool isGenderEmpty = m_Text.text == "";

                    if (!isNameEmpty && !isDobEmpty && !isGenderEmpty)
                    {
                        DateTime dob = DateTime.ParseExact(m_TxtDob.text, "d/M/yyyy", CultureInfo.InvariantCulture);

                        updateInfo = new SNUserRequestDTO()
                        {
                            FullName = m_IpfFullname.text,
                            Gender = m_DrDwGender.value == 0 ? "Male" : "Female",
                            DateOfBirth = dob
                        };
                    }
                    else if (!isNameEmpty && isDobEmpty && isGenderEmpty)
                    {
                        updateInfo = new SNUserRequestDTO()
                        {
                            FullName = m_IpfFullname.text,
                        };
                    }
                    else if (isNameEmpty && !isDobEmpty && isGenderEmpty)
                    {
                        DateTime dob = DateTime.ParseExact(m_TxtDob.text, "d/M/yyyy", CultureInfo.InvariantCulture);

                        updateInfo = new SNUserRequestDTO()
                        {
                            DateOfBirth = dob
                        };
                    }
                    else if (isNameEmpty && isDobEmpty && !isGenderEmpty)
                    {
                        updateInfo = new SNUserRequestDTO()
                        {
                            Gender = m_DrDwGender.value == 0 ? "Male" : "Female",
                        };
                    }

                    string json = JsonConvert.SerializeObject(updateInfo);
                    JObject jObject = JObject.Parse(json);

                    List<string> propertiesToRemove = jObject.Properties()
                    .Where(p => p.Value.Type == JTokenType.Null)
                    .Select(p => p.Name)
                    .ToList();

                    // Remove the properties
                    foreach (string propertyName in propertiesToRemove)
                    {
                        jObject.Remove(propertyName);
                    }

                    if (jObject == null) return;

                    jObject.Remove("RelationshipStatus");

                    Debug.Log("jObjectData " + jObject.ToString());

                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), jObject, () =>
                    {
                        SNMainControl.Api.ReloadProfile();
                    }));
                    break;
                }
            case PNL_CONTACT_EDIT:
                {
                    if (!IsValidEmail(m_IpfEmail.text))
                    {
                        SNControl.Api.ShowFAMPopup(m_TxtWarning, m_TxtWarningIncorrectEmail, m_TxtBack, "NotShow");
                        return;
                    }

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

                    string json = JsonConvert.SerializeObject(updateInfo);
                    JObject jObject = JObject.Parse(json);

                    List<string> propertiesToRemove = jObject.Properties()
                    .Where(p => p.Value.Type == JTokenType.Null)
                    .Select(p => p.Name)
                    .ToList();

                    // Remove the properties
                    foreach (string propertyName in propertiesToRemove)
                    {
                        jObject.Remove(propertyName);
                    }

                    if (jObject == null) return;

                    jObject.Remove("RelationshipStatus");

                    Debug.Log("jObjectData " + jObject.ToString());

                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), jObject, () =>
                    {
                        SNMainControl.Api.ReloadProfile();
                    }));
                    break;
                }

            case PNL_CAREER_EDIT:
                {

                    OccupationRequest rq = new OccupationRequest()
                    {
                        Income = double.Parse(m_IpfIncome.text),
                        PlaceOfWork = m_IpfPlaceOfWork.text,
                        Currency = "VND"
                    };

                    SNUserRequestDTO updateInfo = new SNUserRequestDTO()
                    {
                        Occupation = rq
                    };

                    string json = JsonConvert.SerializeObject(updateInfo);
                    JObject jObject = JObject.Parse(json);

                    List<string> propertiesToRemove = jObject.Properties()
                    .Where(p => p.Value.Type == JTokenType.Null)
                    .Select(p => p.Name)
                    .ToList();

                    // Remove the properties
                    foreach (string propertyName in propertiesToRemove)
                    {
                        jObject.Remove(propertyName);
                    }

                    if (jObject == null) return;

                    jObject.Remove("RelationshipStatus");

                    Debug.Log("jObjectData " + jObject.ToString());

                    StartCoroutine(SNApiControl.Api.EditData(string.Format(SNConstant.USER_UPDATE_INFO, SNModel.Api.CurrentUser.Id), jObject, () =>
                    {
                        SNMainControl.Api.ReloadProfile();
                    }));
                    break;
                }
        }
    }

    public bool IsValidEmail(string email)
    {
        // Regular expression pattern for email validation
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        // Check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }
}
