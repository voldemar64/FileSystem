using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers.CommandHandlers.ConnectionHandlers;

public class ConnectModeHandler : CommandHandlerBase<CommandRequest>
{
    private const string Mode = "local";

    public override CommandRequest? Handle(CommandRequest request)
    {
        if (request.CommandName.Equals("connect", StringComparison.OrdinalIgnoreCase) &&
            request.Mode?.Equals(Mode, StringComparison.OrdinalIgnoreCase) == true)
        {
            if (Next != null)
            {
                return Next.Handle(request.WithStrategy(new CommandStrategy()));
            }

            return request;
        }

        if (request.CommandName.Equals("disconnect", StringComparison.OrdinalIgnoreCase))
        {
            return request.WithStrategy(null).WithMode(null);
        }

        if (Next != null)
        {
            return Next.Handle(request);
        }

        return request;
    }
}