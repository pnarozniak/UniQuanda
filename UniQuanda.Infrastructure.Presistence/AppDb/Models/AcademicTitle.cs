using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class AcademicTitle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public AcademicTitleEnum AcademicTitleType { get; set; }
    public virtual ICollection<AppUserTitle> UsersTitle { get; set; }
    public virtual ICollection<TitleRequest> TitleRequests { get; set; }
}