using FluentResults;
using MediatR;

namespace Y25.Mediatr.Handlers;

public class PingRequestHandler : IRequestHandler<PingRequest, Result<string>>
{
    public Task<Result<string>> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok("Pong"));
    }
}

public class PingRequest : IRequest<Result<string>> { }