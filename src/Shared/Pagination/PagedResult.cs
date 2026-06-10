namespace StarterKit.Api.Shared.Pagination;
public sealed record PagedRequest(int PageNumber=1,int PageSize=20);
public sealed record PagedResult<T>(IReadOnlyList<T> Items,int PageNumber,int PageSize,int TotalCount);
