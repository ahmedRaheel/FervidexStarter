using Dapper;
using MediatR;
using StarterKit.Api.Features.Products.Create;
using StarterKit.Api.Shared.Pagination;


namespace StarterKit.Api.Features.Products.GetPaged;
public sealed record GetPagedProductsQuery(int PageNumber,int PageSize):IRequest<PagedResult<ProductResponse>>;
public sealed class GetPagedProductsHandler(IDbConnectionFactory factory):IRequestHandler<GetPagedProductsQuery,PagedResult<ProductResponse>>
{
    public async Task<PagedResult<ProductResponse>> Handle(GetPagedProductsQuery getPagedProductsQuery,CancellationToken cancellationToken)
    {
        using var c=factory.CreateConnection();
        var offset=(getPagedProductsQuery.PageNumber-1)*getPagedProductsQuery.PageSize;
        var items=(await c.QueryAsync<ProductResponse>("SELECT Id,Name,Price,Sku,CreatedAtUtc FROM Products WHERE IsDeleted=0 ORDER BY CreatedAtUtc DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY",new{Offset=offset,PageSize=getPagedProductsQuery.PageSize})).ToList();
        var total=await c.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Products WHERE IsDeleted=0");
        return new PagedResult<ProductResponse>(items,getPagedProductsQuery.PageNumber,getPagedProductsQuery.PageSize,total);
    }
}
public static class GetPagedProductsEndpoint
{
    public static IEndpointRouteBuilder MapGetPagedProducts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/products", async (int pageNumber,int pageSize,ISender sender,CancellationToken ct)=> 
            Results.Ok(await sender.Send(new GetPagedProductsQuery(pageNumber==0?1:pageNumber,pageSize==0?20:pageSize),ct)))
            .WithTags("Products");
        return app;
    }
}
