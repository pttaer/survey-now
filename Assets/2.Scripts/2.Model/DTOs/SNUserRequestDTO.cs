using System;
using UnityEngine;

public class SNUserRequestDTO
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public RelationshipStatus RelationshipStatus { get; set; }
    public string Email { get; set; }
    public string LangKey { get; set; }
    public string Currency { get; set; }
    public AddressRequest Address { get; set; }
    public OccupationRequest Occupation { get; set; }
}

public enum RelationshipStatus
{
    Single,
    Dating,
    Married,
    Other
}

public class AddressRequest
{
    public long Id { get; set; }
    public string Detail { get; set; }
    public long ProvinceId { get; set; }
    public long CityId { get; set; }
    public long DistrictId { get; set; }
}

public class OccupationRequest
{
    public double? Income { get; set; }
    public string PlaceOfWork { get; set; }
    public string Currency { get; set; }
    public long? FieldId { get; set; }
    public long? PositionId { get; set; }
}
