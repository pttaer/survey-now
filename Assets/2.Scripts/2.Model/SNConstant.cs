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

    public const string LOGIN = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/authentication/login";
    public const string REGISTER = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/authentication/register";

    public const string PACKS_ALL = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/packs/all";
    public const string PACKS_CALCULATE = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/packs/calculate";
    public const string PACKS_RECOMMEND = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/packs/recommend";
    public const string PACKS_PURCHASE = "http://survey-now.us-east-1.elasticbeanstalk.com/api/v1/packs/purchase";

    #endregion API_URI

    // Methods
    public const string METHOD_GET = "GET";
    public const string METHOD_POST = "POST";
    public const string METHOD_PUT = "PUT";
    public const string METHOD_DELETE = "DELETE";

    // title text

    // keys
    private static List<string> questionTypes = new List<string>
    {
        "Text",
        "Radio",
        "CheckBox",
        "Selection",
        "Rating",
        "Likert"
    };

    private static List<string> multipleOptionTypes = new List<string>
    {
        "NoLimit",
        "EqualTo",
        "AtMost"
    };

    private static List<string> packTypes = new()
    {
        "Basic",
        "Medium",
        "Advanced",
        "Expert"
    };

    public static List<string> MultipleOptionTypes { get => multipleOptionTypes; set => multipleOptionTypes = value; }
    public static List<string> QuestionTypes { get => questionTypes; set => questionTypes = value; }
    public static List<string> PackTypes { get => packTypes; set => packTypes = value; }

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
    public static string BEARER_TOKEN = "";
#elif UNITY_ANDROID
    public static string BEARER_TOKEN = "";
#endif
}