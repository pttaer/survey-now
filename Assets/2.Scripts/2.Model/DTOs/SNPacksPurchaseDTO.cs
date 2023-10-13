using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNPacksPurchaseDTO
{
    public string packType { get; set; }
    public int surveyId { get; set; }
    public int totalParticipants { get; set; }

    public SNPacksPurchaseDTO() { }

    public SNPacksPurchaseDTO(string packType, int surveyId, int totalParticipants)
    {
        this.packType = packType;
        this.surveyId = surveyId;
        this.totalParticipants = totalParticipants;
    }
}
