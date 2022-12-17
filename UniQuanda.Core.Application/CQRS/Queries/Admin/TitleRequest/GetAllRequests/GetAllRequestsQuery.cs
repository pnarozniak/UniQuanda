using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Admin.TitleRequest.GetAllRequests
{
	public class GetAllRequestsQuery : IRequest<GetAllRequestsResponseDTO>
    {
		public GetAllRequestsQuery(GetAllRequestsRequestDTO request)
		{
			this.Take = request.PageSize;
			this.Skip = (request.Page - 1) * request.PageSize; 
			this.AddTotalCount = request.AddCount;
		}
		public int Take { get; set; }
		public int Skip { get; set; }
		public bool AddTotalCount { get; set; }
	}
}

