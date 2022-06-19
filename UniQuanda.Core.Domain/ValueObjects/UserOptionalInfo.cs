namespace UniQuanda.Core.Domain.ValueObjects
{
    public class UserOptionalInfo
    {
        public string? Avatar { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
