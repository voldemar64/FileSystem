using Itmo.ObjectOrientedProgramming.Lab4.Commands.CurrentPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ConnectionCommands;

public class DisconnectCommand : ICommand
{
    private readonly ICommandStrategy _strategy;

    public DisconnectCommand(ICommandStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void Execute(ICurrentPath path)
    {
        path.Path = _strategy.DisconnectCommand();
    }
}