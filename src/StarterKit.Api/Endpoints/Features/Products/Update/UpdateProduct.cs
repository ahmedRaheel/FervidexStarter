using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;


namespace StarterKit.Api.Features.Products.Update;
public sealed record UpdateProductRequest(string Name,decimal Price,string Sku);
public sealed record UpdateProductCommand(Guid Id,string Name,decimal Price,string Sku):IRequest;
public sealed class UpdateProductValidator:AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x=>x.Name).NotEmpty(); 
        RuleFor(x=>x.Price).GreaterThan(0);
    }
}
public sealed class UpdateProductHandler(ApplicationDbContext db):IRequestHandler<UpdateProductCommand>
{ public async Task Handle(UpdateProductCommand  updateProductCommand,CancellationToken cancellationToken)
    {
        var p=await db.Products.SingleOrDefaultAsync(x=>x.Id==updateProductCommand.Id, cancellationToken) ?? throw new KeyNotFoundException("Product not found");
        p.Update(updateProductCommand.Name, updateProductCommand.Price, updateProductCommand.Sku);
        await db.SaveChangesAsync(cancellationToken);
    }
}
public static class UpdateProductEndpoint
{
    public static IEndpointRouteBuilder MapUpdateProduct(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v1/products/{id:guid}", async (Guid id,UpdateProductRequest req,ISender sender,CancellationToken ct)=>{ await sender.Send(new UpdateProductCommand(id,req.Name,req.Price,req.Sku),ct); return Results.NoContent();}).WithTags("Products");
        return app;
    }
}
