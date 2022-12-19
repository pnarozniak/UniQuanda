namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class GlobalRanking
    {
        public int Place { get; set; }
        public int AppUserId { get; set; }
        public int Points { get; set; }
        public virtual AppUser AppUserIdNavigation { get; set; }
    }
}
