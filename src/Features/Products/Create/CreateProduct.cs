using FluentValidation; 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
using StarterKit.Api.Infrastructure.Persistence.Context;

namespace StarterKit.Api.Features.Products.Create;
public sealed record CreateProductRequest(string Name,decimal Price,string Sku);
public sealed record ProductResponse(Guid Id,string Name,decimal Price,string Sku,DateTime CreatedAtUtc);
public sealed record CreateProductCommand(string Name,decimal Price,string Sku):IRequest<ProductResponse>;
public sealed class CreateProductValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    { 
        RuleFor(x=>x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x=>x.Price).GreaterThan(0);
        RuleFor(x=>x.Sku).NotEmpty().MaximumLength(64);
    }
}
public sealed class CreateProductHandler(AppDbContext db): IRequestHandler<CreateProductCommand,ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand createProductCommand,CancellationToken cancellationToken )
    {
        var p = new Product(createProductCommand.Name,
                           createProductCommand.Price,
                           createProductCommand.Sku);
        db.Products.Add(p); 
        await db.SaveChangesAsync(cancellationToken); 
        return new(p.Id,p.Name,p.Price,p.Sku,p.CreatedAtUtc);
    } 
}
public static class CreateProductEndpoint 
{
    public static IEndpointRouteBuilder MapCreateProduct(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/products",  async (CreateProductRequest req,ISender sender,CancellationToken ct)
            => Results.Created("/api/v1/products", await sender.Send(new CreateProductCommand(req.Name,req.Price,req.Sku),ct))).WithTags("Products");
        return app;
    }
}
