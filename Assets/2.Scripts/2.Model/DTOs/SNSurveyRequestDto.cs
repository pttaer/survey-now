using System.Collections.Generic;
using System;

public class SNSurveyRequestDTO
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime ExpiredDate { get; set; }

    public List<SNSectionRequestDTO> Sections { get; set; }
}

public class SNSectionRequestDTO
{
    public int Order { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<SNSectionQuestionRequestDTO> Questions { get; set; }
}

public class SNSectionQuestionRequestDTO
{
    public int Order { get; set; }

    public string Type { get; set; }

    public bool? IsRequired { get; set; }

    public string? MultipleOptionType { get; set; }

    public int? LimitNumber { get; set; }

    public string Title { get; set; }

    public string? ResourceUrl { get; set; }

    public List<SNRowOptionRequestDTO> RowOptions { get; set; }

    public List<SNColumnOptionRequestDTO> ColumnOptions { get; set; }
}

public class SNRowOptionRequestDTO
{
    public int Order { get; set; }

    public bool? IsCustom { get; set; }

    public string? Content { get; set; }
}

public class SNColumnOptionRequestDTO
{
    public int Order { get; set; }

    public string? Content { get; set; }
}

public class SurveyPostDateDTO
{
    public DateTime startDate { get; set; }
    public DateTime expiredDate { get; set; }
}