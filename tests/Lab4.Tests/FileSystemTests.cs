using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.ConnectionHandlers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileSystemTests
{
    [Fact]
    public void ConnectCommand_WithValidPathAndLocalMode_ShouldUpdateCurrentPath()
    {
        Directory.CreateDirectory("C:\\Test");

        var currentPath = new CurrentPath();
        var renderableMock = new Mock<IRenderable>();
        var connectRequest = new CommandRequest("connect C:\\Test -m local");

        var connectionModeHandler = new ConnectModeHandler();
        connectionModeHandler
            .AddNext(new ConnectHandler(currentPath, renderableMock.Object))
            .AddNext(new NullCommandHandler());

        CommandRequest? result = connectionModeHandler.Handle(connectRequest);

        Assert.NotNull(result);
        Assert.Equal("C:\\Test", currentPath.Path);
        renderableMock.Verify(x => x.Render(It.IsAny<string>()), Times.Never);

        Directory.Delete("C:\\Test", true);
    }

    [Fact]
    public void DisconnectCommand_ShouldClearCurrentPath()
    {
        Directory.CreateDirectory("C:\\Test");

        var currentPath = new CurrentPath();
        var renderableMock = new Mock<IRenderable>();
        var connectRequest = new CommandRequest("connect C:\\Test -m local");
        var disconnectRequest = new CommandRequest("disconnect");

        var connectionTypeHandler = new ConnectModeHandler();
        connectionTypeHandler
            .AddNext(new ConnectHandler(currentPath, renderableMock.Object))
            .AddNext(new DisconnectHandler(currentPath, renderableMock.Object))
            .AddNext(new NullCommandHandler());

        connectionTypeHandler.Handle(connectRequest);
        Assert.NotNull(currentPath.Path);

        connectionTypeHandler.Handle(disconnectRequest);

        Assert.NotNull(currentPath.Path);
        renderableMock.Verify(x => x.Render(It.IsAny<string>()), Times.Never);

        Directory.Delete("C:\\Test", true);
    }
}