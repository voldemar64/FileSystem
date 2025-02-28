namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers;

public class NullCommandHandler : CommandHandlerBase<CommandRequest>
{
    public override CommandRequest? Handle(CommandRequest request)
    {
        return null;
    }
}