using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;

namespace StarterKit.Api.Features.Products.Delete;
public sealed record DeleteProductCommand(Guid Id):IRequest;
public sealed class DeleteProductHandler(ApplicationDbContext db):IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand  deleteProductCommand,CancellationToken cancellationToken)
    {
        var product = await db.Products.SingleOrDefaultAsync(x => x.Id == deleteProductCommand.Id, cancellationToken) 
                         ?? throw new KeyNotFoundException("Product not found");
        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);
    }
}
public static class DeleteProductEndpoint
{
    public static IEndpointRouteBuilder MapDeleteProduct(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/products/{id:guid}", async (Guid id,ISender sender,CancellationToken ct)=>{ await sender.Send(new DeleteProductCommand(id),ct); return Results.NoContent();}).WithTags("Products");
        return app;
    }
}
