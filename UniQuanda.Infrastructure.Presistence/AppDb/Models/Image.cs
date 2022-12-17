namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public virtual ICollection<ImageInContent> ImagesInContent { get; set; }
        public virtual TitleRequest TitleRequest { get; set; }
    }
}
