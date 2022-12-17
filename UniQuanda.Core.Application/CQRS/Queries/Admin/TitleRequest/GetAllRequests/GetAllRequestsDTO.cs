using System;
using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Admin.TitleRequest.GetAllRequests
{
	public class GetAllRequestsRequestDTO
	{
		[Required]
		public int Page { get; set; }
		[Required]
		public int PageSize { get; set; }
		[Required]
		public bool AddCount { get; set; }
	}

	public class GetAllRequestsResponseDTO
	{
		public IEnumerable<GetAllRequestsResponseDTORequest> Requests { get; set; }
		public int? TotalCount { get; set; }
	}

    public class GetAllRequestsResponseDTORequest
    {
		public int Id { get; set; }
		public string TitleName { get; set; }
		public int TitleId { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public DateTime CreateAt { get; set; }
		public string ScanUrl { get; set; }
		public string? AdditionalInfo { get; set; }
	}
}

