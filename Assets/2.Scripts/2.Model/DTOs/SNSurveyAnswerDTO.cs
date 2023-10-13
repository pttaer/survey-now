using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSurveyAnswerDTO : MonoBehaviour
{
    public class SectionDto
    {
        public int order { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int totalQuestion { get; set; }
        public List<QuestionDto> questions { get; set; }
    }

    public class QuestionDto
    {
        public int id { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public bool isRequire { get; set; }
        public int totalAnswer { get; set; }
        public string multipleOptionType { get; set; }
        public int limitNumber { get; set; }
        public string title { get; set; }
        public string resourceUrl { get; set; }
        public List<RowOptionDto> rowOptions { get; set; }
        public List<ColumnOptionDto> columnOptions { get; set; }
        public List<AnswerDto> answers { get; set; }
    }

    public class RowOptionDto
    {
        public int questionId { get; set; }
        public int order { get; set; }
        public bool isCustom { get; set; }
        public int totalChoose { get; set; }
        public string content { get; set; }
    }

    public class ColumnOptionDto
    {
        public int questionId { get; set; }
        public int order { get; set; }
        public string content { get; set; }
    }

    public class AnswerDto
    {
        public string content { get; set; }
        public int rateNumber { get; set; }
        public List<AnswerOptionDto> answerOptions { get; set; }
    }

    public class AnswerOptionDto
    {
        public int rowOrder { get; set; }
        public int columnOrder { get; set; }
        public string content { get; set; }
    }

    public class SurveyDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int totalQuestion { get; set; }
        public int totalAnswer { get; set; }
        public int totalValidAnswer { get; set; }
        public string status { get; set; }
        public bool isDelete { get; set; }
        public string packType { get; set; }
        public int point { get; set; }
        public string startDate { get; set; }
        public string expiredDate { get; set; }
        public string createdDate { get; set; }
        public string modifiedDate { get; set; }
        public int createdUserId { get; set; }
        public string createdUserFullName { get; set; }
        public List<SectionDto> sections { get; set; }
    }
}
