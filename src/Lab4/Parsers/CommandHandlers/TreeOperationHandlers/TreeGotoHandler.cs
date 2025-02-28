using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.TreeCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.TreeOperationHandlers;

public class TreeGotoHandler : CommandHandlerBase<CommandRequest>
{
    private readonly CurrentPath _fileSystemState;
    private readonly IRenderable _renderable;

    public TreeGotoHandler(CurrentPath currentPath, IRenderable renderable)
    {
        _fileSystemState = currentPath;
        _renderable = renderable;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.StartsWith("tree goto", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                if (request.Arguments.Count > 0)
                {
                    string path = string.Join(" ", request.Arguments);
                    var command = new TreeGotoCommand(path, request.Strategy ?? throw new InvalidOperationException());
                    command.Execute(_fileSystemState);
                    return request;
                }
                else
                {
                    _renderable.Render("Error: Missing path argument for 'tree goto'.");
                    return request;
                }
            }
            catch (Exception exception)
            {
                _renderable.Render($"Error: {exception.Message}");
                return request;
            }
        }
        else if (Next != null)
        {
            return Next.Handle(request);
        }

        return null;
    }
}