using System.Collections.Generic;
using UnityEngine;

public class SNConstant
{
    #region SCENE_NAMES

    // Scene names

    public static string SCENE_LOADFIRST
    {
        get { return "SNLoadFirst"; }
    }
    public static string SCENE_LOGIN
    {
        get { return "SNLogin"; }
    }
    public static string SCENE_MENU
    {
        get { return "SNMenuView"; }
    }
    public static string SCENE_HOME
    {
        get { return "SNHome"; }
    }
    public static string SCENE_MAIN
    {
        get { return "SNMainView"; }
    }
    public static string SCENE_SURVEY_LIST
    {
        get { return "SNSurveyList"; }
    }
    public static string SCENE_CREATE_SURVEY
    {
        get { return "SNCreateSurvey"; }
    }
    public static string SCENE_BUNDLE
    {
        get { return "SNBundle"; }
    }
    public static string SCENE_PAYMENT
    {
        get { return "SNPayment"; }
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

    public const string LOGIN = "https://survey-now-fptu-production.somee.com/api/v1/authentication/login";
    public const string REGISTER = "https://survey-now-fptu-production.somee.com/api/v1/authentication/register";

    public const string USER_UPDATE_INFO = "https://survey-now-fptu-production.somee.com/api/v1/users/{0}";
    public const string USER_CURRENT_INFO = "https://survey-now-fptu-production.somee.com/api/v1/users/login-user";
    public const string USER_GET_FIELDS = "https://survey-now-fptu-production.somee.com/api/v1/occupations/fields";

    public const string OCCUPATION = "https://survey-now-fptu-production.somee.com/api/v1/occupation";

    public const string PACKS_ALL = "https://survey-now-fptu-production.somee.com/api/v1/packs/all";
    public const string PACKS_CALCULATE = "https://survey-now-fptu-production.somee.com/api/v1/packs/calculate";
    public const string PACKS_RECOMMEND = "https://survey-now-fptu-production.somee.com/api/v1/packs/recommend";
    public const string PACKS_PURCHASE = "https://survey-now-fptu-production.somee.com/api/v1/packs/purchase";

    public const string POINTS_PURCHASE = "https://survey-now-fptu-production.somee.com/api/v1/points/purchase/momo";
    public const string POINTS_PURCHASE_REQUEST = "https://survey-now-fptu-production.somee.com/api/v1/points/purchase/request";
    public const string POINTS_PURCHASE_RETURN = "https://survey-now-fptu-production.somee.com/api/v1/points/purchase/momo/return";
    public const string POINTS_PURCHASE_HISTORY = "https://survey-now-fptu-production.somee.com/api/v1/points/history";

    public const string SURVEY_CREATE = "https://survey-now-fptu-production.somee.com/api/v1/surveys";
    public const string SURVEY_GET_HOME = "https://survey-now-fptu-production.somee.com/api/v1/surveys";
    public const string SURVEY_GET_ALL = "https://survey-now-fptu-production.somee.com/api/v1/surveys";
    public const string SURVEY_GET_MY_SURVEY = "https://survey-now-fptu-production.somee.com/api/v1/account/surveys";
    public const string SURVEY_SET_ACTIVE = "https://survey-now-fptu-production.somee.com/api/v1/surveys/status/{0}";
    public const string SURVEY_GET_DETAIL = "https://survey-now-fptu-production.somee.com/api/v1/surveys/{0}";
    public const string SURVEY_PUT = "https://survey-now-fptu-production.somee.com/api/v1/surveys/{0}";
    public const string SURVEY_PATCH = "https://survey-now-fptu-production.somee.com/api/v1/surveys/status/{0}";
    public const string SURVEY_DO = "https://survey-now-fptu-production.somee.com/api/v1/surveys/do-survey";
    public const string SURVEY_DELETE = "https://survey-now-fptu-production.somee.com/api/v1/surveys/{0}";
    public const string SURVEY_POST = "https://survey-now-fptu-production.somee.com/api/v1/surveys/{0}/post-survey";
    public const string SURVEY_HISTORY = "https://survey-now-fptu-production.somee.com/api/v1/account/history";
    public const string SURVEY_HISTORY_DETAIL = "https://survey-now-fptu-production.somee.com/api/v1/account/surveys/{0}/answer";

    public const string REDEEM_MONEY = "https://survey-now-fptu-production.somee.com/api/v1/points/redeem/request";

    #endregion API_URI

    // Methods
    public const string METHOD_GET = "GET";
    public const string METHOD_POST = "POST";
    public const string METHOD_PUT = "PUT";
    public const string METHOD_DELETE = "DELETE";
    public const string METHOD_PATCH = "PATCH";

    // title text

    // keys
    public static readonly List<string> questionTypes = new()
    {
        "Text",
        "Radio",
        "CheckBox",
        "Selection",
        "Rating",
        "Likert"
    };

    public static readonly List<string> multipleOptionTypes = new()
    {
        "NoLimit",
        "EqualTo",
        "AtMost"
    };

    public static readonly List<string> packTypes = new()
    {
        "Basic",
        "Medium",
        "Advanced",
        "Expert"
    };

    // playerprefs
    public const string EMAIL_CACHE = "email_cache";

    public const string PASSWORD_CACHE = "password_cache";

    public const string BEARER_TOKEN_CACHE = "bearer_token_cache";

    public const string PROFILE_CACHE = "profile_cache";

    public const string USER_ID = "user_id";

    public const string USER_EMAIL_CACHE = "user_email";

    public const string USER_FULLNAME_CACHE = "full_name";

    // tween constant
    public const string TIME_UNIT = " mins";

    public const float TWEEN_DURATION = 0.3f;

    // date format
    public const string DATE_FORMAT_SHORT = "dd MMM";

    public const string DATE_FORMAT_FULL = "d/M/yyyy";

    // colors
    public static Color MAIN_COLOR_GREEN { get => m_MainColor; }

    public static Color MAIN_LIGHT_COLOR_GREEN { get => m_MainLightColor; }

    private static Color m_MainColor = new(0f, 0.69f, 0.31f);

    private static Color m_MainLightColor = new(115f / 255f, 209f / 255f, 61f / 255f, 86f / 255f);

    private static Color32 m_RedTextColor = new(202, 33, 43, 255);

    private static Color32 m_GreenTextColor = new(34, 130, 34, 255);

    // api constant
    public enum REQUEST_STATUS
    {
        APPROVE = 2,
        REJECT
    }

#if UNITY_EDITOR
    public static string BEARER_TOKEN = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0IiwiZW1haWwiOiJ1c2VyMUBnbWFpbC5jb20iLCJyb2xlIjoiVXNlciIsIm5iZiI6MTY5NjQ3NzYwMCwiZXhwIjoxNjk2NzM2ODAwLCJpYXQiOjE2OTY0Nzc2MDB9.0wIsn-p-Cwtod9otv3cWMDO-Rx20LbsktHMDmZF9UgY";
#elif UNITY_ANDROID
    public static string BEARER_TOKEN = "";
#endif
    public static string BEARER_TOKEN_EDITOR;
}