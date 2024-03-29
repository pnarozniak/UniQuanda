namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class TempUser
{
    public string EmailConfirmationCode { get; set; }
    public DateTime ExistsUntil { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Contact { get; set; }
    public string? City { get; set; }

    public int IdUser { get; set; }
    public virtual User IdUserNavigation { get; set; }
}