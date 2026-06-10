using Dapper; 
using MediatR;
using StarterKit.Api.Features.Products.Create;


namespace StarterKit.Api.Features.Products.GetById;
public sealed record GetProductByIdQuery(Guid Id):IRequest<ProductResponse?>;
public sealed class GetProductByIdHandler(IDbConnectionFactory factory):IRequestHandler<GetProductByIdQuery,ProductResponse?>
{
    public async Task<ProductResponse?> Handle(GetProductByIdQuery  getProductByIdQuery,CancellationToken cancellationToken)
    {
        using var c=factory.CreateConnection(); 
        return await c.QuerySingleOrDefaultAsync<ProductResponse>("SELECT Id,Name,Price,Sku,CreatedAtUtc FROM Products WHERE Id=@Id AND IsDeleted=0",
            new{getProductByIdQuery.Id});
    }
}
public static class GetProductByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetProductById(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/products/{id:guid}",
            async (Guid id,ISender sender,CancellationToken ct)=> 
            await sender.Send(new GetProductByIdQuery(id),ct) is {} p ? Results.Ok(p) : Results.NotFound())
            .WithTags("Products").CacheOutput(x=>x.Expire(TimeSpan.FromSeconds(30)));
        return app; 
    }
}
