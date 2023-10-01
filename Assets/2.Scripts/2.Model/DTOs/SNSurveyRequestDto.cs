using UnityEngine;
using System.Collections.Generic;
using System;

public class SNSurveyRequestDTO
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? PackType { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public List<SNSectionRequestDTO> Sections { get; set; }
}

public enum PackType
{
    Basic,
    Medium,
    Advanced,
    Expert
}

public class SNSectionRequestDTO
{
    public int Order { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<SNQuestionRequestDTO> Questions { get; set; }
}

public class SNQuestionRequestDTO
{
    public int Order { get; set; }

    public string Type { get; set; }

    public bool? IsRequired { get; set; }

    public string? MultipleOptionType { get; set; }

    public int LimitNumber { get; set; }

    public string Title { get; set; }

    public string? ResourceUrl { get; set; }

    public List<SNRowOptionRequestDTO> RowOptions { get; set; }

    public List<SNColumnOptionRequestDTO> ColumnOptions { get; set; }
}

public enum QuestionType
{
    Text,
    Radio,
    CheckBox,
    Selection,
    Rating,
    Likert
}

public enum MultipleOptionType
{
    NoLimit,
    EqualTo,
    AtMost
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