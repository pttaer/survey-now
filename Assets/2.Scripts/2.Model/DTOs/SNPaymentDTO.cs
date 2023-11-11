using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNPaymentDTO
{
    public int pointAmount { get; set; }
    public string paymentMethod { get; set; }
    public int userId { get; set; }
    public string platform { get; set; }

    public SNPaymentDTO(int pointAmount)
    {
        this.pointAmount = pointAmount;
        this.paymentMethod = "Momo";
        this.userId = (int)SNModel.Api.CurrentUser.Id;
        this.platform = "Mobile";
    }

    public SNPaymentDTO(int pointAmount, string paymentMethod)
    {
        this.pointAmount = pointAmount;
        this.paymentMethod = paymentMethod;
    }
}
