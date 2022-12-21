using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class AppRoleEntity
    {
        public int Id { get; set; }
        public AppRole Name { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
