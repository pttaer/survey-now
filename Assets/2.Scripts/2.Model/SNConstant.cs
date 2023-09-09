using UnityEngine;

public class SNConstant
{
    #region SCENE_NAMES

    // Scene names

    public static string SCENE_LOADFIRST // name scene load first
    {
        get { return "FAMLoadFirst"; }
    }

    public static string SCENE_LOGIN // name scene LOGIN
    {
        get { return "FAMLogin"; }
    }

    public static string SCENE_MAIN // name scene HOME
    {
        get { return "FAMMain"; }
    }

    public static string SCENE_MENU // name scene MENU
    {
        get { return "FAMMenu"; }
    }

    public static string SCENE_NEWS_AND_EVENTS // name scene NEWS
    {
        get { return "FAMNewsAndEvents"; }
    }

    public static string SCENE_POST // name scene NEWS
    {
        get { return "FAMPost"; }
    }

    public static string SCENE_DETAILS // name scene NEWS
    {
        get { return "FAMDetails"; }
    }

    public static string SCENE_MEMBERS_APPROVE // name scene EVENTS
    {
        get { return "FAMMembersApprove"; }
    }

    public static string SCENE_MEMBERS // name scene EVENTS
    {
        get { return "FAMMembers"; }
    }

    public static string SCENE_MEMBERS_GRADE // name scene EVENTS
    {
        get { return "FAMMembers.Grade"; }
    }

    public static string SCENE_MEMBERS_CLASS // name scene EVENTS
    {
        get { return "FAMMembers.Class"; }
    }

    public static string SCENE_SETTINGS // name scene SETTINGS
    {
        get { return "FAMSettings"; }
    }

    public static string SCENE_PROFILE // name scene PROFILE
    {
        get { return "FAMProfile"; }
    }

    public static string SCENE_SCHOOL_DETAIL // name scene PROFILE
    {
        get { return "FAMSchool"; }
    }

    #endregion SCENE_NAMES

    #region PREFABS

    // prefab

    public static string CONFIG_PREFAB_LOADING // name prefab loading
    {
        get { return "Loading"; }
    }

    public static string CONFIG_PREFAB_LOADING_BG // name prefab loading with background
    {
        get { return "LoadingBG"; }
    }

    public static string CONFIG_PREFAB_POPUP_MESSAGE // name prefab popup message
    {
        get { return "PopupMessage"; }
    }

    public static string CONFIG_PREFAB_FAM_POPUP // name prefab FAM popup
    {
        get { return "FAMPopup"; }
    }

    #endregion PREFABS

    #region FORMAT

    // format
    public static string FORMAT_DATETIME_12_HOURS
    {
        get { return "hh:mm tt"; }
    }

    public static string FORMAT_TIMESPAN_IN_DAY // format timespan 24h for timespan
    {
        get { return @"hh\:mm"; }
    }

    public static string FORMAT_TIMESPAN_DURATION_TO_HOUR // format timespan duration with total hours > 0
    {
        get { return @"hh\:mm\:ss"; }
    }

    public static string FORMAT_TIMESPAN_DURATION_TO_MINUTE // format timespan duration with total hours <= 0
    {
        get { return @"mm\:ss"; }
    }

    #endregion FORMAT

    #region API_URI

    public const string EVENTS_GET_ALL = "https://alumniproject.azurewebsites.net/alumni/api/events?pageNo=1&pageSize=10";
    public const string EVENTS_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/alumni/api/events?pageNo={0}&pageSize=10";

    public const string NEWS_GET_ALL = "https://alumniproject.azurewebsites.net/tenant/api/news?pageNo=1&pageSize=10";
    public const string NEWS_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/tenant/api/news?pageNo={0}&pageSize=10";
    public const string NEWS_TAGS_GET_ALL = "https://alumniproject.azurewebsites.net/tenant/api/tagnews";
    public const string NEWS_TAGS_POST = "https://alumniproject.azurewebsites.net/tenant/api/tagnews";

    public const string GRADE_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/tenant/api/grades?pageNo={0}&pageSize=10";
    public const string GRADE_POST = "https://alumniproject.azurewebsites.net/tenant/api/grades";

    public const string CLASS_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/tenant/api/classes?gradeId={0}&pageNo={1}&pageSize=10";
    public const string CLASS_POST = "https://alumniproject.azurewebsites.net/tenant/api/classes";

