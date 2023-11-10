using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserResponseDTO : MonoBehaviour
{
    public long? Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string FullName { get; set; }

    public Gender Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string AvatarUrl { get; set; }

    public UserStatus Status { get; set; }

    public Role Role { get; set; }

    public RelationshipStatus RelationshipStatus { get; set; }

    public string LangKey { get; set; }

    public string Currency { get; set; }

    public AddressDto Address { get; set; }

    public OccupationDto Occupation { get; set; }
}

public class AddressDto
{
    public long? Id { get; set; }

    public string Detail { get; set; }

    public ProvinceDto Province { get; set; }

    public CityDto City { get; set; }

    public DistrictDto District { get; set; }
}

public class ProvinceDto
{
    public long? Id { get; set; }

    public string Name { get; set; }
}

public class CityDto
{
    public long? Id { get; set; }

    public string Name { get; set; }
}

public class DistrictDto
{
    public long? Id { get; set; }

    public string Name { get; set; }
}

public class OccupationDto
{
    public long? Id { get; set; }

    public double? Income { get; set; }

    public string PlaceOfWork { get; set; }

    public string Currency { get; set; }

    public FieldDto Field { get; set; }
}

public class FieldDto
{
    public long? Id { get; set; }

    public string Name { get; set; }
}

public enum Gender { Male, Female, Other }
public enum UserStatus { Active, InActive, Suspending }
public enum Role { User, Admin }