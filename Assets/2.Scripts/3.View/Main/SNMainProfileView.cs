using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<GameObject> m_ListPnl;

    public void Init()
    {
        m_BtnPnlProfile = transform.Find("Viewport/Content/PnlProfile/TopBar").GetComponent<Button>();
        m_BtnPnlContact = transform.Find("Viewport/Content/PnlContact/TopBar").GetComponent<Button>();
        m_BtnPnlCareer = transform.Find("Viewport/Content/PnlCareer/TopBar").GetComponent<Button>();
        m_BtnPnlHobbies = transform.Find("Viewport/Content/PnlHobbies/TopBar").GetComponent<Button>();

        m_BodyProfile = transform.Find("Viewport/Content/PnlProfile/Body").gameObject;
        m_BodyContact = transform.Find("Viewport/Content/PnlContact/Body").gameObject;
        m_BodyCareer = transform.Find("Viewport/Content/PnlCareer/Body").gameObject;
        m_BodyHobbies = transform.Find("Viewport/Content/PnlHobbies/Body").gameObject;

        m_ListPnl = new()
        {
            m_BodyProfile,
            m_BodyContact,
            m_BodyCareer,
            m_BodyHobbies
        };

        m_BtnPnlProfile.onClick.AddListener(OpenProfileDetail);
        m_BtnPnlContact.onClick.AddListener(OpenContactDetail);
        m_BtnPnlCareer.onClick.AddListener(OpenCareerDetail);
        m_BtnPnlHobbies.onClick.AddListener(OpenHobbiesDetail);
    }

    private void OpenHobbiesDetail()
    {
        SNControl.Api.OpenPanel(m_BodyHobbies, m_ListPnl);
    }

    private void OpenCareerDetail()
    {
        SNControl.Api.OpenPanel(m_BodyCareer, m_ListPnl);
    }

    private void OpenContactDetail()
    {
        SNControl.Api.OpenPanel(m_BodyContact, m_ListPnl);
    }

    private void OpenProfileDetail()
    {
        SNControl.Api.OpenPanel(m_BodyProfile, m_ListPnl);
    }
}
