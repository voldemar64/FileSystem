using Itmo.ObjectOrientedProgramming.Lab4.Commands.ConnectionCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.ConnectionHandlers;

public class DisconnectHandler : CommandHandlerBase<CommandRequest>
{
    private readonly CurrentPath _currentPath;
    private readonly IRenderable _renderable;

    public DisconnectHandler(CurrentPath currentPath, IRenderable renderable)
    {
        _currentPath = currentPath;
        _renderable = renderable;
    }

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.Equals("disconnect", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                string fullPath = request.Arguments[0];

                var command = new DisconnectCommand(request.Strategy ?? throw new InvalidOperationException());
                command.Execute(_currentPath);

                return request;
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