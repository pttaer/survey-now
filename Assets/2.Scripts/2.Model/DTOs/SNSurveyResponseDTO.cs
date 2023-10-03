using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSurveyResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int TotalQuestion { get; set; }
    public string Status { get; set; }
    public int Point { get; set; }
    public string StartDate { get; set; }
    public string ExpiredDate { get; set; }
    public int CreatedUserId { get; set; }
    public string CreatedUserFullName { get; set; }
}
