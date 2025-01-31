public class SNUserDTO
{
    public long? Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string DateOfBirth { get; set; }
    public string AvatarUrl { get; set; }
    public float Point { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public string IsDelete { get; set; }
    public string LangKey { get; set; }
    public string Currency { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Occupation { get; set; }

    public enum GenderEnum
    {
        Male,
        Female
    }
}
