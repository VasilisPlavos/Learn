using FluentResults;
using MediatR;

namespace Y25.Mediatr.Handlers;
public interface ICommandHandler<T> : IRequestHandler<T, Result> where T : ICommand { }
public interface ICommand : IRequest<Result> { }
