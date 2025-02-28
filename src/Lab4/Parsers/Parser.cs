using Itmo.ObjectOrientedProgramming.Lab4.Commands.Renderables;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public class Parser : IParser
{
    private readonly ICommandHandler<CommandRequest> _commandHandler;
    private readonly IRenderable _renderable;

    public Parser(ICommandHandler<CommandRequest> commandHandler, IRenderable renderable)
    {
        _commandHandler = commandHandler;
        _renderable = renderable;
    }

    public void ParseAndRun(string input)
    {
        try
        {
            var request = new CommandRequest(input);

            _commandHandler.Handle(request);
        }
        catch (ArgumentException exception)
        {
            _renderable.Render($"No such command: {exception.Message}");
        }
        catch (Exception exception)
        {
            _renderable.Render($"Error: {exception.Message}");
        }
    }
}