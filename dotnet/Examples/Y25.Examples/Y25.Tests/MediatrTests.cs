using Y25.Mediatr.Handlers;

namespace Y25.Tests;

public class MediatrTests
{
    [Test]
    public async Task PingRequestHandler_ShouldReturnPong()
    {
        // Arrange
        var handler = new PingRequestHandler();
        var request = new PingRequest();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo("Pong"));
    }

    [Test]
    public async Task NotifyHandler_ShouldReturnSuccess()
    {
        // Arrange
        var handler = new NotifyHandler();
        var notification = new NotifyRequest { Message = "Test" };

        // Act
        var result = await handler.Handle(notification, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }
}
