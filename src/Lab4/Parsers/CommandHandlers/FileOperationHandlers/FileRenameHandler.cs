using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.FileCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.FileOperationHandlers;

public class FileRenameHandler : CommandHandlerBase<CommandRequest>
{
    private readonly CurrentPath _currentPath;
    private readonly IRenderable _renderable;

    public FileRenameHandler(CurrentPath currentPath, IRenderable renderable)
    {
        _currentPath = currentPath;
        _renderable = renderable;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("file rename ", StringComparison.OrdinalIgnoreCase))
        {
            if (Next != null)
            {
                return Next.Handle(request);
            }

            return request;
        }

        try
        {
            string[] parts = request.CommandName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string sourcePath = parts[2];
            string newName = string.Join(" ", parts.Skip(3));
            var command = new FileRenameCommand(
                sourcePath,
                newName,
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