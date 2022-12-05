namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class ImageInContent
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int ContentId { get; set; }
        public virtual Image ImageIdNavigation { get; set; }
        public virtual Content ContentIdNavigation { get; set; }
    }
}
