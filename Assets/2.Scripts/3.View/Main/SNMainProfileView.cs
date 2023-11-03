using System.Collections.Generic;
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
        m_BtnPnlProfile = transform.Find("Viewport/Content/PnlProfile/TopBar").GetComponent<Button>();
        m_BtnPnlContact = transform.Find("Viewport/Content/PnlContact/TopBar").GetComponent<Button>();
        m_BtnPnlCareer = transform.Find("Viewport/Content/PnlCareer/TopBar").GetComponent<Button>();
        m_BtnPnlHobbies = transform.Find("Viewport/Content/PnlHobbies/TopBar").GetComponent<Button>();

        m_BodyProfile = transform.Find("Viewport/Content/PnlProfile/PnlInfo").gameObject;
        m_BodyContact = transform.Find("Viewport/Content/PnlContact/PnlInfo").gameObject;
        m_BodyCareer = transform.Find("Viewport/Content/PnlCareer/PnlInfo").gameObject;
        m_BodyHobbies = transform.Find("Viewport/Content/PnlHobbies/PnlInfo").gameObject;

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

    private void OpenEditPnl(SNMainProfileEditView pnlEdit)
    {
        pnlEdit.transform.gameObject.SetActive(true);
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
