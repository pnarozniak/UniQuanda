namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string HashedPassword { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExp {get; set; }

        public virtual TempUser IdTempUserNavigation {get; set;}
        public virtual ICollection<UserEmail> Emails { get; set; }
    }
}