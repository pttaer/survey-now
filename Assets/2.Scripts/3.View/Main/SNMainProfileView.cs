using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SNMainProfileView : MonoBehaviour
{
    private Button m_BtnPnlProfile;
    private Button m_BtnPnlContact;
    private Button m_BtnPnlCareer;
    private Button m_BtnPnlHobbies;

    private GameObject m_BodyProfile;
    private GameObject m_BodyContact;
    private GameObject m_BodyCareer;
    private GameObject m_BodyHobbies;

    private SNMainProfileEditView m_BodyProfileEdit;
    private SNMainProfileEditView m_BodyContactEdit;
    private SNMainProfileEditView m_BodyCareerEdit;
    private SNMainProfileEditView m_BodyHobbiesEdit;

    private List<GameObject> m_ListPnl;
    private List<SNMainProfileEditView> m_ListPnlEdit;

    public void Init()
    {
        Transform content = transform.Find("Viewport/Content");

        m_BtnPnlProfile = content.Find("PnlProfile/TopBar").GetComponent<Button>();
        m_BtnPnlContact = content.Find("PnlContact/TopBar").GetComponent<Button>();
        m_BtnPnlCareer = content.Find("PnlCareer/TopBar").GetComponent<Button>();
        m_BtnPnlHobbies = content.Find("PnlHobbies/TopBar").GetComponent<Button>();

        m_BodyProfile = content.Find("PnlProfile/PnlInfo").gameObject;
        m_BodyContact = content.Find("PnlContact/PnlInfo").gameObject;
        m_BodyCareer = content.Find("PnlCareer/PnlInfo").gameObject;
        m_BodyHobbies = content.Find("PnlHobbies/PnlInfo").gameObject;

        m_ListPnl = new()
        {
            m_BodyProfile,
            m_BodyContact,
            m_BodyCareer,
            m_BodyHobbies
        };

        m_ListPnlEdit = new()
        {
            m_BodyProfileEdit,
            m_BodyContactEdit,
            m_BodyCareerEdit,
            m_BodyHobbiesEdit
        };

        m_BtnPnlProfile.onClick.AddListener(OpenProfileDetail);
        m_BtnPnlContact.onClick.AddListener(OpenContactDetail);
        m_BtnPnlCareer.onClick.AddListener(OpenCareerDetail);
        m_BtnPnlHobbies.onClick.AddListener(OpenHobbiesDetail);

        RefGObjects();

        SNMainControl.Api.OnReloadProfileEvent += ReloadProfile;
    }

    private void OnDestroy()
    {
        SNMainControl.Api.OnReloadProfileEvent -= ReloadProfile;
    }

    private void RefGObjects()
    {
        for (int i = 0; i < m_ListPnl.Count; i++)
        {
            int index = i;

            Button btnEdit = m_ListPnl[i].transform.Find("BtnEdit").GetComponent<Button>();
            m_ListPnlEdit[i] = transform.Find("Viewport/Content/" + m_ListPnl[i].transform.parent.name + "Edit").GetComponent<SNMainProfileEditView>();

            m_ListPnl[i].transform.parent.GetComponent<SNMainProfileItemView>().Init();
            m_ListPnlEdit[i].Init();

            btnEdit.onClick.AddListener(delegate { OpenEditPnl(m_ListPnlEdit[index]); });
        }
    }

    private void ReloadProfile()
    {
        Debug.Log("GOOO1 " + m_ListPnl.Count);
        for (int i = 0; i < m_ListPnl.Count; i++)
        {
            m_ListPnl[i].transform.parent.GetComponent<SNMainProfileItemView>().Init(i == 0);
            Debug.Log("GOOO");
        }
    }

    private void OpenEditPnl(SNMainProfileEditView pnlEdit)
    {
        pnlEdit.transform.gameObject.SetActive(true);

        if (pnlEdit.transform.name == "PnlCareerEdit")
        {
            m_ListPnlEdit[2].StartGetDropdownFields();
        }
    }

    private void OpenHobbiesDetail()
    {
        ShowPnl(m_BodyHobbies);
    }

    private void OpenCareerDetail()
    {
        ShowPnl(m_BodyCareer);
    }

    private void OpenContactDetail()
    {
        ShowPnl(m_BodyContact);
    }

    private void OpenProfileDetail()
    {
        ShowPnl(m_BodyProfile);
    }

    private void ShowPnl(GameObject pnl)
    {
        SNControl.Api.OpenPanel(pnl, m_ListPnl, true);
        SNProfileControl.Api.OnCloseEditPnl(pnl.transform.parent.name);
    }
}
