using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNMomoReturn 
{
    public string status { get; set; }
    public string message { get; set; }
    public int pointAmount { get; set; }
    public int moneyAmount { get; set; }
    public string transactionId { get; set; }
    public string eWalletTransactionId { get; set; }
    public string paymentMethod { get; set; }
}
