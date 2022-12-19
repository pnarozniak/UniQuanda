using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Domain.ValueObjects;

public class AuthorContent
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string? AvatarUrl { get; set; }
    public IEnumerable<AcademicTitleEntity> AcademicTitles { get; set; }
}
