using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string HashedPassword { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public UserOptionalInfo OptionalInfo { get; set; }
        public IEnumerable<string> Emails { get; set; }
    }
}
