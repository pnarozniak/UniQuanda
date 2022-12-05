using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Domain.ValueObjects
{
    public class Content
    {
        public int Id { get; set; }
        public string RawText { get; set; }
        public string Text { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
