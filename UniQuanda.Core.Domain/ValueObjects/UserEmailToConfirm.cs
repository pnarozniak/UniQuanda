namespace UniQuanda.Core.Domain.ValueObjects;

public class UserEmailToConfirm
{
    public int IdUser { get; set; }
    public int? IdEmail { get; set; }
    public string? Email { get; set; }
    public string ConfirmationToken { get; set; }
    public DateTime ExistsUntil { get; set; }
}