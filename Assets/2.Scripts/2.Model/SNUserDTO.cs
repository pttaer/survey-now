public class SNUserDTO
{
    public long? Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public GenderEnum Gender { get; set; }
    public string AvatarUrl { get; set; }
    public decimal Point { get; set; }
    public string Token { get; set; }

    public enum GenderEnum
    {
        Male,
        Female
    }
}
