using System.Collections.Generic;

public enum SNHistoryRecordType
{
    Purchase,
    Exchange
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
    public List<string> ScenesLoaded = new(); // For back state
}