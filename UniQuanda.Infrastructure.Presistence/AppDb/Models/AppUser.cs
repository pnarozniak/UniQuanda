namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
