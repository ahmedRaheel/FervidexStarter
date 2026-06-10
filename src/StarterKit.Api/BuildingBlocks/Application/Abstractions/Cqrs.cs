using MediatR;
namespace StarterKit.Api.BuildingBlocks.Application.CQRS;
public interface ICommand<out TResponse> : IRequest<TResponse> { }
public interface IQuery<out TResponse> : IRequest<TResponse> { }
