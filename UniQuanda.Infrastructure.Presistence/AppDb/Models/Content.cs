using NpgsqlTypes;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string RawText { get; set; }
        public string Text { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        public virtual ICollection<ImageInContent> ImagesInContent { get; set; }
        public virtual Question QuestionIdNavigation { get; set; }
        public virtual Answer AnswerIdNavigation { get; set; }
        public NpgsqlTsVector SearchVector { get; set; }
    }
}
