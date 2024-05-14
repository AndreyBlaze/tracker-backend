using MediatR;
using Shared;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Custom interface to override MediatR interface with our logic to separate commands
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Custom interfacs to override MediatR interface with our logic to separate commands with response
/// </summary>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
