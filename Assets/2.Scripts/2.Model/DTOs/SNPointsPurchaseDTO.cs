using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNPointsPurchaseDTO
{
    public string status { get; set; }
    public string message { get; set; }
    public int pointAmount { get; set; }
    public int moneyAmount { get; set; }
    public string currency { get; set; }
    public string paymentMethod { get; set; }
    public string destinationAccount { get; set; }
    public string description { get; set; }
    public int transactionId { get; set; }
}
