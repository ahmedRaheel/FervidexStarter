using StarterKit.Api.Features.Products.Create;
using StarterKit.Api.Features.Products.Delete;
using StarterKit.Api.Features.Products.GetById;
using StarterKit.Api.Features.Products.GetPaged;
using StarterKit.Api.Features.Products.Update;
namespace StarterKit.Api.StarterKit.Api.Extensions;
public static class EndpointExtensions
{
    /// <summary>
    /// Map API endpoints
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapApiEndpoints(this WebApplication app)
    { 
       
        app.MapCreateProduct()
           .MapGetProductById()
           .MapGetPagedProducts()
           .MapUpdateProduct()
           .MapDeleteProduct();
        return app;
    }
}
