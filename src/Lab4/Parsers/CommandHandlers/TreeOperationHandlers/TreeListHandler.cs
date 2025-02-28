using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.TreeCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.TreeOperationHandlers;

public class TreeListHandler : CommandHandlerBase<CommandRequest>
{
    private readonly CurrentPath _currentPath;
    private readonly IRenderable _renderable;

    public TreeListHandler(CurrentPath currentPath, IRenderable renderable)
    {
        _currentPath = currentPath;
        _renderable = renderable;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (!request.CommandName.StartsWith("tree list", StringComparison.OrdinalIgnoreCase))
        {
            if (Next != null)
            {
                return Next.Handle(request);
            }

            return request;
        }

        try
        {
            var command = new TreeListCommand(request.Strategy ?? throw new InvalidOperationException(), _renderable);
            if (request.Depth.HasValue)
            {
                command.SetDepth(request.Depth.Value);
            }

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