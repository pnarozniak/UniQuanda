namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class UserEmail
{
    public int Id { get; set; }
    public string Value { get; set; }
    public bool IsMain { get; set; }

    public int IdUser { get; set; }
    public virtual User IdUserNavigation { get; set; }
}