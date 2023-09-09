public enum BWLoadingType
{
    Default,
    WithBackground
}

public enum BWPopupIconType
{
    Normal,
    Success,
    Error
}

public class SNModel
{
    public static SNModel m_api;

    public static SNModel Api
    {
        get
        {
            if (m_api == null)
                m_api = new SNModel();
            return m_api;
        }
    }

    public bool IsUnloadingScene = false;
}