    public const string MEMBER_GET_ALL = "https://alumniproject.azurewebsites.net/tenant/api/alumnis/filter?pageNo=1&pageSize=10";
    public const string MEMBER_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/tenant/api/class/{0}/alumnis?pageNo={1}&pageSize=10";
    public const string MEMBER_REQUEST_GET_ALL = "https://alumniproject.azurewebsites.net/tenant/api/accessReqeuest?pageNo=1&pageSize=10";
    public const string MEMBER_REQUEST_GET_ONE_PAGE = "https://alumniproject.azurewebsites.net/tenant/api/accessReqeuest?pageNo={0}&pageSize=10";
    public const string MEMBER_ACCESS_REQUEST = "https://alumniproject.azurewebsites.net/tenant/api/accessReqeuest";

    public const string ALUMNI_GET_SELF_DETAILS = "https://alumniproject.azurewebsites.net/alumni/api/alumnis";
    public const string ALUMNI_GET_SELF_SCHOOL_DETAILS = "https://alumniproject.azurewebsites.net/alumni/api/schools";

    public const string SCHOOL_ACCESS_REQUEST = "https://alumniproject.azurewebsites.net/tenant/api/schools/id";
    public const string SCHOOL_POST = "https://alumniproject.azurewebsites.net/alumni/api/schools";
    public const string SCHOOL_CHECK_CREATE_REQUEST = "https://alumniproject.azurewebsites.net/tenant/api/schools/isAcceptRequest";
    public const string SCHOOL_CHECK_EXIST_REQUEST = "https://alumniproject.azurewebsites.net/tenant/api/schools/isExistRequest";

    public const string NEWS = "https://alumniproject.azurewebsites.net/tenant/api/news";
    public const string EVENTS = "https://alumniproject.azurewebsites.net/tenant/api/events";

    public const string GRADE = "https://alumniproject.azurewebsites.net/tenant/api/grades";
    public const string CLASS = "https://alumniproject.azurewebsites.net/tenant/api/classes";

    #endregion API_URI

    // Methods
    public const string METHOD_GET = "GET";
    public const string METHOD_POST = "POST";
    public const string METHOD_PUT = "PUT";
    public const string METHOD_DELETE = "DELETE";

    // title text
    public const string TITLE_HOME = "Admin";

    public const string TITLE_MEMBERS = "Members";
    public const string TITLE_GRADES = "Grades";
    public const string TITLE_CLASSES = "Classes";
    public const string TITLE_MEMBERS_APPROVE = "Members Approve";
    public const string TITLE_MEMBERS_DETAILS = "Members Details";
    public const string TITLE_NEWS_DETAILS = "News Details";
    public const string TITLE_EVENTS_DETAILS = "Events Details";
    public const string TITLE_PROFILE = "Profile";
    public const string TITLE_SETTINGS = "Settings";
    public const string TITLE_POST = "News & Events Post";
    public const string TITLE_SCHOOL_DETAIL = "School Detail";

    // keys
    public const string KEY_HOME = "Home";

    public const string KEY_NEWS_AND_EVENTS = "News";
    public const string KEY_EVENTS = "Events";

    public const string KEY_MEMBERS = "Members";

    // playerprefs
    public const string EMAIL_CACHE = "email_cache";

    public const string PASSWORD_CACHE = "password_cache";

    public const string BEARER_TOKEN_CACHE = "bearer_token_cache";

    public const string PROFILE_CACHE = "profile_cache";

    public const string SCHOOL_CACHE = "school_cache";

    // tween constant
    public const string TIME_UNIT = " mins";

    public const float TWEEN_DURATION = 0.3f;

    // date format
    public const string DATE_FORMAT_SHORT = "dd MMM";

    public const string DATE_FORMAT_FULL = "d/M/yyyy";

    // colors
    public static Color MAIN_COLOR_GREEN { get => m_MainColor; }

    public static Color MAIN_LIGHT_COLOR_GREEN { get => m_MainLightColor; }

    private static Color m_MainColor = new Color(115f / 255f, 209f / 255f, 61f / 255f);

    private static Color m_MainLightColor = new Color(115f / 255f, 209f / 255f, 61f / 255f, 86f / 255f);

    // api constant
    public enum REQUEST_STATUS
    {
        APPROVE = 2,
        REJECT
    }

#if UNITY_EDITOR
    public static string BEARER_TOKEN = "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJhbHVtbmlJZCI6IjkiLCJzY2hvb2xJZCI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ0ZW5hbnQiLCJleHAiOjE2OTAwNzUwNDJ9.LpyKk9j42kyGN76Qwr349mO_e1nwQjviRDlF1rUXez7P0Bdl6-KtoUeew_aTOPon7ZqVHS-Y84BahT_q2gP0dw";
#elif UNITY_ANDROID
    public static string BEARER_TOKEN = "";
#endif
}