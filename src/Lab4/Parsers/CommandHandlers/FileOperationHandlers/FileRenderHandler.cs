using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.FileCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.FileOperationHandlers;

public class FileRenderHandler : CommandHandlerBase<CommandRequest>
{
    private readonly ICommandHandler<CommandRequest> _nextHandler;
    private readonly CurrentPath _currentPath;
    private readonly IRenderable _renderable;

    public FileRenderHandler(ICommandHandler<CommandRequest> nextHandler, CurrentPath currentPath, IRenderable renderable)
    {
        _nextHandler = nextHandler;
        _currentPath = currentPath;
        _renderable = renderable;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file show ", StringComparison.OrdinalIgnoreCase))
        {
            return _nextHandler?.Handle(request);
        }

        try
        {
            string[] parts = request.CommandName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string filePath = string.Join(" ", parts.Skip(2));
            var command = new FileRenderCommand(
                filePath,
                request.Strategy ?? throw new InvalidOperationException());
            command.Execute(_currentPath);
            return request;
        }
        catch (Exception exception)
        {
            _renderable.Render($"Error: {exception.Message}");
            return request;
        }
    }
}