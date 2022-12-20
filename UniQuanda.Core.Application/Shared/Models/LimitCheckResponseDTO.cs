using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Shared.Models
{
    public class LimitCheckResponseDTO
    {
        /// <summary>
        ///     How many times, permission was used. If permission is not limited, this value is null.
        /// </summary>
        public int? UsedTimes { get; set; }
        /// <summary>
        ///    How many times, permission can be used. If null, permission can be used unlimited times
        /// </summary>
        public int? MaxTimes { get; set; }
        /// <summary>
        ///     When usages of permission will be cleared, If null, permission is not limited.
        /// </summary>
        public DurationEnum? ShortestRefreshPeriod { get; set; }
    }
}
