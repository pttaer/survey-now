using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNMomoRedirect : MonoBehaviour
{
    public string partnerCode { get; set; }
    public string orderId { get; set; }
    public string requestId { get; set; }
    public long amount { get; set; }
    public string orderInfo { get; set; }
    public string orderType { get; set; }
    public string transId { get; set; }
    public string resultCode { get; set; }
    public string message { get; set; }
    public string payType { get; set; }
    public string responseTime { get; set; }
    public string extraData { get; set; }
    public string signature { get; set; }

    public SNMomoRedirect() { }

    public SNMomoRedirect(
     string partnerCode,
     string orderId,
     string requestId,
     long amount,
     string orderInfo,
     string orderType,
     string transId,
     string resultCode,
     string message,
     string payType,
     string responseTime,
     string extraData,
     string signature)
    {
        this.partnerCode = partnerCode;
        this.orderId = orderId;
        this.requestId = requestId;
        this.amount = amount;
        this.orderInfo = orderInfo;
        this.orderType = orderType;
        this.transId = transId;
        this.resultCode = resultCode;
        this.message = message;
        this.payType = payType;
        this.responseTime = responseTime;
        this.extraData = extraData;
        this.signature = signature;
    }
}
