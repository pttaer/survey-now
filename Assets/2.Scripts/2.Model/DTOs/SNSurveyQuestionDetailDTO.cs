using System.Collections.Generic;

public class SNSectionQuestionRowOptionDTO
{
    public int questionId { get; set; }
    public int order { get; set; }
    public bool isCustom { get; set; }
    public int totalChoose { get; set; }
    public string content { get; set; }
}

public class SNSectionQuestionColumnOptionDTO
{
    public int questionId { get; set; }
    public int order { get; set; }
    public string content { get; set; }
}

public class SNSectionQuestionDTO
{
    public int id { get; set; }
    public int order { get; set; }
    public string type { get; set; }
    public bool isRequire { get; set; }
    public int totalAnswer { get; set; }
    public string? multipleOptionType { get; set; }
    public int? limitNumber { get; set; }
    public string title { get; set; }
    public string? resourceUrl { get; set; }
    public List<SNSectionQuestionRowOptionDTO> rowOptions { get; set; }
    public List<SNSectionQuestionColumnOptionDTO> columnOptions { get; set; }
}

public class SNSectionDTO
{
    public int order { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int totalQuestion { get; set; }
    public List<SNSectionQuestionDTO> questions { get; set; }
}

public class SNSurveyQuestionDetailDTO
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int totalQuestion { get; set; }
    public int totalAnswer { get; set; }
    public int totalValidAnswer { get; set; }
    public string status { get; set; }
    public bool isDelete { get; set; }
    public string? packType { get; set; }
    public float point { get; set; }
    public string startDate { get; set; }
    public string expiredDate { get; set; }
    public string createdDate { get; set; }
    public string modifiedDate { get; set; }
    public int createdUserId { get; set; }
    public string createdUserFullName { get; set; }
    public List<SNSectionDTO> sections { get; set; }
}