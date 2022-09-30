using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Domain.Entities
{
    public class AcademicTitleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AcademicTitleEnum Type { get; set; }
        public int Order { get; set; }
    }
}
