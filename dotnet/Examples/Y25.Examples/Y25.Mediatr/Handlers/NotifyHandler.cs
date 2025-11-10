using FluentResults;

namespace Y25.Mediatr.Handlers;

public class NotifyHandler : ICommandHandler<NotifyRequest>
{
    public Task<Result> Handle(NotifyRequest request, CancellationToken cancellationToken)
    {
        // In a real application, you would do something with the notification,
        // like sending an email, logging, etc.
        // For this example, we'll just return a success result.
        return Task.FromResult(Result.Ok());
    }
}
public class NotifyRequest : ICommand
{
    public string? Message { get; set; }
}
