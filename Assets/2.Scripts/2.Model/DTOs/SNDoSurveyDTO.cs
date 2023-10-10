using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNDoSurveyDTO : MonoBehaviour
{
    public class AnswerOptionDTO
    {
        public int? RowOrder { get; set; }
        public int? ColumnOrder { get; set; }
        public string? Content { get; set; }
    }

    public class AnswerDTO
    {
        public int QuestionId { get; set; }
        public string? Content { get; set; }
        public int? RateNumber { get; set; }
        public List<AnswerOptionDTO> AnswerOptions { get; set; }
    }

    public class SurveyDTO
    {
        public int SurveyId { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
